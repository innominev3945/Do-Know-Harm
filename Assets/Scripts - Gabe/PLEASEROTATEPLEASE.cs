using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;


public class PLEASEROTATEPLEASE : MonoBehaviour
{
    private Camera myCam;
    private Vector2 screenPos;
    private float angleOffset;
    private Collider2D col;


    private void Start()
    {
        myCam = Camera.main;
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
            
        
            Vector3 mousePos = myCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {

                if (col == Physics2D.OverlapPoint(mousePos))
                {
                    screenPos = myCam.WorldToScreenPoint(transform.position);
                    Vector3 vec3 = Mouse.current.position.ReadValue() - screenPos;
                    angleOffset = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(vec3.y, vec3.x)) * Mathf.Rad2Deg;
                    Debug.Log(transform.localEulerAngles.z);
            }
                }
            if (Mouse.current.leftButton.isPressed)
            {
                if (col == Physics2D.OverlapPoint(mousePos))
                {
                    Vector3 vec3 = Mouse.current.position.ReadValue() - screenPos;
                    float angle = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, angle + angleOffset);

                }
            }
        
        
    }

}
