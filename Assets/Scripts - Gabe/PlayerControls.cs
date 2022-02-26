using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    public void MouseDrag(InputAction.CallbackContext context)
    {
        Vector2 pos2D = Mouse.current.position.ReadValue();
        pos2D = Camera.main.ScreenToWorldPoint(pos2D);
        RaycastHit2D hit = Physics2D.Raycast(pos2D, Vector2.zero, Mathf.Infinity);

        //Debug.Log(pos2D);
        if (hit.collider != null && hit.collider.gameObject.tag == "Tourniquet")
        {
            if (hit.collider.gameObject.name == "Tourniquet Tab")
            {
                hit.collider.gameObject.transform.parent.gameObject.GetComponent<Tourniquet>().TourniquetDrag(context);
            }
            else
            {
                hit.collider.gameObject.GetComponent<Tourniquet>().TourniquetDrag(context);
            }
        }
        else if (hit.collider != null && hit.collider.gameObject.tag == "Scissors")
        {
            hit.collider.gameObject.GetComponent<Scissors>().useScissors(context);
        }
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        Camera.main.GetComponent<Zoom>().ZoomIn(context);
    }
}
