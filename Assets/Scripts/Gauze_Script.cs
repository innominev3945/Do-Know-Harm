using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauze_Script : MonoBehaviour
{

    private Vector3 mousePosition;

    private int numClicks;
    private float timePassed;
    [SerializeField] int totalClicks;
    [SerializeField] float timeLimit;

    public bool healed;

    // Start is called before the first frame update
    void Start()
    {
        numClicks = 0;
        timePassed = 0;

        totalClicks = 12;
        timeLimit = 3;

        healed = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Debug.Log("Collision happened");
        if (collision.gameObject.tag == "Bleeding Wound")
        {
            // Debug.Log("Gauze is on the wound");
            if (Input.GetMouseButtonDown(0))
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
                    Debug.Log("Successfully stopped bleeding by applying pressure on wound");
                    // TODO: change wound sprite to indicate less blood
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
