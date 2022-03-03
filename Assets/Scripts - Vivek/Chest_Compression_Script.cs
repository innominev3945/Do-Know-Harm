using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChestCompressionScript
{
    public class Chest_Compression_Script : MonoBehaviour
    {
        [SerializeField] int maxCompressions;
        private int numCompressions;
        [SerializeField] float maxTimeInterval;
        [SerializeField] float minTimeInterval;
        private float timeElapsed;
        private bool healed;
        private bool contact;

        public bool GetHealed() { return healed; }


        // Start is called before the first frame update
        void Start()
        {
            maxCompressions = 30;
            numCompressions = 0;
            maxTimeInterval = 1f;
            minTimeInterval = 0.5f;
            timeElapsed = 0f;
            healed = false;
            contact = false;
        }

        // Update is called once per frame

        /*void Update()
        {
            if (treatmentStarted && cursor.GetComponent<NewCursor>().getSelected())
            {
                if (injury.IsSelected(cursor.transform.position))
                {
                    if (timeElapsed >= minTimeInterval && timeElapsed <= maxTimeInterval)
                    {
                        numCompressions++;
                        if (numCompressions >= maxCompressions)
                        {
                            vitalSpike = false;
                            injury.RemoveTreatment();
                            Destroy(this);
                        }
                    }
                    timeElapsed = 0;
                }
                else
                    timeElapsed += Time.deltaTime;
                Debug.Log(numCompressions);
            }
            else
                timeElapsed += Time.deltaTime;
        }
        */

        void Update()
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), 1);

            timeElapsed += Time.deltaTime;
        }

        public void Compress(InputAction.CallbackContext context) 
        {
            if (context.started)
            {
                Debug.Log(numCompressions);
                if (timeElapsed >= minTimeInterval && timeElapsed <= maxTimeInterval && contact)
                    numCompressions++;
                if (numCompressions >= maxCompressions)
                    healed = true;
                timeElapsed = 0f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Chest")
                contact = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            contact = false;
        }
    }
}