using System.Collections;
using System.Collections.Generic;
using ChestReactionScript;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChestCompressionScript
{
    public class Chest_Compression_Script : MonoBehaviour
    {
        [SerializeField] float maxTimeInterval;
        [SerializeField] float minTimeInterval;
        GameObject Chest;
        private float timeElapsed;
        private bool contact;



        // Start is called before the first frame update
        void Start()
        {
            maxTimeInterval = 0.6f;
            minTimeInterval = 0.2f;
            Chest = null;
            timeElapsed = 0f;
            contact = false;
        }

        void Update()
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), 1);
            transform.position.Set(transform.position.x, transform.position.y, -2f);
            timeElapsed += Time.deltaTime;
        }

        public void Compress(InputAction.CallbackContext context) 
        {
            if (context.started)
            {
                if (timeElapsed >= minTimeInterval && timeElapsed <= maxTimeInterval && contact)
                {
                    if (Chest != null)
                    {
                        ChestReaction comp = Chest.GetComponent<ChestReaction>();
                        if (comp != null)
                            comp.Compress();
                    }
                }
                timeElapsed = 0f;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Chest")
            {
                contact = true;
                Chest = collision.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            contact = false;
            Chest = null;
        }

    }
}