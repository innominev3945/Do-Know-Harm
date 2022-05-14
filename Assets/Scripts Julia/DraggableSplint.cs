
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class DraggableSplint : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public delegate void DragEndedDelegate(DraggableSplint draggableObject);
    public DragEndedDelegate dragEndedCallback;
    //callback is invoked once the user drops the sprite

    private bool isDragged = false;
    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition; //used to calculate a new sprite position when you drag it

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(this.gameObject.name + " Was Clicked.");

        isDragged = true; //when you press down on the sprite, you start dragging it
        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); //variable holding current mouse position; converts cursor location from on the screen into the scene position
        spriteDragStartPosition = transform.localPosition; //holds the current sprite position
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");

        //updates sprite position when you drag the mouse:
        if (isDragged)
        {
            //finding the difference between the sprite's starting position and the mouse's starting and current position
            transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - mouseDragStartPosition);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("Mouse Up");
        isDragged = false;
        dragEndedCallback(this);

    }



}
