using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tourniquet : MonoBehaviour
{
    // see: https://youtu.be/eUWmiV4jRgU

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    [SerializeField] private GameObject arm;
    private bool isOnArm = false;
    private Vector2 mousePosition;

    void Update()
    {
        /*if (isBeingHeld)
        {
            Vector3 mousePos = Input.mousePosition; // how to change to new input?
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePosition.x - startPosX, mousePosition.y - startPosY, 0); // previously mousePos instead of mousePosition
        }*/

        
    }

 /*   private void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void Drag(InputAction.CallbackContext context)
    {
        Debug.Log(Mouse.current.position.ReadValue());
    }*/

    /*private void OnMouseDown()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // Input.GetMouseButtonDown(0) previously
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;
        }

        
    }

    private void OnMouseUp() // checks if tourniquet placed on arm
    {
        isBeingHeld = false;
        Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        int layerMask = ~(LayerMask.GetMask("Draggable Objects"));
        RaycastHit2D hit = Physics2D.Raycast(pos2D, -Vector2.up, Mathf.Infinity, layerMask);
        Debug.Log(layerMask);
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == arm)
            {
                Debug.Log("arm hit");
                this.gameObject.transform.localPosition = new Vector3(arm.transform.position.x, transform.position.y, 0);
                isOnArm = true;
            }
            else
            {
                isOnArm = false;
            }
        }
        else
        {
            isOnArm = false;
        }

        if (isOnArm && arm.GetComponentInChildren<Transform>().position.y < transform.position.y) // checks tourniquet is on arm and is above wound
        {
            arm.GetComponentInChildren<WoundBleeding>().isBleeding = false;
        }
        else
        {
            arm.GetComponentInChildren<WoundBleeding>().isBleeding = true;
        }
    }*/
}
