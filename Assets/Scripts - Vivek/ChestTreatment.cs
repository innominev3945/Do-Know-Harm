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
        public GameObject compressor;

        public static ChestTreatment MakeChestTreatmentObject(GameObject ob, Injury inj) 
        {
            ChestTreatment ret = ob.AddComponent<ChestTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.chest = Instantiate((UnityEngine.Object)Resources.Load("Chest"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.compressor = Instantiate((UnityEngine.Object)Resources.Load("Compressor"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;

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
            compressor.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            chest.SetActive(false);
            compressor.SetActive(false);
        }

        public override void ShowInjury()
        {
            chest.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && compressor.GetComponent<Chest_Compression_Script>().GetHealed())
            {
                injury.RemoveTreatment();
                compressor.SetActive(false);
                Destroy(compressor);
                Destroy(chest);
                Destroy(this);
            }
        }
    }
}