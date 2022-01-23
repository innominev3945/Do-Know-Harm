using UnityEngine;
// Base class for injuries. Child classes inheriting Injury will override the Treat() method
// and implement specific functionality for treating injuries 

namespace InjuryClass
{
    public class Injury
    {
        private float injurySeverity; // Amount of damage an injury will do
        private Vector2 location;  // Location of the injury relative to the BodyPart sprite  

        public Injury(float severity, Vector2 loc)
        {
            injurySeverity = severity;
            location = loc;
        }

        public float GetInjurySeverity() { return injurySeverity; }
        public Vector2 GetLocation() { return location;  }

        // OVERRIDE THIS METHOD WHEN INHERITING        
        public bool Treat()
        {
            return true;
        }
    }
}