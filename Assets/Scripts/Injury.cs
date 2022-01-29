using InjuryHelperClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InjuryClass
{
    public class Injury
    {
        private float injurySeverity; 
        private Vector2 location;
        private InjuryHelper helper;

        public Injury(float severity, Vector2 loc, InjuryHelper hel)
        {
            injurySeverity = severity;
            location = loc;
            helper = hel;
        }

        public float GetInjurySeverity() { return injurySeverity; }
        public Vector2 GetLocation() { return location;  }

        public void Treat()
        {
            helper.startTreatment();
        }

        public void AbortTreatment()
        {
            helper.stopTreatment();
        }

        public void UpdateState()
        {
            if (helper.GetTreated())
                injurySeverity = 0f;
        }
    }
}