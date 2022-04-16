using TreatmentClass;
using InjuryClass;
using ChestCompressionScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestTreatmentClass
{
    public class ChestTreatment : Treatment
    {
        public GameObject chest;

        public static ChestTreatment MakeChestTreatmentObject(GameObject ob, Injury inj) 
        {
            ChestTreatment ret = ob.AddComponent<ChestTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.chest = Instantiate((UnityEngine.Object)Resources.Load("Chest"), new Vector3(ret.injury.GetLocation().x, ret.injury.GetLocation().y, 1f), Quaternion.identity) as GameObject;

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
            chest.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            chest.SetActive(false);
        }

        public override void ShowInjury()
        {
            chest.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && chest.transform.GetChild(0).tag == "Healed")
            {
                injury.RemoveTreatment();
                Destroy(chest);
                Destroy(this);
            }
        }
    }
}