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

        private void OnDestroy()
        {
            if (bloodPool != null)
                Destroy(bloodPool);
            if (bleedingWound != null)
                Destroy(bleedingWound);
        }
        public static GauzeTreatment MakeGauzeTreatmentObject(GameObject ob, Injury inj, float rotation)
        {
            GauzeTreatment ret = ob.AddComponent<GauzeTreatment>();
            Quaternion q = Quaternion.Euler(0, 0, rotation);
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            //ret.gauze = Instantiate((UnityEngine.Object)Resources.Load("Gauze"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bloodPool = Instantiate((UnityEngine.Object)Resources.Load("BloodPool"), ret.injury.GetLocation(), q) as GameObject;
            ret.bloodPool.transform.parent = ob.transform;
            ret.bleedingWound = Instantiate((UnityEngine.Object)Resources.Load("BleedingWound"), ret.injury.GetLocation(), q) as GameObject;
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
            Debug.Log("Start Gauze Treatment");

            treatmentStarted = true;
            //gauze.SetActive(true);
            if (bloodPool != null)
                bloodPool.SetActive(true);
            if (bleedingWound != null)
            {
                bleedingWound.SetActive(true);
                bleedingWound.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            //gauze.SetActive(false);
            if (bloodPool != null)
                bloodPool.SetActive(false);
            if (bleedingWound != null)
                bleedingWound.SetActive(false);
        }

        public override void ShowInjury()
        {
            if (bloodPool != null)
                bloodPool.SetActive(true);
            if (bleedingWound != null)
                bleedingWound.SetActive(true);
        }

        public override string GetToolName() { return "Gauze";  }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && bloodPool.transform.GetChild(0).tag == "Healed")
            {
                if (bleedingWound != null)
                {
                    bleedingWound.GetComponent<BoxCollider2D>().enabled = true;
                }
            }

            if (treatmentStarted && bleedingWound.transform.GetChild(0).tag == "Healed") //gauze.GetComponent<Gauze_Script>().GetHealed())
            {
                Debug.Log("Bloodpool and bleeding wound removed");

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
