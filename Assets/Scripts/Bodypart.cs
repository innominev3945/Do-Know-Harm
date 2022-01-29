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
        public void UpdateHealth()
        {
            float loss = 0;
            foreach (Injury injury in injuries)
            {
                loss += (0.1f * injury.GetInjurySeverity());
            }
            health -= loss;
            if (health < 0)
                health = 0;
        }

        public void UpdateInjuries()
        {
            for (int i = 0; i < injuries.Count; i++)
            {
                injuries[i].UpdateState();
            }
            for (int i = 0; i < injuries.Count; i++)
            {
                if (injuries[i].GetInjurySeverity() == 0f)
                {
                    injuries.Remove(injuries[i]);
                    i--;
                }
            }
        }
    }
}