using InjuryClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Represents a Patient's Bodypart which, for all practical purposes, is just a collection of injuries specific to the given
Bodypart. A Bodypart's health is updated to reduce over time while injuries are left untreated.
*/
namespace BodypartClass
{
    public class Bodypart : MonoBehaviour
    {
        private Vector2 location;
        private float timeInterval;
        private float nextTime;
        private float health; // Health of the body part, not the entire patient 
        private float severityMultiplier; // Weighted importance of body part relative to health of entire patient (i.e. head is higher than legs)
        private List<Injury> injuries; // Injuries that impact the health of the body part 
        private bool damagePause;


        // Constructor 
        // Unity, being the helpful engine, doesn't like to have normal Constructors work properly when dealing with
        // Monobehaviour scripts, so instead of creating a Patient object using the Patient() constructor, use 
        // the MakePatientObject() method instead 
        public static Bodypart MakeBodypartObject(GameObject ob /*Make sure this parameter is this.gameObject when calling the constructor*/, float severity, float interval, Vector2 loc)
        {
            Bodypart ret = ob.AddComponent<Bodypart>();
            ret.location = loc;
            ret.timeInterval = interval;
            ret.nextTime = 0f;

            ret.health = 100;
            ret.severityMultiplier = severity;
            ret.injuries = new List<Injury>();
            ret.damagePause = false;
            return ret;
        }

        // Accessors 
        public float GetHealth() { return health; }
        public float GetSeverityMultiplier() { return severityMultiplier; }

        public Vector2 GetLocation() { return location; }

        public void DestroyTreatmentObjects()
        {
            foreach (Injury injury in injuries)
                if (injury != null)
                    injury.DestroyTreatmentObjects();
        }
        public bool GetHealed() 
        {
            foreach (Injury injury in injuries)
            {
                if (!injury.GetHealed())
                    return false;
            }
            return true;
        }

        // Add an injury that the BodyPart is dealing with
        public void AddInjury(Injury injury) 
        { 
            injuries.Add(injury);
        }

        public void TreatInjuries()
        {
            foreach (Injury injury in injuries)
                injury.Treat();
        }

        public void StopInjuries()
        {
            foreach (Injury injury in injuries)
                injury.AbortTreatment();
        }

        // Stops the body part for taking damage until "unpause"
        public void PauseDamage()
        {
            damagePause = true;
        }

        public void UnpauseDamage()
        {
            damagePause = false;
        }

        public int GetNumInjuries()
        {
            return injuries.Count;
        }

        public List<string> GetInjuryNames()
        {
            List<string> ret = new List<string>();
            foreach (Injury inj in injuries)
            {
                ret.Add(inj.GetName());
            }
            return ret;
        }

        public List<string> GetToolNames()
        {
            List<string> ret = new List<string>();
            foreach (Injury inj in injuries)
                ret.Add(inj.GetTreatableTool());
            return ret;
        }

        // Update functionality that is called every timeInterval
        void Update()
        {
            if ((Time.time - nextTime) > timeInterval)
            {
                nextTime = Time.time;
            }
            if (Time.time >= nextTime)
            {
                if (injuries.Count != 0)
                {
                    float loss = 0;
                    // The loss of health of a Bodypart is dependent on all of its given injuries 
                    foreach (Injury injury in injuries)
                    {
                        loss += (injury.GetInjurySeverity());
                    }
                    if (!damagePause)
                        health -= loss;
                    if (health < 0)
                        health = 0;

                    // Cycle through the Injuries List and remove any that are treated (having a severity of 0)
                    for (int i = 0; i < injuries.Count; i++)
                    {
                        if (injuries[i].GetInjurySeverity() == 0)
                        {
                            injuries.RemoveAt(i);
                            i--;
                        }
                    }
                }
                nextTime += timeInterval; 
            }
        }

        public void BoostHealth(float amount)
        {
            health += amount;
            if (health > 100)
            {
                health = 100;
            }
        }

        /*IEnumerator RaiseHealth(float amount)
        {
            for (int i = 0; i < 100; i++)
            {
                if (health + (amount / 100) < 100)
                {
                    health += (amount / 100);
                }
            }
            //Debug.Log("Bodypart boosted to " + health);
            yield return null;
        }*/

    }
}