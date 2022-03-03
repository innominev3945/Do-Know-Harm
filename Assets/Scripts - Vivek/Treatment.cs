using InjuryClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents an abstract treatment for an injury 
namespace TreatmentClass
{
    public class Treatment : MonoBehaviour
    {
        protected bool treatmentStarted;
        protected bool vitalSpike;
        protected Injury injury;

        public bool GetTreatmentStarted() { return treatmentStarted; }
        public bool GetVitalSpike() { return vitalSpike; } // If making a mistake in a treatment causes the patient to take more health, this boolean should be set to true

        public virtual void StartTreatment() { treatmentStarted = true; }
        public virtual void StopTreatment() { treatmentStarted = false; }
    }
}
