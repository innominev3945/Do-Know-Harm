using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SyretteClass
{
    public class Syrette_Script : MonoBehaviour
    {
        private Vector2 mousePosition;

        [SerializeField] private bool mousePressed;
        [SerializeField] private Sprite SyretteOpen;
        [SerializeField] private Sprite SyretteClosed;
        private SpriteRenderer sprender;
        private float clickStartTime;
        private bool injecting;
        private GameObject triggeredHitbox;

        // Start is called before the first frame update
        void Start()
        {
            mousePressed = false;
            injecting = false;
            sprender = this.GetComponent<SpriteRenderer>();
            sprender.sprite = SyretteOpen;
        }

        // Update is called once per frame
        void Update()
        {
            if (injecting)
            {
                if (Time.time > (clickStartTime + 2f)) // time to hold syrette in seconds
                {
                    injecting = false;
                    triggeredHitbox.GetComponent<SyretteHitboxScript>().SyretteOnHit();
                    triggeredHitbox = null;
                }
            }
            else
            {
                mousePosition = Mouse.current.position.ReadValue();
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
                transform.position = mousePosition;
            }
        }

        public void SyretteInjection(InputAction.CallbackContext context)
        {
            if (context.started) // && onForeignObject
            {
                mousePressed = true;
                int layerMask = ~(LayerMask.GetMask("Patient"));
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("Patient"));
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "SyretteHitbox")
                    {
                        if (!hit.collider.gameObject.GetComponent<SyretteHitboxScript>().BoostHealthOnCooldown())
                        {
                            triggeredHitbox = hit.collider.gameObject;
                            injecting = true;
                            clickStartTime = Time.time;
                            sprender.sprite = SyretteClosed;
                        }
                        else
                        {
                            Debug.Log("Syrette on cooldown!");
                        }
                    }
                }
            }
            else if (context.canceled)
            {
                injecting = false;
                mousePressed = false;
                //Debug.Log("mouseNotPressed");
                sprender.sprite = SyretteOpen;
            }
        }

        /*private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Foreign Object")
            {
                // Debug.Log("Forceps are on foreign object");
                onForeignObject = true;
                if (mousePressed)
                {
                    currentForeignObject = collision.gameObject;
                    dragForeignObject = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Foreign Object")
            {
                // Debug.Log("Forceps no longer on foreign object");
                onForeignObject = false;
            }
        }*/
    }
}