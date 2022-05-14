using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;

public class rightLegFractureRealign : MonoBehaviour
{
    private Camera myCam;
    private Vector2 screenPos;
    private float angleOffset;
    private Collider2D col;

    private float boneStartingRotation = 318; //approximate value, used to check if player worsens the patient's fracture by moving it the wrong way


    private void Start()
    {
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //if bone is misaligned (bone is considered misaligned if bone's z rotation displayed in the inspector tab is less than -5 or greater than 5)
        if ((transform.rotation.eulerAngles.z > 276) || transform.rotation.eulerAngles.z < 270)
        {
            Vector3 mousePos = myCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                //Debug.Log(transform.rotation.eulerAngles.z);
                if (col == Physics2D.OverlapPoint(mousePos))
                {

                    screenPos = myCam.WorldToScreenPoint(transform.position);
                    Vector3 vec3 = Mouse.current.position.ReadValue() - screenPos;
                    angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;


                }
            }

            if (Mouse.current.leftButton.isPressed)
            {
                //Debug.Log(transform.rotation.eulerAngles.z);
                if (col == Physics2D.OverlapPoint(mousePos))
                {
                    Vector3 vec3 = Mouse.current.position.ReadValue() - screenPos;
                    float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);

                    if (transform.rotation.eulerAngles.z > boneStartingRotation)    //if bone is rotated the wrong way
                    {
                        //Debug.Log("BRO WRONG WAY YOURE MAKING IT WORSE");
                        //subtract health or something idk
                    }
                }
            }
        }
        //else
        //{
            //Debug.Log("Bone realigned!");
        //}
        

    }
}
