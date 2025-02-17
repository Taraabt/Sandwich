using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;
    #endregion

    public static InputManager Instance;


    private InputSystem_Actions playerControls;
    private Camera mainCamera;  

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        playerControls = new InputSystem_Actions();
        mainCamera= Camera.main;
    }
        
    private void OnEnable()
    {
        playerControls.Enable();  
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext ctx)
    {

        if (OnStartTouch != null) 
            OnStartTouch(Utils.ScreenToWorld(mainCamera,playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()),(float)ctx.startTime);

    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx)
    {

        if (OnEndTouch != null)
            OnEndTouch(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)ctx.time);

    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }


}
