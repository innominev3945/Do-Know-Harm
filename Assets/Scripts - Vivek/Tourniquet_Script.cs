using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tourniquet_Script : MonoBehaviour
{
    private bool isTightening;
    private bool finished;
    [SerializeField] public bool mousePressed;
    private bool onLimb;

    private int currentQuad;
    [SerializeField] private int rotations;
    private int startQuad;
    private int maxRotations;
    [SerializeField] private GameObject tab;

    private void Start()
    {
        isTightening = false;
        finished = false;
        mousePressed = false;
        onLimb = false;

        rotations = 0;
        maxRotations = 3;
    }

    private void Update()
    {
        //Debug.Log(mousePressed);
        if (!finished)
        {
            if (mousePressed)
            {
                Vector2 pos2D = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                transform.position = pos2D;
            }
            else if (!mousePressed && !onLimb)
            {
                Vector3 temp = new Vector3(150, 700, 0);

                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(temp).x, Camera.main.ScreenToWorldPoint(temp).y, 0);
            }
            if (isTightening)
            {
                Mouse.current.position.ReadValue();
                Vector2 pos2D = Mouse.current.position.ReadValue();
                pos2D = Camera.main.ScreenToWorldPoint(pos2D);
                RaycastHit2D hit = Physics2D.Raycast(pos2D, Vector2.zero, Mathf.Infinity);
                if (hit.collider != null && hit.collider.gameObject == this) // checks if mouse hits tourniquet, resets if true
                {
                    rotations = 0;
                    isTightening = false;
                }
                else
                {
                    int mouseLocation = FindPos(pos2D);
                    if (mouseLocation != currentQuad)
                    {
                        if (InOrderAdjacent(currentQuad, mouseLocation))
                        {
                            currentQuad = mouseLocation;
                            if (mouseLocation == startQuad)
                            {
                                rotations++;
                            }
                        }
                    }
                }
                if (rotations >= maxRotations) // finish treatment here
                {
                    finished = true;
                    isTightening = false;
                    //Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
                }
            }
            else
            {
                rotations = 0;
            }
        }
        
        
        if (finished)
        {
        }
    }
    public bool GetHealed()
    {
        return finished;
    }

    public void mouseClickedTrue()
    {
        mousePressed = true;
    }

    public void mouseClickedFalse()
    {
        mousePressed = false;
    }

    public void ClickTourniquet() // rework so input action is on hand tool instead, have reaction to it here
    {


        /*if (context.started)
        {
            mousePressed = true;
            if (onLimb && isSet)
            {
                Vector2 pos2D = Mouse.current.position.ReadValue(); // is there a way to rid this of mouse dependency? last line left that uses it
                pos2D = Camera.main.ScreenToWorldPoint(pos2D);
                RaycastHit2D hit = Physics2D.Raycast(pos2D, Vector2.zero, Mathf.Infinity); // fix layering?
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.gameObject == tab)
                    {
                        Debug.Log("tab hit");
                        isTightening = true; // starts tightening input check
                        Mouse.current.position.ReadValue();
                        Vector2 posCheck = Mouse.current.position.ReadValue();
                        pos2D = Camera.main.ScreenToWorldPoint(posCheck);
                        startQuad = FindPos(posCheck);
                        currentQuad = startQuad;
                    }
                    else if (hit.collider.gameObject == this.gameObject)
                    {
                        Debug.Log("tq hit");
                        isSet = false;
                    }
                }
            }
        }
        else if (context.canceled)
        {
            if (onLimb)
            {
                isSet = true;
            }
            mousePressed = false;
            isTightening = false;
        }
        Debug.Log(mousePressed);*/
    }

    public void StartTightening()
    {
        isTightening = true; // starts tightening input check
        Vector2 pos2D = Mouse.current.position.ReadValue();
        Vector2 posCheck = Mouse.current.position.ReadValue();
        pos2D = Camera.main.ScreenToWorldPoint(posCheck);
        startQuad = FindPos(posCheck);
        currentQuad = startQuad;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //if (!mousePressed)
        // {
        Debug.Log(collision.gameObject.name);
            GameObject arm = collision.gameObject;
            if (arm.name == "UpperArm") // change this to tag when tag system is decided
            {
                //transform.parent = arm.transform;
                Vector3 center = arm.GetComponent<Collider2D>().bounds.center;
                transform.position = new Vector2(center.x, this.transform.position.y); // add rotation effect later maybe? see below
                /*transform.rotation = arm.transform.rotation;

                float heightDiff = transform.position.y - arm.transform.position.y;
                Vector3 eulerAngles = arm.transform.rotation.eulerAngles;
                float xDiff = Mathf.Tan(Mathf.Deg2Rad * eulerAngles.z) * heightDiff;
                transform.position = new Vector2(arm.transform.position.x - xDiff, transform.position.y);*/

                Debug.Log(center);
            }
            onLimb = true;
        //Debug.Log("placed");
        
       // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "UpperArm")
        {
            onLimb = false;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            //transform.parent = null;
        }
    }

    private int FindPos(Vector2 pos2D)
    {
        if (pos2D.x < transform.position.x) // sets starting quadrant
        {
            if (pos2D.y < transform.position.y)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (pos2D.y < transform.position.y)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }

    private bool InOrderAdjacent(int a, int b) // checks if a and b are in order and adjacent according to (0, 1, 2, 3)
    {
        if (a < 3)
        {
            if (b == a + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (b == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
