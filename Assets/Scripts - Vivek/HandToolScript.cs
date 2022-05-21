using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandToolScript : MonoBehaviour
{


    private GameObject lastClickedObject;
    // Start is called before the first frame update
    void Start()
    {
        lastClickedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void ClickOnTarget(InputAction.CallbackContext context) // change so that variable if mousepressed true is contained in handtool object (Here) and set action for mousePressed to collider funcs in tourniquet
    {
        //int layerMask = ~(LayerMask.GetMask("Draggable Object"));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Draggable Object"));
        if (context.started)
        {
            //Vector2 pos2D = Mouse.current.position.ReadValue(); // is there a way to rid this of mouse dependency? last line left that uses it
            //pos2D = Camera.main.ScreenToWorldPoint(pos2D);
             // fix layering?
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.gameObject.tag == "Tourniquet")
                {
                    if (hit.collider.gameObject.transform.parent != null)
                    {
                        if (hit.collider.gameObject.transform.parent.gameObject.tag == "Tourniquet")
                        {
                            Debug.Log("hit tab");
                            if (hit.collider.gameObject.GetComponentInParent<Tourniquet_Script>().GetOnLimb()) {
                                hit.collider.gameObject.GetComponentInParent<Tourniquet_Script>().StartTightening();
                                hit.collider.gameObject.GetComponentInParent<Tourniquet_Script>().mouseClickedTrue();
                            }
                            else
                            {
                                hit.collider.gameObject.GetComponentInParent<Tourniquet_Script>().mouseClickedTrue();
                            }

                            lastClickedObject = hit.collider.gameObject.transform.parent.gameObject;
                        }
                        else
                        {
                            Debug.Log("hit tq");
                            hit.collider.gameObject.GetComponent<Tourniquet_Script>().mouseClickedTrue();
                            lastClickedObject = hit.collider.gameObject;
                        }
                    }
                    else
                    {
                        Debug.Log("hit tq");
                        hit.collider.gameObject.GetComponent<Tourniquet_Script>().mouseClickedTrue();
                        lastClickedObject = hit.collider.gameObject;
                    }
                }
            }
        }
        else if (context.canceled)
        {
            if (lastClickedObject != null)
            {
                if (lastClickedObject.tag == "Tourniquet")
                {
                    lastClickedObject.GetComponent<Tourniquet_Script>().mouseClickedFalse();
                    lastClickedObject = null;
                }
            }
            /*if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Tourniquet")
                {
                    hit.collider.gameObject.GetComponent<Tourniquet_Script>().mouseClickedFalse();
                }
            }*/
        }
    }
}
