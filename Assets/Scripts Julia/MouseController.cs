using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{

    GameObject objSelected = null;

    public GameObject[] snapPoints;
    private float snapSensitivity = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckHitObject();

        }

        if(Mouse.current.leftButton.isPressed && objSelected != null)
        {
            //drag an object
                DragObject();

        }

        if(Mouse.current.leftButton.wasReleasedThisFrame && objSelected != null)
        {
            //drop object
                DropObject();
        }

    }

    void CheckHitObject()
    {
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
    
        if (hit2D.collider != null && hit2D.collider.gameObject.CompareTag("Draggable"))
        {
            objSelected = hit2D.transform.gameObject;
        }
    }

    void DragObject()
    {
        //objSelected.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 10.0f));

        //objSelected.transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());


        //if (hit2D.collider.gameObject.CompareTag("Draggable"))
        //{
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.farClipPlane * .5f;  //490 z, distance is off
                                                          //mousePos.z = 0; //-10 z, disappears

            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

            objSelected.transform.position = worldPoint;
        //}
    }

    void DropObject()
    {
        //RaycastHit2D hit2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
        //if (hit2D.collider.gameObject.CompareTag("Draggable"))
        //{
            for (int i = 0; i < snapPoints.Length; i++)
            {
                //Debug.Log("For");
                //Debug.Log(Vector3.Distance(snapPoints[i].transform.position, objSelected.transform.position));
                if (Vector2.Distance(snapPoints[i].transform.position, objSelected.transform.position) < snapSensitivity)
                {
                    objSelected.transform.position = new Vector3(snapPoints[i].transform.position.x, snapPoints[i].transform.position.y, snapPoints[i].transform.position.z - 0.1f);
                

            }
                objSelected = null;
            }
        //}
    }

}
