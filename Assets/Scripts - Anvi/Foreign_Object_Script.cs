using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForeignObjectClass
{
    public class Foreign_Object_Script : MonoBehaviour
    {
        // NOTE: Foreign Object should always be placed within the collider of the wound

        // private bool exitedWound;
        // private Vector3 woundLocation;
        // private const float minimumDistanceAway = 5;
        private GameObject wound;
        private bool healed;

        public bool GetHealed() { return healed; }

        public void SetWound(GameObject woundObj)
        {
            wound = woundObj;
        }

        // Start is called before the first frame update
        void Start()
        {
            // exitedWound = false;
            // woundLocation = wound.transform.position;
            // woundLocation = new Vector3(0, 0, 0);
            healed = false;
        }

        // Update is called once per frame
        void Update()
        {
            // Debug.Log("Distance: " + Vector3.Distance(transform.position, woundLocation));
            //if (exitedWound)
            //{
            /*
            if (Vector3.Distance(transform.position, woundLocation) > minimumDistanceAway)
            {
                healed = true;
            }
            */
            //}
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Foreign Object Disposal")
            {
                Debug.Log("In disposal");
                healed = true;
                Destroy(gameObject);
            }
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
}