using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreign_Object_Script : MonoBehaviour
{
    // NOTE: Foreign Object should always be placed within the collider of the wound

    private bool exitedWound;
    private Vector3 woundLocation;
    private const float minimumDistanceAway = 5;
    [SerializeField] GameObject wound;

    // Start is called before the first frame update
    void Start()
    {
        exitedWound = false;
        woundLocation = wound.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Distance: " + Vector3.Distance(transform.position, woundLocation));
        //if (exitedWound)
        //{
            if (Vector3.Distance(transform.position, woundLocation) > minimumDistanceAway)
            {
                Destroy(gameObject);
            }
        //}
    }

    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bleeding Wound")
        {
            woundLocation = collision.gameObject.transform.position;
            exitedWound = true;
            Debug.Log("exitedWound = true");
        }
    }
    */
}
