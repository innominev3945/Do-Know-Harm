using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gauze_Script : MonoBehaviour
{

    private Vector3 mousePosition;

    private int numClicks;
    private float timePassed;
    [SerializeField] int totalClicks;
    [SerializeField] float timeLimit;
    private bool mouseButtonDown;
    private bool mouseHeldDown;

    public bool healed;

    // Start is called before the first frame update
    void Start()
    {
        numClicks = 0;
        timePassed = 0;

        totalClicks = 12;
        timeLimit = 3;

        mouseButtonDown = false;
        mouseHeldDown = false;

        healed = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
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
            if (timePassed >= timeLimit)
            {
                if (numClicks >= totalClicks)
                {
                    healed = true;
                    // TODO: change wound sprite to indicate less blood
                    GameObject wound = GameObject.Find("Bleeding Wound");
                    wound.GetComponent<SpriteRenderer>().color = Color.grey;
                    Debug.Log("Successfully stopped bleeding by applying pressure on wound");
                }
                else
                {
                    numClicks = 0;
                    timePassed = 0;
                }
            }
        }
    }
}
