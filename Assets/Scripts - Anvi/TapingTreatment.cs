using TreatmentClass;
using InjuryClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapingTreatmentClass
{
    public class TapingTreatment : Treatment
    {
        private GameObject hitBox1;
        private GameObject hitBox2;

        private void OnDestroy()
        {
            if (hitBox1 != null)
                Destroy(hitBox1);
            if (hitBox2 != null)
                Destroy(hitBox2);
        }

        public static TapingTreatment MakeTapingTreatmentObject(GameObject ob, Injury inj)
        {
            TapingTreatment ret = ob.AddComponent<TapingTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.hitBox1 = Instantiate((UnityEngine.Object)Resources.Load("Hit Box (Tape) 1"), new Vector3(ret.injury.GetLocation().x - 2f, ret.injury.GetLocation().y, 0), Quaternion.identity) as GameObject;
            ret.hitBox2 = Instantiate((UnityEngine.Object)Resources.Load("Hit Box (Tape) 2"), new Vector3(ret.injury.GetLocation().x + 2f, ret.injury.GetLocation().y, 0), Quaternion.identity) as GameObject;

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
            if (hitBox1 != null)
                hitBox1.SetActive(true);
            if (hitBox2 != null)
                hitBox2.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            if (hitBox1 != null)
                hitBox1.SetActive(false);
            if (hitBox2 != null)
                hitBox2.SetActive(false);
        }

        public override void ShowInjury()
        {
            if (hitBox1 != null)
                hitBox1.SetActive(true);
            if (hitBox2 != null)
                hitBox2.SetActive(true);
        }

        public override string GetToolName()
        {
            return "Tape Pivot (Revised)";
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && hitBox1.tag == "Healed" && hitBox2.tag == "Healed")
            {
                injury.RemoveTreatment();
                treatmentStarted = false;
                Destroy(hitBox1);
                Destroy(hitBox2);
                Destroy(this);
            }
        }
    }
}
