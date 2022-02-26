using TreatmentClass;
using InjuryClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace WoundDressingClass
{
    public class WoundDressing : Treatment
    {
        private bool selected; // Determines whether the mouse is on the Injury or not
        private float wrappage; // Total amount of degrees, signed, that the mouse has gone around the injury
        private float maxWrappage; // Total amount of degrees needed to fully treat the injury 
        private GameObject cursor;


        private Vector2 prev; // Used for wrapping functionaltiy; determines where the mouse has previously been 
        private LineRenderer renderer; // Temporary LineRenderer object to animate the wrapping motion - replace later with actual assets 

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
        }

        // Start is called before the first frame update
        void Start()
        {
            renderer = gameObject.AddComponent<LineRenderer>();
            selected = false;
            cursor = GameObject.Find("Cursor");
        }

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
        }
    }
}
