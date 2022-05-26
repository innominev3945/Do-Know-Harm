using Gauze;
using InjuryClass;
using TreatmentClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GauzeTreatmentClass
{
    public class ApplyGauzeTreatment : Treatment
    {
        private GameObject gauzeHitBoxManager;

        public static ApplyGauzeTreatment MakeApplyGauzeTreatmentObject(GameObject ob, Injury inj)
        {
            ApplyGauzeTreatment ret = ob.AddComponent<ApplyGauzeTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.gauzeHitBoxManager = Instantiate((UnityEngine.Object)Resources.Load("Gauze Hit Box Manager"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.gauzeHitBoxManager.transform.parent = ob.transform;
            ret.gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().addHitBox(ob.transform.position.x, ob.transform.position.y);

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
            gauzeHitBoxManager.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            gauzeHitBoxManager.SetActive(false);
        }

        public override void ShowInjury()
        {
            gauzeHitBoxManager.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().getAllWounds()[0].isAllHitBoxesCovered())
            {
                injury.RemoveTreatment();
                Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
                treatmentStarted = false;
                Destroy(gauzeHitBoxManager);
                Destroy(this);
            }
        }
    }
}
