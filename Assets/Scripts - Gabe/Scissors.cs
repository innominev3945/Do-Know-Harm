using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scissors : MonoBehaviour
{
    private bool inUse;

    void Start()
    {
        inUse = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inUse)
        {
            Vector2 pos2D = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = pos2D;
        }
    }

    public void useScissors(InputAction.CallbackContext context)
    {
        Debug.Log("called");
        if (context.started)
        {
            inUse = !inUse;
        }
    }
}
