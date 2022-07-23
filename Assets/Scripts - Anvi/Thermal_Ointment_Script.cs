using TreatmentClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ThermalOintmentClass
{
    public class Thermal_Ointment_Script : MonoBehaviour
    {
        private Vector3 mousePosition;

        private bool mouseHeldDown;
        private bool enteredOtherTrigger;
        private bool getTriggerTag;
        private bool exit;
        [SerializeField] private int numTimes;
        [SerializeField] int totalNumTimes;
        private string triggerTag;
        private int mouseHeldInTriggerCounter;
        private bool healed;

        [SerializeField] GameObject hitbox;
        private GameObject initialHitbox;
        private GameObject otherHitbox;
        private float hitbox1X;
        private float hitbox1Y;
        private float hitbox2X;
        private float hitbox2Y;

        public bool GetHealed() { return healed; }

        // Start is called before the first frame update
        void Start()
        {
            mouseHeldDown = false;
            enteredOtherTrigger = false;
            getTriggerTag = false;
            exit = false;
            numTimes = 0;
            totalNumTimes = 6;
            triggerTag = "";
            mouseHeldInTriggerCounter = 0;

            initialHitbox = null;
            otherHitbox = null;
            GameObject trigger1 = GameObject.FindWithTag("Trigger 1 (TO)");
            GameObject trigger2 = GameObject.FindWithTag("Trigger 2 (TO)");
            if (trigger1 == null || trigger2 == null)
            {
                Debug.Log("Need triggers for thermal ointment!");
                // TODO: have insurance that script will do nothing else
            }
            else
            {
                hitbox1X = trigger1.transform.position.x;
                hitbox1Y = trigger1.transform.position.y;
                hitbox2X = trigger2.transform.position.x;
                hitbox2Y = trigger2.transform.position.y;
                // assume that player will start with trigger 1
                initialHitbox = Instantiate(hitbox, new Vector3(hitbox1X, hitbox1Y, 0), Quaternion.identity);
                otherHitbox = Instantiate(hitbox, new Vector3(hitbox2X, hitbox2Y, 0), Quaternion.identity);
                Debug.Log("Create hit boxes");
            }

            healed = false;
        }

        // Update is called once per frame
        void Update()
        {
            mousePosition = Mouse.current.position.ReadValue();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, 1);

            if (getTriggerTag)
            {
                if (mouseHeldInTriggerCounter < 5)
                {
                    mouseHeldInTriggerCounter++;
                }
            }
            /*
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
            */
        }

        public void ApplyOintment(InputAction.CallbackContext context)
        {
            if (initialHitbox != null && otherHitbox != null)
            {
                if (context.started)
                {
                    getTriggerTag = true;

                    //Debug.Log("Started");
                }
                else if (context.canceled)
                {
                    // reset if mouse button released
                    mouseHeldDown = false;
                    enteredOtherTrigger = false;
                    exit = false;
                    numTimes = 0;
                    triggerTag = "";
                    getTriggerTag = false;
                    mouseHeldInTriggerCounter = 0;

                    if (initialHitbox.transform.position.x != hitbox1X && initialHitbox.transform.position.y != hitbox1Y)
                    {
                        // assume that player will start with trigger 1 (for initial hit box)
                        GameObject temp = initialHitbox;
                        initialHitbox = otherHitbox;
                        otherHitbox = temp;
                    }

                    initialHitbox.GetComponent<SpriteRenderer>().enabled = true;
                    otherHitbox.GetComponent<SpriteRenderer>().enabled = true;

                    Debug.Log("mouseHeldDown = false");
                }
            }
        }

        void OnTriggerStay2D(Collider2D collision)
        {
             Debug.Log("Inside Trigger");
            if (collision.gameObject.tag == "Trigger 1 (TO)" || collision.gameObject.tag == "Trigger 2 (TO)")
            {
                // first time mouse is pressed down inside a trigger
                if (getTriggerTag && (mouseHeldInTriggerCounter >= 1 && mouseHeldInTriggerCounter <= 4)/*!mouseHeldDown &&*/ /*Input.GetMouseButtonDown(0)*/)
                {
                    triggerTag = collision.gameObject.tag;
                    if (triggerTag == "Trigger 2 (TO)")
                    {
                        // if player starts with trigger 2 instead, swap the current initial hit box and other hit box
                        GameObject temp = initialHitbox;
                        initialHitbox = otherHitbox;
                        otherHitbox = temp;
                    }

                    // make initial hit box invisible
                    initialHitbox.GetComponent<SpriteRenderer>().enabled = false;
                    otherHitbox.GetComponent<SpriteRenderer>().enabled = true;

                    mouseHeldDown = true;
                    Debug.Log("mouseHeldDown = true");
                    getTriggerTag = false;
                }
                // when entering trigger while mouse continues to be held down
                else if (mouseHeldDown && exit)
                {
                    exit = false;
                    if (triggerTag == collision.gameObject.tag && enteredOtherTrigger)
                    {
                        enteredOtherTrigger = false;
                        numTimes++;
                        // TODO: fix error message associated with the below line
                        // Camera.main.GetComponent<SFXPlaying>().SFXstepClear();
                        Debug.Log("numTimes: " + numTimes);
                        if (numTimes >= totalNumTimes)
                        {
                            healed = true;
                            GameObject obChild = collision.gameObject.transform.GetChild(0).gameObject;
                            if (obChild != null)
                                obChild.tag = "Healed";
                            // TODO: change after getting correct sprites
                            //GameObject burn = GameObject.Find("Burn");
                            //burn.GetComponent<SpriteRenderer>().color = Color.white;
                            Debug.Log("Healed!");
                        }
                        else
                        {
                            initialHitbox.GetComponent<SpriteRenderer>().enabled = false;
                            otherHitbox.GetComponent<SpriteRenderer>().enabled = true;
                        }
                    }
                    else if (triggerTag != collision.gameObject.tag)
                    {
                        enteredOtherTrigger = true;
                        Debug.Log("enteredOtherTrigger = true");

                        initialHitbox.GetComponent<SpriteRenderer>().enabled = true;
                        otherHitbox.GetComponent<SpriteRenderer>().enabled = false;
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

        void OnDestroy()
        {
            if (initialHitbox != null)
            {
                Destroy(initialHitbox);
            }
            if (otherHitbox != null)
            {
                Destroy(otherHitbox);
            }
        }
    }
}