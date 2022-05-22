using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tape_Script : MonoBehaviour
{
    private Vector3 mousePosition;

    private bool stretchTape;
    private Vector2 startTape;

    private GameObject tapePivot;

    private bool insideBox1;
    private bool insideBox2;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;

        stretchTape = false;
        startTape = new Vector2(0, 0);
        tapePivot = GameObject.FindWithTag("Tape Pivot");

        insideBox1 = false;
        insideBox2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (!stretchTape)
        {
            tapePivot.transform.position = Vector2.Lerp(transform.position, new Vector3(mousePosition.x - 0.5f, mousePosition.y, mousePosition.z), 1);
        }
        else
        {
            Vector2 currentMoustPosition = new Vector2(mousePosition.x, mousePosition.y);
            float distance = Vector2.Distance(currentMoustPosition, startTape);
            Debug.Log("Distance of stretched tape: " + distance);
            Vector3 scale = new Vector3(1 + distance, 1, 1);
            tapePivot.transform.localScale = scale;

            float angle = Mathf.Atan2(mousePosition.y - tapePivot.transform.position.y, mousePosition.x - tapePivot.transform.position.x) * Mathf.Rad2Deg;
            tapePivot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    public void Extend_Tape(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            stretchTape = true;
            startTape = new Vector2(mousePosition.x, mousePosition.y);
        }
        else if (context.canceled)
        {
            stretchTape = false;

            if (insideBox1 && insideBox2)
            {
                Destroy(this);
            }
            else
            {
                tapePivot.transform.localScale = new Vector3(1, 1, 1);
                tapePivot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            insideBox1 = false;
            insideBox2 = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hit Box (Tape) 1")
        {
            insideBox1 = true;
        }
        else if (collision.gameObject.tag == "Hit Box (Tape) 2")
        {
            insideBox2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hit Box (Tape) 1")
        {
            insideBox1 = false;
        }
        else if (collision.gameObject.tag == "Hit Box (Tape) 2")
        {
            insideBox2 = false;
        }
    }
}
