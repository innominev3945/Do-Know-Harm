using Gauze;
using InjuryClass;
using TreatmentClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GauzeTreatmentClass
{
    public class GauzeTreatment : Treatment
    {
        //private GameObject gauze;
        private GameObject bloodPool;
        private GameObject bleedingWound;

        public static GauzeTreatment MakeGauzeTreatmentObject(GameObject ob, Injury inj)
        {
            GauzeTreatment ret = ob.AddComponent<GauzeTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            //ret.gauze = Instantiate((UnityEngine.Object)Resources.Load("Gauze"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bloodPool = Instantiate((UnityEngine.Object)Resources.Load("BloodPool"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bloodPool.transform.parent = ob.transform;
            ret.bleedingWound = Instantiate((UnityEngine.Object)Resources.Load("BleedingWound"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bleedingWound.transform.parent = ob.transform;

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
            //gauze.SetActive(true);
            bloodPool.SetActive(true);
            bleedingWound.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            //gauze.SetActive(false);
            bloodPool.SetActive(false);
            bleedingWound.SetActive(false);
        }

        public override void ShowInjury()
        {
            bloodPool.SetActive(true);
            bleedingWound.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && bleedingWound.transform.GetChild(0).tag == "Healed") //gauze.GetComponent<Gauze_Script>().GetHealed())
            {
                injury.RemoveTreatment();
                Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
                treatmentStarted = false;
                //gauze.SetActive(false);
                //Destroy(gauze);
                if (bloodPool != null)
                    Destroy(bloodPool);
                Destroy(bleedingWound);
                Destroy(this);
            }
        }
    }
}
