using TreatmentClass; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Represents an injury. An injury is defined in terms of the treatments that are required to treat it. 
*/
namespace InjuryClass
{
    public class Injury
    {
        private float injurySeverity; 
        private Vector2 location; // Represents the location of the Injury (for treatment and animation purposes)
        private float radius; // An injury is represented as a circle of the given radius centered at location
        private bool beingTreated; // Determines if an injury is currently being treated by the Player 
        private Queue<Treatment> treatments; // Queue of treatments - as soon as one treatment is finished, the next one in priority is to be started 

        public Injury(float severity, Vector2 loc, float rad)
        {
            injurySeverity = severity;
            location = loc;
            radius = rad;
            beingTreated = false;
            treatments = new Queue<Treatment>();
        }

        // Acessors 
        public float GetInjurySeverity() 
        {
            if (treatments.Count != 0)
                return injurySeverity;
            return 0f; // If the Queue of treatments is empty, then the injury has been fully treated
        }

        public Vector2 GetLocation() { return location;  }
        public bool GetBeingTreated() { return beingTreated; }
        public float GetRadius() { return radius; }

        public bool IsSelected(Vector2 selectionPosition) // Check if a Vector2 (i.e. mouse cursor position) is selecting an Injury by checking if the selection is within the bounds of the injury's circle 
        {
            if ((location - selectionPosition).magnitude <= radius)
                return true;
            return false;
        }

        // Starts treating an Injury by activating the Treatment of the topmost item in the Queue 
        public void Treat()
        {
            if (treatments.Count != 0)
            {
                beingTreated = true;
                treatments.Peek().StartTreatment();
            }
        }

        // Holds the treatment of an Injury by stopping the Treatment of the topmost item in the Queue (this still preserves all progress in treating the injury) 
        public void AbortTreatment() 
        { 
            beingTreated = false;
            treatments.Peek().StopTreatment();
        } // Ends a treatment

        public void AddTreatment(Treatment treatment) { treatments.Enqueue(treatment); }
        public void RemoveTreatment() 
        {
            if (treatments.Count != 0)
            {
                treatments.Dequeue();
                if (treatments.Count != 0)
                    treatments.Peek().StartTreatment();
                else
                    beingTreated = false;
            }
        }
    }
}