using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandToolScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void ClickOnTarget(InputAction.CallbackContext context)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity);
        if (context.started)
        {
            //Vector2 pos2D = Mouse.current.position.ReadValue(); // is there a way to rid this of mouse dependency? last line left that uses it
            //pos2D = Camera.main.ScreenToWorldPoint(pos2D);
             // fix layering?
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.gameObject.name == "TourniquetTab2")
                {
                    Debug.Log("hit tab");
                    hit.collider.gameObject.GetComponentInParent<Tourniquet_Script>().StartTightening();
                }
                else if (hit.collider.gameObject.name == "Tourniquet2(Clone)")
                {
                    Debug.Log("hit tq");
                    hit.collider.gameObject.GetComponent<Tourniquet_Script>().mouseClickedTrue();
                }
            }
        }
        else if (context.canceled)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "Tourniquet2(Clone)")
                {
                    hit.collider.gameObject.GetComponent<Tourniquet_Script>().mouseClickedFalse();
                }
            }
        }
    }
}
