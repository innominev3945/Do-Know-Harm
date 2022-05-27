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
        private bool beingTreated; // Determines if an injury is currently being treated by the Player 
        private Queue<Treatment> treatments; // Queue of treatments - as soon as one treatment is finished, the next one in priority is to be started 
        private string injuryName;

        ~Injury()
        {
            foreach (Treatment treatment in treatments)
                if (treatment != null)
                    GameObject.Destroy(treatment);
        }

        public void DestroyTreatmentObjects()
        {
            foreach (Treatment treatment in treatments)
                if (treatment != null)
                    GameObject.Destroy(treatment);
            treatments.Clear();
        }

        public Injury(float severity, Vector2 loc, string name)
        {
            injurySeverity = severity;
            location = loc;
            beingTreated = false;
            treatments = new Queue<Treatment>();
            injuryName = name;
        }

        // Acessors 
        public float GetInjurySeverity()
        {
            if (treatments.Count != 0)
                return injurySeverity;
            return 0f; // If the Queue of treatments is empty, then the injury has been fully treated
        }

        public Vector2 GetLocation() { return location; }
        public bool GetBeingTreated() { return beingTreated; }

        public bool GetHealed() { return (treatments.Count == 0); }

        public string GetName() { return injuryName; }

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
            if (treatments.Count != 0)
            {
                beingTreated = false;
                treatments.Peek().StopTreatment();
            }
        }

        public void AddTreatment(Treatment treatment) 
        {
            treatment.StopTreatment();
            treatments.Enqueue(treatment);
        }

        public void RemoveTreatment()
        {
            if (beingTreated && treatments.Count != 0)
            {
                treatments.Dequeue();
                if (treatments.Count != 0)
                    treatments.Peek().StartTreatment();
                else
                    beingTreated = false;
                Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
            }
        }

        public string GetTreatableTool()
        {
            return treatments.Peek().GetToolName();
        }

    }
}