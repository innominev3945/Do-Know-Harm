using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragSprite : MonoBehaviour
{
    // see: https://youtu.be/eUWmiV4jRgU

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    [SerializeField] private GameObject arm;
    private bool isOnArm = false;

    void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }

        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
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
    }
}
