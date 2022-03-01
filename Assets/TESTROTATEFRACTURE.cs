using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;


public class TESTROTATEFRACTURE : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


    /*public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("arm pressed");
        //Set a rate at which we should turn
        float turnSpeed = speed * Time.deltaTime;
        //Connect turning rate to horizonal motion for smooth transition
        float rotate = Input.GetAxis("Horizontal") * turnSpeed;
        //Get current rotation
        float currentRotation = gameObject.transform.rotation.eulerAngles.z;
        //Add current rotation to rotation rate to get new rotation
        Quaternion rotation = Quaternion.Euler(0, 0, currentRotation + rotate);
        //Move object to new rotation
        gameObject.transform.rotation = rotation;

    }
    */



    //please. please work please. i just want it to work. please. please. im sorry for my transgression. just let my code work. i swear omg pleas.e 
    private Camera myCam;
    private Vector2 screenPos;
    private float angleOffset;
    private Collider2D col;

    private bool isHeld;
    private bool isClicked;

    private void Start()
    {
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {

        Vector3 mousePos = myCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (isHeld)
        {
            Debug.Log("is held");
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                screenPos = myCam.WorldToScreenPoint(transform.position);
                Vector3 vec3 = Mouse.current.position.ReadValue() - screenPos;
                angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
            }
        }
        if (isClicked)
        {
            if (col == Physics2D.OverlapPoint(mousePos))
            {
                Vector3 vec3 = Mouse.current.position.ReadValue() - screenPos;
                float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        Debug.Log("Pressed down!");

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld=false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked = true;

    }
    

}

