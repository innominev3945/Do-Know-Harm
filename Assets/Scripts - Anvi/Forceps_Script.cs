using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Forceps_Script : MonoBehaviour
{
    private Vector3 mousePosition;

    private bool mousePressed;
    private bool onForeignObject;
    private bool dragForeignObject;
    GameObject currentForeignObject;

    // Start is called before the first frame update
    void Start()
    {
        mousePressed = false;
        onForeignObject = false;
        dragForeignObject = false;
        currentForeignObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 1);

        if (currentForeignObject != null && dragForeignObject)
        {
            currentForeignObject.transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
        }
    }

    public void RemoveObject(InputAction.CallbackContext context)
    {
        if (context.started && onForeignObject)
        {
            mousePressed = true;
        }
        else if (context.canceled)
        {
            mousePressed = false;
            dragForeignObject = false;
            currentForeignObject = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foreign Object")
        {
            // Debug.Log("Forceps are on foreign object");
            onForeignObject = true;
            if (mousePressed)
            {
                currentForeignObject = collision.gameObject;
                dragForeignObject = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foreign Object")
        {
            // Debug.Log("Forceps no longer on foreign object");
            onForeignObject = false;
        }
    }
}
