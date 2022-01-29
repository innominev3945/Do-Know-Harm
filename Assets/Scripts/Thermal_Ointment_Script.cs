using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thermal_Ointment_Script : MonoBehaviour
{
    private Vector3 mousePosition;

    private bool mouseHeldDown;
    private bool enteredOtherTrigger;
    private bool exit;
    private int numTimes;
    [SerializeField] int totalNumTimes;
    private string triggerTag;

    public bool healed;

    // Start is called before the first frame update
    void Start()
    {
        mouseHeldDown = false;
        enteredOtherTrigger = false;
        exit = false;
        numTimes = 0;
        totalNumTimes = 3;
        triggerTag = "";
        healed = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 1);

        if (Input.GetMouseButtonUp(0))
        {
            // reset if mouse button released
            mouseHeldDown = false;
            enteredOtherTrigger = false;
            exit = false;
            numTimes = 0;
            triggerTag = "";
            Debug.Log("mouseHeldDown = false");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log("Inside Trigger");
        if (collision.gameObject.tag == "Trigger 1 (TO)" || collision.gameObject.tag == "Trigger 2 (TO)")
        {
            // first time mouse is pressed down inside a trigger
            if (/*!mouseHeldDown &&*/ Input.GetMouseButtonDown(0))
            {
                triggerTag = collision.gameObject.tag;
                mouseHeldDown = true;
                Debug.Log("mouseHeldDown = true");
            }
            // when entering trigger while mouse continues to be held down
            else if (mouseHeldDown && exit)
            {
                exit = false;
                if (triggerTag == collision.gameObject.tag && enteredOtherTrigger)
                {
                    enteredOtherTrigger = false;
                    numTimes++;
                    Debug.Log("numTimes: " + numTimes);
                    if (numTimes == totalNumTimes)
                    {
                        healed = true;
                        // TODO: change after getting correct sprites
                        GameObject burn = GameObject.Find("Burn");
                        burn.GetComponent<SpriteRenderer>().color = Color.white;
                        Debug.Log("Healed!");
                    }
                }
                else if (triggerTag != collision.gameObject.tag)
                {
                    enteredOtherTrigger = true;
                    Debug.Log("enteredOtherTrigger = true");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trigger 1 (TO)" || collision.gameObject.tag == "Trigger 2 (TO)")
        {
            Debug.Log("Exited Trigger (TO)");
            exit = true;
        }
    }
}
