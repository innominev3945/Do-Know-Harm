using InjuryClass;
using TreatmentClass;
using WoundDressingScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DressTreatmentClass
{
    public class DressTreatment : Treatment
    {
        private GameObject woundDress;
        private GameObject bandage;

        public static DressTreatment MakeDressTreatmentObject(GameObject ob, Injury inj)
        {
            DressTreatment ret = ob.AddComponent<DressTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.woundDress = Instantiate((UnityEngine.Object)Resources.Load("WoundDress"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bandage = Instantiate((UnityEngine.Object)Resources.Load("Bandage"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;

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
            woundDress.SetActive(true);
            bandage.SetActive(true);
        }

        public override void StopTreatment() 
        {
            treatmentStarted = false;
            woundDress.SetActive(false);
            bandage.SetActive(false);
        }

        public override void ShowInjury()
        {
            woundDress.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && bandage.GetComponent<Wound_Dress_Script>().GetHealed())
            {
                injury.RemoveTreatment();
                Destroy(woundDress);
                bandage.SetActive(false);
                Destroy(bandage);
                Destroy(this);
            }
        }
    }
}