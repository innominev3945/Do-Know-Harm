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
        private int totalClicks;
        [SerializeField] float timeLimit;
        private bool mouseButtonDown;
        private bool rightmouseButtonDown;
        private bool mouseHeldDown;
        private int mouseHeldInBloodPoolCounter;
        private bool mouseHeldBloodPool;
        private bool mouseHeldInBloodPoolCollider;
        [SerializeField] float shrinkScale;

        // TODO: have gauze hitbox manager manage the creation of hitboxes
        [SerializeField] GameObject hitbox;
        [SerializeField] GameObject gauzeHitBoxCollider;
        GameObject gauzeCollider;
        [SerializeField] GameObject gauzeImage;
        public List<GameObject> allHitBoxes;

        [SerializeField] Sprite gauze0;
        [SerializeField] Sprite gauze1;
        [SerializeField] Sprite gauze2;
        [SerializeField] Sprite gauze3;
        [SerializeField] Sprite gauze4;
        // [SerializeField] Sprite cooldown;
        private List<Sprite> gauzeSprites;
        private SpriteRenderer sprender;
        private int currentGauzeSprite;

        GameObject[] gauzeHitBoxManagers;

        private int lockBleedingWound;

        private bool undergoDressing;

        private bool healed;

        // TODO: there are two cases for healed
        // 1. when bleeding is stopped by applying pressure
        // 2. when applying gauze squares to cover the wound
        public bool GetHealed() { return healed; }

        public bool doingDressing()
        {
            return undergoDressing;
        }

        public void changeDressing(bool state)
        {
            undergoDressing = state;
        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Perform start method");

            gauzeSprites = new List<Sprite>();
            gauzeSprites.Add(gauze0);
            gauzeSprites.Add(gauze1);
            gauzeSprites.Add(gauze2);
            gauzeSprites.Add(gauze3);
            gauzeSprites.Add(gauze4);

            currentGauzeSprite = 0;

            sprender = gameObject.GetComponent<SpriteRenderer>();
            sprender.sprite = gauzeSprites[currentGauzeSprite];

            undergoDressing = false;
            // TODO: fix Unity Exception
            if (GameObject.FindWithTag("Gauze Hit Box Manager") != null)
            {
                undergoDressing = true;
            }

            if (undergoDressing)
            {
                /*
                // obtain hit box positions and create hit boxes
                gauzeHitBoxManager = GameObject.FindWithTag("Gauze Hit Box Manager");
                List<float> positionsX = gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().allHitBoxLocationsX();
                List<float> positionsY = gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().allHitBoxLocationsY();
                // List<bool> hitBoxNotCovered = gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().allHitBoxNotCovered();
                for (int i = 0; i < positionsX.Count; i++)
                {
                    // if (hitBoxNotCovered[i])
                        allHitBoxes.Add(Instantiate(hitbox, new Vector3(positionsX[i], positionsY[i], 0), Quaternion.identity));
                }
                */
                gauzeCollider = Instantiate(gauzeHitBoxCollider, new Vector3(0, 0, 0), Quaternion.identity);
                
            }

            numClicks = 0;
            timePassed = 0;

            totalClicks = 12;
            timeLimit = 3;

            Debug.Log("Initialized totalClicks to: " + totalClicks);

            mouseButtonDown = false;
            rightmouseButtonDown = false;
            mouseHeldDown = false;

            mouseHeldInBloodPoolCounter = 0;
            mouseHeldBloodPool = false;
            mouseHeldInBloodPoolCollider = false;

            shrinkScale = -0.3f;

            lockBleedingWound = 0;

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
                    if (rightmouseButtonDown && sprender.sprite == gauzeSprites[0])
                    {
                        Debug.Log("Placed down gauze square");
                        float xCoord = gauzeCollider.GetComponent<Gauze_Hit_Box_Collider_Script>().getCollisionX();
                        float yCoord = gauzeCollider.GetComponent<Gauze_Hit_Box_Collider_Script>().getCollisionY();
                        Instantiate(gauzeImage, new Vector3(xCoord, yCoord, 0), Quaternion.identity);
                        // gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().notifyHitBoxCovered(xCoord, yCoord);
                        gauzeHitBoxManagers = GameObject.FindGameObjectsWithTag("Gauze Hit Box Manager");
                        foreach (GameObject manager in gauzeHitBoxManagers)
                        {
                            manager.GetComponent<Gauze_Hit_Box_Manager_Script>().notifyHitBoxCovered(xCoord, yCoord);
                        }

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
                /*
                if (mouseHeldInBloodPoolCollider && currentGauzeSprite <= 3)
                {
                    currentGauzeSprite++;
                    sprender.sprite = gauzeSprites[currentGauzeSprite];
                }
                */

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
                Debug.Log("timePassed: " + timePassed + "\ntimeLimit: " + timeLimit);
                // Debug.Log("Colliding");
                if (timePassed >= timeLimit)
                {
                    if (numClicks >= totalClicks)
                    {
                        Debug.Log("numClicks: " + numClicks);
                        Debug.Log("totalClicks: " + totalClicks);

                        lockBleedingWound++;

                        if (lockBleedingWound == 1)
                        {
                            healed = true;

                            GameObject obChild = collision.gameObject.transform.GetChild(0).gameObject;
                            Debug.Log("healed: " + obChild.tag);
                            if (obChild != null)
                                obChild.tag = "Healed";
                            Debug.Log("healed: " + obChild.tag);

                            // TODO: change wound sprite to indicate less blood
                            //GameObject wound = GameObject.Find("Bleeding Wound");
                            //wound.GetComponent<SpriteRenderer>().color = Color.grey;

                            if (currentGauzeSprite <= 3)
                            {
                                currentGauzeSprite++;
                                sprender.sprite = gauzeSprites[currentGauzeSprite];
                            }
                            if (currentGauzeSprite == 4)
                            {
                                StartCoroutine(gauzeIsDead());
                            }

                            Debug.Log("Successfully stopped bleeding by applying pressure on wound");
                        }
                    }
                    else
                    {
                        numClicks = 0;
                        timePassed = 0;

                        lockBleedingWound = 0;
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
                        // TODO: fix error associated with the below line
                        //Camera.main.GetComponent<SFXPlaying>().SFXstepClear();
                        Debug.Log("Will destory blood pool");
                        collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                        // Destroy(collision.gameObject);

                        // Mark blood pool as healed
                        GameObject obChild = collision.gameObject.transform.GetChild(0).gameObject;
                        Debug.Log("healed: " + obChild.tag);
                        if (obChild != null)
                            obChild.tag = "Healed";
                        Debug.Log("healed: " + obChild.tag);

                        if (currentGauzeSprite <= 3)
                        {
                            currentGauzeSprite++;
                            sprender.sprite = gauzeSprites[currentGauzeSprite];
                        }
                        if (currentGauzeSprite == 4)
                        {
                            StartCoroutine(gauzeIsDead());
                        }
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Bleeding Wound")
            {
                numClicks = 0;
                timePassed = 0;

                lockBleedingWound = 0;
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

        private IEnumerator gauzeIsDead()
        {
            Debug.Log("Start cooldown");
            Collider2D gauzeCollider = gameObject.GetComponent<CircleCollider2D>();
            GameObject toolWheel = GameObject.Find("Wheel");
            gauzeCollider.enabled = false;
            if (toolWheel != null)
            {
                toolWheel.GetComponent<Tool_Wheel_Script>().setDeactivation(true);
            }
            yield return new WaitForSecondsRealtime(5);
            gauzeCollider.enabled = true;
            if (toolWheel != null)
            {
                toolWheel.GetComponent<Tool_Wheel_Script>().setDeactivation(false);
            }
            Debug.Log("End cooldown");

            currentGauzeSprite = 0;
            sprender.sprite = gauzeSprites[currentGauzeSprite];
        }
    }
}