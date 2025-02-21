using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{

    public static List<Transform> stack=new List<Transform>();
    public static void AddItem(Transform toadd)
    {
        stack.Add(toadd);
    }

    private void Update()
    {
        //Debug.Log(stack.Count);
    }



}
