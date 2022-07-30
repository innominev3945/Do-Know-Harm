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
        private float timeInterval; // Time interval for how often a Patient's health will update - obsolete
        // see bodypart script for timeInterval use instead
        private float setTime;
        private Bodypart[] bodyparts;
        private float health; // Health of ENTIRE Patient, weighted via the various Bodyparts 
        private bool clothesOpen;
        private bool debugHealth;
        private bool healthBoostOnCooldown;
        private bool isMale;

        private void OnDestroy()
        {
            foreach (Bodypart bodypart in bodyparts)
                if (bodypart != null)
                    Destroy(bodypart);
        }

        // Constructor
        // Unity, being the helpful engine, doesn't like to have normal Constructors work properly when dealing with
        // Monobehaviour scripts, so instead of creating a Patient object using the Patient() constructor, use 
        // the MakePatientObject() method instead 
        public static Patient MakePatientObject(GameObject ob /*This is the GameObject that Patient is being attached to, so when using it in a driver program, use the "this" keyword*/,
            Bodypart[] parts, float interval, bool isPatientMale)
        {
            Patient ret = ob.AddComponent<Patient>();
            ret.timeInterval = interval;
            ret.setTime = 0f;
            ret.bodyparts = parts; // Takes an array of Bodyparts as the parameter, so Bodyparts can be split and weighted as seen necessary 
            ret.health = 100;
            ret.clothesOpen = false;
            ret.debugHealth = false;
            ret.healthBoostOnCooldown = false;
            ret.isMale = isPatientMale;
            return ret;
        }

        //Accessors
        public float GetHealth() { return health; }
        public Bodypart[] GetBodyparts() { return bodyparts; }

        public bool GetClothesOpen()
        {
            return clothesOpen;
        }

        public void OpenClothes()
        {
            clothesOpen = true;
        }

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
            if (health == 0)
                return false;
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

        public bool isPatientMale()
        {
            return isMale;
        }

        // Update functionality that is called every timeInterval 
        public void Update()
        {
            /*if ((Time.time - nextTime) > timeInterval)
            {
                nextTime = Time.time;
            }
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
                if (debugHealth)
                {
                    Debug.Log("Total: " + total + ", Deducted: " + deducted + ", Result: " + (100 + total - deducted));
                }
                health = 100 + total - deducted;
                if (health < 0)
                    health = 0;
                nextTime += timeInterval;
            }*/
            float total = 0f; // Total HP loss calculated via weighted severity of Bodyparts 
            float deducted = 0f; // Adjustment to keep Patient's health less than or equal to 100 (also allows for the Patient to die if a critically weighted 
                                 // Bodypart dies, such as the head or torso)
            foreach (Bodypart bodypart in bodyparts) // Iterate through all of the Patient's Bodyparts to update health 
            {
                total += bodypart.GetHealth() * bodypart.GetSeverityMultiplier();
                deducted += bodypart.GetSeverityMultiplier() * 100;
            }
            if (debugHealth)
            {
                Debug.Log("Total: " + total + ", Deducted: " + deducted + ", Result: " + (100 + total - deducted));
            }
            health = 100 + total - deducted;
            if (health < 0)
                health = 0;
            if (Time.time > (setTime + 10f))
            {
                healthBoostOnCooldown = false;
            }
        }

        public void ToggleHealthDebug()
        {
            debugHealth = !debugHealth;
        }

        public void DestroyTreatmentObjects()
        {
            foreach (Bodypart bodypart in bodyparts)
                if (bodypart != null)
                    bodypart.DestroyTreatmentObjects();
        }

        public int GetNumInjuries()
        {
            int amt = 0;
            foreach (Bodypart part in bodyparts)
                amt += part.GetNumInjuries();
            return amt;
        }

        public List<string> GetToolNames()
        {
            List<string> ret = new List<string>();
            foreach (Bodypart part in bodyparts)
                ret.AddRange(part.GetToolNames());
            return ret;
        }

        public void BoostHealth(float amount)
        {
            //health += amount;
            /*foreach (Bodypart bodypart in bodyparts)
            {
                bodypart.BoostHealth(amount);
            }*/
            healthBoostOnCooldown = true;
            setTime = Time.time;
            StartCoroutine(RaiseHealth(amount, health + 10));
            /*foreach (Bodypart bodypart in bodyparts)
            {
                bodypart.BoostHealth(amount);
            }*/
        }

        public bool IsHealthBoostOnCooldown()
        {
            return healthBoostOnCooldown;
        }

        IEnumerator RaiseHealth(float amount, float targetHealth)
        {
            float startTime = Time.time;
            if (targetHealth > 99)
                targetHealth = 99;
            while (health <= targetHealth && (Time.time - startTime) < 3f)
            {
                if (health + (amount / 200) < 100)
                {
                    //health += (amount / 200);
                    foreach (Bodypart bodypart in bodyparts)
                    {
                        bodypart.BoostHealth(amount / 200);
                    }
                }
                yield return null;
            }
            //Debug.Log("Boosted Health to " + health);
        }
    }
}