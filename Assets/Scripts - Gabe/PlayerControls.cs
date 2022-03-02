using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private GameObject temp;

    public void MouseDrag(InputAction.CallbackContext context)
    {
        Vector2 pos2D = Mouse.current.position.ReadValue();
        pos2D = Camera.main.ScreenToWorldPoint(pos2D);
        RaycastHit2D hit = Physics2D.Raycast(pos2D, Vector2.zero, Mathf.Infinity);

        if (context.canceled)
        {
            if (temp != null)
            {
                useScript(temp, context);
            }
            temp = null;
        }
        else if (hit.collider != null)
        {
            if (temp == null)
            {
                useScript(hit.collider.gameObject, context);
                temp = hit.collider.gameObject;
            }
            else
            {
                useScript(temp, context);
            }
        }
    }

    private void useScript(GameObject obj, InputAction.CallbackContext context)
    {
        if (obj.tag == "Tourniquet")
        {
            if (obj.name == "Tourniquet Tab")
            {
                obj.transform.parent.gameObject.GetComponent<Tourniquet>().TourniquetDrag(context);
            }
            else
            {
                obj.GetComponent<Tourniquet>().TourniquetDrag(context);
            }
        }
        else if (obj.tag == "Scissors")
        {
            //obj.GetComponent<Scissors>().useScissors(context);
        }
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        Camera.main.GetComponent<Zoom>().ZoomIn(context);
    }
}
