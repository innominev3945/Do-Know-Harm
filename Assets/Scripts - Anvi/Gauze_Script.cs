using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gauze
{
    public class Gauze_Script : MonoBehaviour
    {

        private Vector3 mousePosition;

        [SerializeField] private int numClicks;
        private float timePassed;
        [SerializeField] int totalClicks;
        [SerializeField] float timeLimit;
        private bool mouseButtonDown;
        private bool mouseHeldDown;
        private int mouseHeldInBloodPoolCounter;
        private bool mouseHeldBloodPool;
        private bool mouseHeldInBloodPoolCollider;
        [SerializeField] float shrinkScale;

        private bool healed;

        public bool GetHealed() { return healed; }

        // Start is called before the first frame update
        void Start()
        {
            numClicks = 0;
            timePassed = 0;

            totalClicks = 12;
            timeLimit = 3;

            mouseButtonDown = false;
            mouseHeldDown = false;

            mouseHeldInBloodPoolCounter = 0;
            mouseHeldBloodPool = false;
            mouseHeldInBloodPoolCollider = false;

            shrinkScale = -0.3f;

            healed = false;
        }

        // Update is called once per frame
        void Update()
        {
            mousePosition = Mouse.current.position.ReadValue();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, 1);

            if (mouseHeldBloodPool)
            {
                if (mouseHeldInBloodPoolCounter < 5)
                {
                    mouseHeldInBloodPoolCounter++;
                }
            }
        }

        public void StopBleeding(InputAction.CallbackContext context)
        {
            if (context.started /*&& !mouseHeldDown*/)
            {
                mouseButtonDown = true;
                mouseHeldDown = true;
            }
            else
            {
                mouseButtonDown = false;
                mouseHeldDown = false;
            }
        }

        public void AbsorbBlood(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                mouseHeldBloodPool = true;
            }
            else if (/*context.performed || */context.canceled)
            {
                mouseHeldBloodPool = false;
                mouseHeldInBloodPoolCounter = 0;
                mouseHeldInBloodPoolCollider = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            // Debug.Log("Collision happened");
            if (collision.gameObject.tag == "Bleeding Wound")
            {
                // Debug.Log("Gauze is on the wound");
                if (mouseButtonDown)
                {
                    numClicks++;
                    Debug.Log("numClicks: " + numClicks);
                }
                timePassed = timePassed + Time.deltaTime;
                // Debug.Log("timePassed: " + timePassed);
                Debug.Log("Colliding");
                if (timePassed >= timeLimit)
                {
                    if (numClicks >= totalClicks)
                    {
                        healed = true;
                        GameObject obChild = collision.gameObject.transform.GetChild(0).gameObject;
                        if (obChild != null)
                            obChild.tag = "Healed";
                        // TODO: change wound sprite to indicate less blood
                        //GameObject wound = GameObject.Find("Bleeding Wound");
                        //wound.GetComponent<SpriteRenderer>().color = Color.grey;
                        Debug.Log("Successfully stopped bleeding by applying pressure on wound");
                    }
                    else
                    {
                        numClicks = 0;
                        timePassed = 0;
                    }
                }
            }

            if (collision.gameObject.tag == "Blood Pool")
            {
                Debug.Log("Inside Blood Pool Collider");
                if (mouseHeldBloodPool && (mouseHeldInBloodPoolCounter >= 1 && mouseHeldInBloodPoolCounter <= 4))
                {
                    mouseHeldInBloodPoolCollider = true;
                    Debug.Log("Mouse held down inside blood pool collider");
                }
                if (mouseHeldInBloodPoolCollider)
                {
                    // shrink blood pool sprite
                    Debug.Log("Blood pool shrinking");
                    // just need to check length of one dimension (object as whole decreases in size)
                    if (collision.gameObject.transform.localScale.x >= 0.2f)
                    {
                        collision.gameObject.transform.localScale += new Vector3(shrinkScale, shrinkScale, shrinkScale) * Time.deltaTime;
                    }
                    else
                    {
                        Camera.main.GetComponent<SFXPlaying>().SFXstepClear();
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
    }
}