using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SyretteClass
{
    public class Syrette_Script : MonoBehaviour
    {
        private Vector3 mousePosition;

        [SerializeField] private bool mousePressed;
        private bool onForeignObject;
        private bool dragForeignObject;
        GameObject currentForeignObject;
        [SerializeField] private Sprite SyretteOpen;
        [SerializeField] private Sprite SyretteClosed;
        private SpriteRenderer sprender;

        // Start is called before the first frame update
        void Start()
        {
            mousePressed = false;
            onForeignObject = false;
            dragForeignObject = false;
            currentForeignObject = null;
            sprender = this.GetComponent<SpriteRenderer>();
            sprender.sprite = SyretteOpen;
        }

        // Update is called once per frame
        void Update()
        {
            mousePosition = Mouse.current.position.ReadValue();
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, 1);


            if (currentForeignObject != null && dragForeignObject)
            {
                currentForeignObject.transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
            }
        }

        public void RemoveObject(InputAction.CallbackContext context)
        {
            if (context.started) // && onForeignObject
            {
                mousePressed = true;
                Debug.Log("mousePressed");
                sprender.sprite = SyretteClosed;
            }
            else if (context.canceled)
            {
                mousePressed = false;
                Debug.Log("mouseNotPressed");
                dragForeignObject = false;
                currentForeignObject = null;
                sprender.sprite = SyretteOpen;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
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
        }
    }
}