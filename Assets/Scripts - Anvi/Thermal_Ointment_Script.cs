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
        private int numTimes;
        [SerializeField] int totalNumTimes;
        private string triggerTag;
        private int mouseHeldInTriggerCounter;
        private bool healed;


        public bool GetHealed() { return healed; }

        // Start is called before the first frame update
        void Start()
        {
            mouseHeldDown = false;
            enteredOtherTrigger = false;
            getTriggerTag = false;
            exit = false;
            numTimes = 0;
            totalNumTimes = 3;
            triggerTag = "";
            mouseHeldInTriggerCounter = 0;

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
            if (context.started)
            {
                getTriggerTag = true;

                Debug.Log("Started");
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
                Debug.Log("mouseHeldDown = false");
            }
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            // Debug.Log("Inside Trigger");
            if (collision.gameObject.tag == "Trigger 1 (TO)" || collision.gameObject.tag == "Trigger 2 (TO)")
            {
                // first time mouse is pressed down inside a trigger
                if (getTriggerTag && (mouseHeldInTriggerCounter >= 1 && mouseHeldInTriggerCounter <= 4)/*!mouseHeldDown &&*/ /*Input.GetMouseButtonDown(0)*/)
                {
                    triggerTag = collision.gameObject.tag;
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
                        Debug.Log("numTimes: " + numTimes);
                        if (numTimes >= totalNumTimes)
                        {
                            healed = true;
                            // TODO: change after getting correct sprites
                            //GameObject burn = GameObject.Find("Burn");
                            //burn.GetComponent<SpriteRenderer>().color = Color.white;
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
}