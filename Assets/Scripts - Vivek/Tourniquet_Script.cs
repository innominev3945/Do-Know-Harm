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
    [SerializeField] private GameObject leftEnd;
    [SerializeField] private GameObject rightEnd;
    [SerializeField] private GameObject strap;
    private Vector3 originalScale;
    [SerializeField] private Vector3 armScale;
    [SerializeField] private Vector3 legScale;


    private void Start()
    {
        isTightening = false;
        finished = false;
        mousePressed = false;
        onLimb = false;
        strap.SetActive(false);

        rotations = 0;
        maxRotations = 3;
        originalScale = this.transform.localScale;
    }

    private void Update()
    {
        //Debug.Log(mousePressed);
        if (!finished)
        {
            if (mousePressed && !isTightening)
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
                if (mousePressed)
                {
                    //Mouse.current.position.ReadValue();
                    Vector2 pos2D = Mouse.current.position.ReadValue();
                    pos2D = Camera.main.ScreenToWorldPoint(pos2D);
                    strap.SetActive(true);

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
                            else
                            {
                                currentQuad = mouseLocation;
                            }
                        }
                        Vector3 strapStartPos;
                        if (currentQuad == startQuad || currentQuad == NextQuad(startQuad))
                        {
                            strap.GetComponent<SpriteRenderer>().sortingLayerName = "Draggable Object";
                            strap.GetComponent<SpriteRenderer>().sortingOrder = 1;
                            strapStartPos = rightEnd.transform.position;
                        }
                        else
                        {
                            strap.GetComponent<SpriteRenderer>().sortingLayerName = "Patient";
                            strap.GetComponent<SpriteRenderer>().sortingOrder = 0;
                            strapStartPos = leftEnd.transform.position;
                        }
                        if (Vector3.Distance(strapStartPos, pos2D) > 4)
                        {
                            rotations = 0;
                            isTightening = false;
                        }
                        else
                        {
                            Stretch(strap, strapStartPos, pos2D, true);
                        }
                    }
                    if (rotations >= maxRotations) // finish treatment here
                    {
                        finished = true;
                        isTightening = false;
                        strap.SetActive(false);
                    }
                }
                else
                {
                    isTightening = false;
                    rotations = 0;
                    strap.SetActive(false);
                }
                
            }
            else
            {
                strap.SetActive(false);
                rotations = 0;
            }
        }
        
    }

    public void Stretch(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = _sprite.transform.localScale;
        scale.x = Vector3.Distance(_initialPosition, _finalPosition) / _sprite.transform.parent.transform.localScale.x / 5.5f;
        _sprite.transform.localScale = scale;
    }

    public bool GetHealed()
    {
        return finished;
    }
    
    public bool GetOnLimb()
    {
        return onLimb;
    }

    public void mouseClickedTrue()
    {
        mousePressed = true;
    }

    public void mouseClickedFalse()
    {
        mousePressed = false;
        if (onLimb && !finished)
        {
            Camera.main.GetComponent<SFXPlaying>().SFXstepClear();
        }
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
            if (arm.name.Contains("arm") || arm.name.Contains("Arm")) // change this to tag when tag system is decided
            {
                //transform.parent = arm.transform;
                Vector3 center = arm.GetComponent<Collider2D>().bounds.center;
                transform.position = new Vector2(center.x, this.transform.position.y); // add rotation effect later maybe? see below
                /*transform.rotation = arm.transform.rotation;

                float heightDiff = transform.position.y - arm.transform.position.y;
                Vector3 eulerAngles = arm.transform.rotation.eulerAngles;
                float xDiff = Mathf.Tan(Mathf.Deg2Rad * eulerAngles.z) * heightDiff;
                transform.position = new Vector2(arm.transform.position.x - xDiff, transform.position.y);*/
                if (!mousePressed)
                {
                    this.transform.localScale = armScale;
                }
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
        this.transform.localScale = originalScale;
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

    private int NextQuad(int a)
    {
        if (a >= 3)
        {
            return 0;
        }
        else
        {
            return a + 1;
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
