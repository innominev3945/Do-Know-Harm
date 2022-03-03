using InjuryClass;
using TreatmentClass;
using ForeignObjectClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForcepsTreatmentClass
{
    public class ForcepsTreatment : Treatment
    {
        private GameObject forceps;
        private GameObject foreignObject;
        private GameObject bleedingWound;

        public static ForcepsTreatment MakeForcepsTreatmentObject(GameObject ob, Injury inj)
        {
            ForcepsTreatment ret = ob.AddComponent<ForcepsTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.forceps = Instantiate((UnityEngine.Object)Resources.Load("Forceps"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bleedingWound = Instantiate((UnityEngine.Object)Resources.Load("BleedingWound"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.foreignObject = Instantiate((UnityEngine.Object)Resources.Load("ForeignObject"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.foreignObject.GetComponent<Foreign_Object_Script>().SetWound(ret.bleedingWound);
            return ret;
        }

        // Start is called before the first frame update
        void Start()
        {
            StopTreatment();
        }

        public override void StartTreatment()
        {
            treatmentStarted = true;
            forceps.SetActive(true);
            foreignObject.SetActive(true);
            bleedingWound.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            forceps.SetActive(false);
            foreignObject.SetActive(false);
            bleedingWound.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && foreignObject.GetComponent<Foreign_Object_Script>().GetHealed())
            {
                injury.RemoveTreatment();
                forceps.SetActive(false);
                Destroy(forceps);
                foreignObject.SetActive(false);
                Destroy(foreignObject);
                Destroy(bleedingWound);
                Destroy(this);
            }
        }
    }
}