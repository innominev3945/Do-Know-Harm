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

        public void OnDestroy()
        {
            if (tourniquet != null)
                Destroy(tourniquet);
            if (bleedingWound != null)
                Destroy(bleedingWound);
        }
        public static TourniquetTreatment MakeTourniquetTreatmentObject(GameObject obj, Injury inj)
        {
            TourniquetTreatment ret = obj.AddComponent<TourniquetTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.tourniquet = Instantiate((UnityEngine.Object)Resources.Load("Tourniquet"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;

            ret.bleedingWound = Instantiate((UnityEngine.Object)Resources.Load("BleedingWound"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.tourniquet.transform.parent = obj.transform;
            ret.tourniquet.GetComponent<Tourniquet_Script>().setWoundObject(ret.bleedingWound);

            ret.bleedingWound.transform.parent = obj.transform;
            
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
                Destroy(bleedingWound);
                Destroy(this);
            }
        }
    }
}

