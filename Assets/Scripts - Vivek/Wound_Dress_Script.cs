using System;
using DressReactionScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WoundDressingScript
{
    public class Wound_Dress_Script : MonoBehaviour
    {
        private bool contact;
        private bool selected;
        private bool performed;
        GameObject WoundDress;
        [SerializeField] private Sprite circle;
        [SerializeField] private Sprite wound;

        private Vector2 prev; 
        private LineRenderer renderer; 

        
        // Start is called before the first frame update
        void Start()
        {
            contact = false;
            selected = false;
            WoundDress = null;
            renderer = gameObject.AddComponent<LineRenderer>();
            renderer.material = new Material(Shader.Find("Sprites/Default"));
            renderer.material.color = Color.white;
            renderer.sortingLayerName = "Tool";
            prev = new Vector2(0f, 0f);
            performed = false;
        }


        void Update()
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), 1);
            if (selected && (WoundDress != null))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
                renderer.SetPosition(0, WoundDress.transform.position);
                renderer.SetPosition(1, mousePosition);
                WoundDress.GetComponent<SpriteRenderer>().sprite = circle;


                mousePosition = (new Vector2(WoundDress.transform.position.x, WoundDress.transform.position.y)) - mousePosition;
                WoundDress.GetComponent<DressReaction>().Dress(Vector2.SignedAngle(prev, mousePosition));
                prev = mousePosition;
                if (WoundDress.transform.GetChild(0).tag == "Healed")
                {
                    renderer.SetPosition(0, mousePosition);
                    renderer.SetPosition(1, mousePosition);
                }
            }
        }

        public void Dress(InputAction.CallbackContext context)
        {
            if (context.started && contact) // Calls when mouse button is pressed 
            {
                selected = true;
            }
            else if (context.performed) // Calls when mouse button is released
            {
                // FOR SOME REASON UNITY LIKES TO CALL THIS TWICE IF THE MOUSE BUTTON IS RELEASED SO I GOTTA DO THIS SHADY WORKAROUND
                performed = !performed;
                if (!performed)
                {
                    selected = false;
                    if (WoundDress != null)
                    {
                        WoundDress.GetComponent<DressReaction>().Unwrap();
                        WoundDress.GetComponent<SpriteRenderer>().sprite = wound;
                    }
                    WoundDress = null;
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    renderer.SetPosition(0, mousePosition);
                    renderer.SetPosition(1, mousePosition);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "WoundDress")
            {
                contact = true;
                WoundDress = collision.gameObject;
                wound = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            contact = false;
        }
    }
}
