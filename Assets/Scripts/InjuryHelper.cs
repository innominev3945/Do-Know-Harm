using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrap wound 8 full rotations to heal 
namespace InjuryHelperClass
{
    public class InjuryHelper : MonoBehaviour
    {
        private bool treated;
        private bool beingTreated;

        private bool selected;
        private float wrappage;

        private Vector2 prev;
        private LineRenderer renderer;

        public InjuryHelper()
        {
            wrappage = 0f;
            treated = false;
            beingTreated = false;
        }

        public void startTreatment()
        {
            beingTreated = true;
        }

        public void stopTreatment()
        {
            beingTreated = false; 
        }

        public bool GetTreated() { return treated; }
        
        public void Start()
        {
            renderer = gameObject.AddComponent<LineRenderer>();
            selected = false; 
        }

        public void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                selected = true;
                prev = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public void OnMouseUp()
        {
            selected = false; 
        }

        // Update is called once per frame
        public void Update()
        {
            if (beingTreated && selected)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                renderer.SetPosition(0, transform.localPosition);
                renderer.SetPosition(1, mousePosition);
                wrappage += Vector2.SignedAngle(prev, mousePosition);
                prev = mousePosition;
                if (Math.Abs(wrappage) >= 2880)
                {
                    treated = true;
                    beingTreated = false;
                    Destroy(renderer);
                }
            }
        }
    }
}
