using Gauze;
using TreatmentClass;
using InjuryClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DressingTreatmentClass
{
    public class DressingTreatment : Treatment
    {
        private GameObject gauzeHitBoxManager;

        private void OnDestroy()
        {
            if (gauzeHitBoxManager != null)
            {
                Destroy(gauzeHitBoxManager);
            }
        }

        public static DressingTreatment MakeDressingTreatmentObject(GameObject ob, Injury inj)
        {
            DressingTreatment ret = ob.AddComponent<DressingTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.gauzeHitBoxManager = Instantiate((UnityEngine.Object)Resources.Load("Gauze Hit Box Manager"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.gauzeHitBoxManager.transform.parent = ob.transform;

            // TODO: change later so that hitbox locations can be decided from patient manager script
            List<float> hitBoxesX = new List<float>();
            List<float> hitBoxesY = new List<float>();
            List<int> woundIDs = new List<int>();

            hitBoxesX.Add(inj.GetLocation().x);
            hitBoxesY.Add(inj.GetLocation().y);
            woundIDs.Add(0);

            hitBoxesX.Add((inj.GetLocation().x) + 1);
            hitBoxesY.Add(inj.GetLocation().y);
            woundIDs.Add(0);

            hitBoxesX.Add((inj.GetLocation().x) - 1);
            hitBoxesY.Add(inj.GetLocation().y);
            woundIDs.Add(0);

            ret.gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().addAllHitBoxes(hitBoxesX, hitBoxesY, woundIDs);

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

            // TODO: disable hitboxes
        }

        public override void ShowInjury()
        {
            gauzeHitBoxManager.SetActive(true);
        }

        public override string GetToolName() { return "Gauze"; }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && gauzeHitBoxManager.GetComponent<Gauze_Hit_Box_Manager_Script>().getAllWounds()[0].isAllHitBoxesCovered())
            {
                injury.RemoveTreatment();
                treatmentStarted = false;
                Destroy(gauzeHitBoxManager);
                Destroy(this);
            }
        }
    }
}

