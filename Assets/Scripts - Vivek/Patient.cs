using BodypartClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Represents a Patient which, for all practical purposes, is merely just a collection of Bodyparts. 
The overall health of a Patient is determined by the weighted health of its Bodyparts - some Bodyparts 
are weighted more than others and it is possible for a Patient to still reach zero health if some Bodyparts 
are alive (i.e. if the Bodypart representing the head of a Patient reaches zero health, its weighted health
will contribute to the Patient dying).
*/
namespace PatientClass
{
    public class Patient : MonoBehaviour
    {
        private float timeInterval; // Time interval for how often a Patient's health will update 
        private float nextTime;
        private Bodypart[] bodyparts;
        private float health; // Health of ENTIRE Patient, weighted via the various Bodyparts 

        // Constructor
        // Unity, being the helpful engine, doesn't like to have normal Constructors work properly when dealing with
        // Monobehaviour scripts, so instead of creating a Patient object using the Patient() constructor, use 
        // the MakePatientObject() method instead 
        public static Patient MakePatientObject(GameObject ob /*This is the GameObject that Patient is being attached to, so when using it in a driver program, use the "this" keyword*/,
            Bodypart[] parts, float interval)
        {
            Patient ret = ob.AddComponent<Patient>();
            ret.timeInterval = interval;
            ret.nextTime = 0f;
            ret.bodyparts = parts; // Takes an array of Bodyparts as the parameter, so Bodyparts can be split and weighted as seen necessary 
            ret.health = 100;
            return ret;
        }

        //Accessors
        public float GetHealth() { return health; }
        public Bodypart[] GetBodyparts() { return bodyparts; }

        public void AbortTreatments()
        {
            foreach (Bodypart bodypart in bodyparts)
            {
                bodypart.StopInjuries();
            }
        }

        public void StartTreatments()
        {
            foreach (Bodypart bodypart in bodyparts)
            {
                bodypart.TreatInjuries();
            }
        }

        public bool GetHealed()
        {
            foreach (Bodypart bodypart in bodyparts)
            {
                if (!bodypart.GetHealed())
                    return false;
            }
            return true;
        }

        public void PauseDamage()
        {
            foreach (Bodypart part in bodyparts)
            {
                part.PauseDamage();
            }
        }

        public void UnpauseDamage()
        {
            foreach (Bodypart part in bodyparts)
            {
                part.UnpauseDamage();
            }
        }

        // Update functionality that is called every timeInterval 
        public void Update()
        {
            if (Time.time >= nextTime)
            {
                float total = 0f; // Total HP loss calculated via weighted severity of Bodyparts 
                float deducted = 0f; // Adjustment to keep Patient's health less than or equal to 100 (also allows for the Patient to die if a critically weighted 
                // Bodypart dies, such as the head or torso)
                foreach (Bodypart bodypart in bodyparts) // Iterate through all of the Patient's Bodyparts to update health 
                {
                    total += bodypart.GetHealth() * bodypart.GetSeverityMultiplier();
                    deducted += bodypart.GetSeverityMultiplier() * 100;
                }
                health = 100 + total - deducted;
                if (health < 0)
                    health = 0;
                nextTime += timeInterval;
            }
        }
    }
}