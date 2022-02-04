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
        private float timeInterval;
        private float nextTime;
        private float health; // Health of the body part, not the entire patient 
        private float severityMultiplier; // Weighted importance of body part relative to health of entire patient (i.e. head is higher than legs)
        private List<Injury> injuries; // Injuries that impact the health of the body part 
        // The injuries array is polymorphic; it will contain injuries derived from the Injury base class 

        // Constructor 
        // Unity, being the helpful engine, doesn't like to have normal Constructors work properly when dealing with
        // Monobehaviour scripts, so instead of creating a Patient object using the Patient() constructor, use 
        // the MakePatientObject() method instead 
        public static Bodypart MakeBodypartObject(GameObject ob /*Make sure this parameter is this.gameObject when calling the constructor*/, float severity, float interval)
        {
            Bodypart ret = ob.AddComponent<Bodypart>();
            ret.timeInterval = interval;
            ret.nextTime = 0f;

            ret.health = 100;
            ret.severityMultiplier = severity;
            ret.injuries = new List<Injury>();
            return ret;
        }

        // Accessors 
        public float GetHealth() { return health; }
        public float GetSeverityMultiplier() { return severityMultiplier; }

        // Add an injury that the BodyPart is dealing with
        public void AddInjury(Injury injury) { injuries.Add(injury); }

        // Update functionality that is called every timeInterval
        void Update()
        {
            if (Time.time >= nextTime)
            {
                float loss = 0;
                // The loss of health of a Bodypart is dependent on all of its given injuries 
                foreach (Injury injury in injuries)
                {
                    loss += (injury.GetInjurySeverity());
                }
                health -= loss;
                if (health < 0)
                    health = 0;
                nextTime += timeInterval; 
            }
        }
    }
}