using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gauze
{
    public class Gauze_Script : MonoBehaviour
    {
        // ***** IMPORTANT: PLEASE READ *****
        // WHENEVER GUAZE IS USED IN A SCENE, ALSO INCLUDE THE GAUZE HIT BOX MANAGER GAME OBJECT IN THE SCENE
        // PLEASE LOOK AT THE TODO COMMENTS IN THE GAUZE_HIT_BOX_MANAGER_SCRIPT EVERY TIME GAUZE IS USED FOR A LEVEL

        private Vector3 mousePosition;

        [SerializeField] private int numClicks;
        private float timePassed;
        [SerializeField] int totalClicks;
        [SerializeField] float timeLimit;
        private bool mouseButtonDown;
        private bool rightmouseButtonDown;
        private bool mouseHeldDown;
        private int mouseHeldInBloodPoolCounter;
        private bool mouseHeldBloodPool;
        private bool mouseHeldInBloodPoolCollider;
        [SerializeField] float shrinkScale;

        [SerializeField] GameObject hitbox;
        [SerializeField] GameObject gauzeHitBoxCollider;
        [SerializeField] private GameObject gauzeCollider;
        [SerializeField] GameObject gauzeImage;
        public List<GameObject> allHitBoxes;

        GameObject gauzeHitBoxManager;

        private bool healed;

        // TODO: there are two cases for healed
        // 1. when bleeding is stopped by applying pressure
        // 2. when applying gauze squares to cover the wound
        public bool GetHealed() { return healed; }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("bruhhuhuh");
            // obtain hit box positions and create hit boxes
            gauzeHitBoxManager = GameObject.Find("Gauze Hit Box Manager");
            List<float> positionsX = gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().allHitBoxLocationsX();
            List<float> positionsY = gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().allHitBoxLocationsY();
            for (int i = 0; i < positionsX.Count; i++)
            {
                allHitBoxes.Add(Instantiate(hitbox, new Vector3(positionsX[i], positionsY[i], 0), Quaternion.identity));
            }

            gauzeCollider = Instantiate(gauzeHitBoxCollider, new Vector3(0, 0, 0), Quaternion.identity);

            numClicks = 0;
            timePassed = 0;

            totalClicks = 12;
            timeLimit = 3;

            mouseButtonDown = false;
            rightmouseButtonDown = false;
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

            if (gauzeCollider != null)
            {
                //Debug.Log("Have gauze collider");
                if (gauzeCollider.GetComponent<Gauze_Hit_Box_Collider_Script>().insideHitBox())
                {
                    Debug.Log("Inside hit box function");
                    if (rightmouseButtonDown)
                    {
                        Debug.Log("Placed down gauze square");
                        float xCoord = gauzeCollider.GetComponent<Gauze_Hit_Box_Collider_Script>().getCollisionX();
                        float yCoord = gauzeCollider.GetComponent<Gauze_Hit_Box_Collider_Script>().getCollisionY();
                        Instantiate(gauzeImage, new Vector3(xCoord, yCoord, 0), Quaternion.identity);
                        gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().notifyHitBoxCovered(xCoord, yCoord);

                        // to make sure this body of code is executed only once and not multiple times for a single right mouse click
                        rightmouseButtonDown = false;
                    }
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

        public void AddGauzeSquare(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log("Right mouse button pressed (gauze)");
                rightmouseButtonDown = true;
            }
            else if (context.canceled)
            {
                rightmouseButtonDown = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.name);
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
                //Debug.Log("Colliding");
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

        void OnDestroy()
        {
            foreach(GameObject hitbox in allHitBoxes)
            {
                Destroy(hitbox);
            }

            if (gauzeCollider != null)
            {
                Destroy(gauzeCollider);
            }
        }
    }
}