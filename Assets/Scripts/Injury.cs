using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Represents a general, abstract Injury. Specific Injuries (such as bleedings, burn wounds, etc.) are to be derived from the
Injury class and then specialized. 
*/
namespace InjuryClass
{
    public class Injury : MonoBehaviour
    {
        protected float injurySeverity; 
        protected Vector2 location; // Represents the location of the Injury (for treatment and animation purposes)
        protected bool beingTreated; // Determines if an injury is currently being treated by the Player 

        // Constructor 
        // Unity, being the helpful engine, doesn't like to have normal Constructors work properly when dealing with
        // Monobehaviour scripts, so instead of creating a Patient object using the Patient() constructor, use 
        // the MakePatientObject() method instead 
        public static Injury MakeInjuryObject(GameObject ob, float severity, Vector2 loc)
        {
            Injury ret = ob.AddComponent<Injury>();
            ret.injurySeverity = severity;
            ret.location = loc;
            ret.beingTreated = false;
            return ret;
        }

        // Acessors 
        public float GetInjurySeverity() { return injurySeverity; }
        public Vector2 GetLocation() { return location;  }

        public void Treat() { beingTreated = true; } // Starts a treatment
        public void AbortTreatment() { beingTreated = false; } // Ends a treatment

        // Other functionality, such as the specific treatment of injuries, is expected to be extended in classes derived from Injury 
    }
}