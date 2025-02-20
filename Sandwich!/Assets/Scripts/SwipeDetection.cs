using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField,Range(0f,1f)]
    private float directionThreshold = Mathf.Sqrt(2);
    [SerializeField] LayerMask layerMask;

    private InputManager inputManager;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    RaycastHit hit,hit2; 

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        Ray ray=Camera.main.ScreenPointToRay(position);
        Debug.DrawRay(ray.origin, ray.direction*10f,Color.red,5f);
        if (Physics.Raycast(ray,out hit,10f,layerMask))
        {
            startPosition = position;
            startTime = time;
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition)>=minimumDistance&&
            (endTime - startTime)<=maximumTime){          
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up,direction)> directionThreshold)
        {
            Vector3 pos = new Vector3(hit.transform.position.x, 1f, hit.transform.position.z+1f);
            Swipe(pos,Vector2.up, Vector3.right);
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Vector3 pos = new Vector3(hit.transform.position.x, 1f, hit.transform.position.z - 1f);
            Swipe(pos,Vector2.down, Vector3.left);
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Vector3 pos = new Vector3(hit.transform.position.x+1f, 1f, hit.transform.position.z);
            Swipe(pos,Vector2.right, Vector3.back);
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Vector3 pos = new Vector3(hit.transform.position.x-1f, 1f,hit.transform.position.z);
            Swipe(pos,Vector2.left,Vector3.forward);
        }
    }

    public void Swipe(Vector3 dir,Vector2 point,Vector3 axis)
    {        
        Debug.DrawRay(dir, Vector3.down * 1f, Color.red, 5f);
        //Transform parent = GetParent(hit.transform);
        if (Physics.Raycast(dir,Vector3.down,out hit2,1f,layerMask))
        {
            Transform parent =hit2.transform;
            Vector3 point3 = new Vector3(point.x, parent.transform.position.y * 2, point.y);
            Vector3 rotpoint = hit.transform.position + (point3 * 0.5f);
            hit.transform.RotateAround(rotpoint,axis,180);
            hit.transform.SetParent(parent);
            Debug.Log(parent.transform.position);
            Vector3 newPosition = parent.transform.position;
            newPosition.y += (parent.transform.childCount) * 0.1f;
            Debug.Log(parent.transform.childCount);
            parent.transform.GetChild(0).position= newPosition;
        }
    }

    public Transform GetParent(Transform child)
    {
        Transform parentTransform = child;
        while(parentTransform != null)
        {
            if (parentTransform.parent == null)
                return parentTransform;
            parentTransform = parentTransform.parent;
        }
        return parentTransform;
    }



}
