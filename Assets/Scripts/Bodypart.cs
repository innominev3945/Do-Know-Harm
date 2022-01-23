using InjuryClass;
using System.Collections.Generic;

namespace BodypartClass
{
    public class Bodypart
    {
        private float health; // Health of the body part, not the entire patient 
        private float severityMultiplier; // Weighted importance of body part relative to health of entire patient (i.e. head is higher than legs)
        private List<Injury> injuries; // Injuries that impact the health of the body part 

        // Constructor 
        public Bodypart(float severity)
        {
            health = 100;
            severityMultiplier = severity;
            injuries = new List<Injury>();
        }

        // Accessors 
        public float GetHealth() { return health; }
        public float GetSeverityMultiplier() { return severityMultiplier; }

        // Add an injury that the BodyPart is dealing with
        public void AddInjury(Injury injury)
        {
            injuries.Add(injury);
        }

        // Cumulative health loss from each Injury (affected by each injury's severity)
        public float UpdateHealth()
        {
            float loss = 0;
            foreach (Injury injury in injuries)
            {
                loss += (0.1f * injury.GetInjurySeverity());
            }
            health -= loss;
            if (health < 0)
                health = 0;
            return health;
        }

        // To do, implement some sort of way to add an event button that will treat a given Injury
        /*
        OnClick()
        {
            if (mouse.location == injury.location)
            {
                if (injury.treat())
                    injuries.remove(injury);
            }
        }
        */
    }
}