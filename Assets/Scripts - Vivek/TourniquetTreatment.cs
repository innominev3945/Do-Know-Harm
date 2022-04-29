using InjuryClass;
using TreatmentClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// see: Forceps_script, Foreign_object_script, injury, ForcepsTreatment, PatientFunctionality, Tourniquet, TourniquetTab


namespace TourniquetTreatmentClass
{
    public class TourniquetTreatment : Treatment
    {
        private GameObject tourniquet;
        private GameObject bleedingWound;

        public static TourniquetTreatment MakeTourniquetTreatment(GameObject obj, Injury inj)
        {
            TourniquetTreatment ret = obj.AddComponent<TourniquetTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.tourniquet = Instantiate((UnityEngine.Object)Resources.Load("Tourniquet2"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bleedingWound = Instantiate((UnityEngine.Object)Resources.Load("BleedingWound"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            
            return ret;
        }

        public override void StartTreatment()
        {
            treatmentStarted = true;
            tourniquet.SetActive(true);
            bleedingWound.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            tourniquet.SetActive(false);
            bleedingWound.SetActive(false);
        }

        private void Update()
        {
            if (tourniquet.GetComponent<Tourniquet_Script>().GetHealed())
            {
                injury.RemoveTreatment(); // more stuff needed here?
                Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
                Debug.Log("playsfx");
            }
        }
    }
}

