using InjuryClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/*
Prototype of a special inury - Requires wrapping a wound a set number of times to treat an injury 
*/
namespace LacerationClass
{
    public class Laceration : Injury, IPointerDownHandler, IPointerUpHandler // Remember to inherit specialized injuries from the Injury class!
    {
        private bool selected; // Determines whether the mouse is on the Injury or not
        private float wrappage; // Total amount of degrees, signed, that the mouse has gone around the injury
        private float maxWrappage; // Total amount of degrees needed to fully treat the injury 

        private Vector2 prev; // Used for wrapping functionaltiy; determines where the mouse has previously been 
        private LineRenderer renderer; // Temporary LineRenderer object to animate the wrapping motion - replace later with actual assets 

        // Unity's stupid way of making me create a constructor
        public static Laceration MakeLacerationObject(GameObject ob, float severity, Vector2 loc, float maxWrappage)
        {
            Laceration ret = ob.AddComponent<Laceration>();
            ret.injurySeverity = severity;
            ret.location = loc;
            ret.beingTreated = false;
            ret.wrappage = 0f;
            ret.maxWrappage = maxWrappage; 
            return ret; 
        }
        
        public void Start()
        {
            renderer = gameObject.AddComponent<LineRenderer>();
            selected = false; 
        }

        public void OnPointerDown(PointerEventData pointerEventData)
        {
            selected = true;
        }

        public void OnPointerUp(PointerEventData pointerEventData)
        {
            selected = false;
        }

        // Update is called once per frame
        public void Update()
        {
            if (beingTreated && selected)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); // Get the mouse's position
                // Render line from injury to mouse 
                renderer.SetPosition(0, location);
                renderer.SetPosition(1, mousePosition);
                // Determine the signed angle change - if the mouse goes in the opposite direction, then, the wrappage will decrease 
                wrappage += Vector2.SignedAngle(prev, mousePosition);
                prev = mousePosition;
                if (Math.Abs(wrappage) >= Math.Abs(maxWrappage))
                {
                    // When an injury is fully treated, set its severity to 0f and set beingTreated to false 
                    beingTreated = false;
                    injurySeverity = 0f;
                    Destroy(renderer);
                }
            }
        }
    }
}
