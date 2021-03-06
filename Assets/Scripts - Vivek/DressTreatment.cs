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

        private void OnDestroy()
        {
            if (woundDress != null)
                Destroy(woundDress);
        }
        public static DressTreatment MakeDressTreatmentObject(GameObject ob, Injury inj)
        {
            DressTreatment ret = ob.AddComponent<DressTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.woundDress = Instantiate((UnityEngine.Object)Resources.Load("WoundDress"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.woundDress.transform.parent = ob.transform;

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
        }

        public override void StopTreatment() 
        {
            treatmentStarted = false;
            woundDress.SetActive(false);
        }

        public override void ShowInjury()
        {
            woundDress.SetActive(true);
        }

        public override string GetToolName() { return "Bandage"; }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && (woundDress.transform.GetChild(0).tag == "Healed"))
            {
                injury.RemoveTreatment();
                Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
                Destroy(woundDress);
                Destroy(this);
            }
        }
    }
}