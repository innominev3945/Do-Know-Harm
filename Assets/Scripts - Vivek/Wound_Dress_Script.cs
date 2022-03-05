using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WoundDressingScript
{
    public class Wound_Dress_Script : MonoBehaviour
    {
        [SerializeField] float wrappage; // Total amount of degrees, signed, that the mouse has gone around the injury
        [SerializeField] float maxWrappage; // Total amount of degrees needed to fully treat the injury 
        private bool contact;
        private bool selected;
        private bool performed;

        private Vector2 woundLocation;
        private Vector2 prev; // Used for wrapping functionaltiy; determines where the mouse has previously been 
        private LineRenderer renderer; // Temporary LineRenderer object to animate the wrapping motion - replace later with actual assets 

        private bool healed;

        public bool GetHealed() { return healed; }

        /*
        // Unity's stupid way of making me create a constructor
        public static WoundDressing MakeWoundDressingObject(GameObject ob, Injury injury, float maxWrappage)
        {
            WoundDressing ret = ob.AddComponent<WoundDressing>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = injury;
            ret.wrappage = 0f;
            ret.maxWrappage = maxWrappage;
            return ret;
        }

        public override void StopTreatment()
        {
            renderer.SetPosition(0, new Vector2(0,0));
            renderer.SetPosition(1, new Vector2(0, 0));
            treatmentStarted = false;
        }*/

        // Start is called before the first frame update
        void Start()
        {
            wrappage = 0;
            maxWrappage = 2880;
            contact = false;
            selected = false;
            renderer = gameObject.AddComponent<LineRenderer>();
            woundLocation = new Vector2(0f, 0f);
            prev = new Vector2(0f, 0f);
            performed = false;
            healed = false;
        }

        /*
        // Update is called once per frame
        void Update()
        {
            if (cursor.GetComponent<NewCursor>().getSelected() && injury.IsSelected(cursor.transform.position))
                selected = true;
            else if (!cursor.GetComponent<NewCursor>().getSelected())
                selected = false;

            if (treatmentStarted && selected)
            {
                Vector2 mousePosition = cursor.transform.position; // Get the mouse's position
                // Render line from injury to mouse 
                renderer.SetPosition(0, injury.GetLocation());
                renderer.SetPosition(1, mousePosition);

                // Determine the signed angle change - if the mouse goes in the opposite direction, then, the wrappage will decrease 
                wrappage += Vector2.SignedAngle(prev, mousePosition);
                prev = mousePosition;
                if (Math.Abs(wrappage) >= Math.Abs(maxWrappage))
                {
                    // When an injury is fully treated, set its severity to 0f and set beingTreated to false 
                    vitalSpike = false;
                    Destroy(renderer);
                    injury.RemoveTreatment();
                    Destroy(this);
                }
            }
        }*/

        void Update()
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), 1);
            if (selected && !healed)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); // Get the mouse's position
                // Render line from injury to mouse 
                renderer.SetPosition(0, woundLocation);
                renderer.SetPosition(1, mousePosition);

                // Determine the signed angle change - if the mouse goes in the opposite direction, then, the wrappage will decrease 
                wrappage += Vector2.SignedAngle(prev, mousePosition);
                prev = mousePosition;
                if (Math.Abs(wrappage) >= Math.Abs(maxWrappage))
                {
                    renderer.SetPosition(0, mousePosition);
                    renderer.SetPosition(1, mousePosition);
                    Destroy(renderer);
                    healed = true;
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
                    selected = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "WoundDress")
            {
                contact = true;
                woundLocation = collision.gameObject.transform.position;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "WoundDress")
                contact = false;
        }
    }
}
