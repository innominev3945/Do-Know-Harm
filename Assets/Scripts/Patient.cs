using BodypartClass;

namespace PatientClass
{
    public class Patient
    {
        private Bodypart[] bodyparts; // Array of Bodyparts = {Legs, Arms, Torso, Head}
        private float health;

        // Constructor
        public Patient()
        {
            bodyparts = new Bodypart[4];
            bodyparts[0] = new Bodypart(0.2f); // Arms
            bodyparts[1] = new Bodypart(0.2f); // Legs
            bodyparts[2] = new Bodypart(0.6f); // Torso 
            bodyparts[3] = new Bodypart(0.8f); // Head
            health = 100;
        }

        public float GetHealth() { return health; }

        // The cumulative health of all Bodyparts is 180, but the effective health of the Patient is this subtracted by 80 (to have it be 100); 
        // this ensures that vital Bodyparts are prioritized 
        public float UpdateHealth()
        {
            float total = 0f;
            foreach (Bodypart bodypart in bodyparts)
            {
                total += bodypart.GetHealth() * bodypart.GetSeverityMultiplier();
            }
            health = total - 80;
            if (health < 0)
                health = 0;
            return health;
        }

        public Bodypart[] GetBodyparts()
        {
            return bodyparts; 
        }
    }
}