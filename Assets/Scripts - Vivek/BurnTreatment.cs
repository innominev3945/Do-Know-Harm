using TreatmentClass;
using InjuryClass;
using ThermalOintmentClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurnTreatmentClass
{
    public class BurnTreatment : Treatment
    {
        //private GameObject thermalOintment;
        private GameObject burnWound;
        private GameObject trigger1;
        private GameObject trigger2;

        private void OnDestroy()
        {
            if (burnWound != null)
                Destroy(burnWound);
            if (trigger1 != null)
                Destroy(trigger1);
            if (trigger2 != null)
                Destroy(trigger2);
        }

        public static BurnTreatment MakeBurnTreatmentObject(GameObject ob, Injury inj)
        {
            BurnTreatment ret = ob.AddComponent<BurnTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            //ret.thermalOintment = Instantiate((UnityEngine.Object)Resources.Load("ThermalOintment"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.burnWound = Instantiate((UnityEngine.Object)Resources.Load("BurnUpdated"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.trigger1 = ret.burnWound.transform.GetChild(1).gameObject;
            ret.trigger2 = ret.burnWound.transform.GetChild(2).gameObject;
            //ret.trigger1 = Instantiate((UnityEngine.Object)Resources.Load("Trigger1"), new Vector2(ret.injury.GetLocation().x - 2f, ret.injury.GetLocation().y), Quaternion.identity) as GameObject;
            //ret.trigger2 = Instantiate((UnityEngine.Object)Resources.Load("Trigger2"), new Vector2(ret.injury.GetLocation().x + 2f, ret.injury.GetLocation().y), Quaternion.identity) as GameObject;
            ret.burnWound.transform.parent = ob.transform;
            

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
            //thermalOintment.SetActive(true);
            burnWound.SetActive(true);
            trigger1.SetActive(true);
            trigger2.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            //thermalOintment.SetActive(false);
            burnWound.SetActive(false);
            trigger1.SetActive(false);
            trigger2.SetActive(false);
        }

        public override void ShowInjury()
        {
            burnWound.SetActive(true);
        }

        public override string GetToolName() { return "Thermal Ointment"; }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && (trigger1.transform.GetChild(0).tag == "Healed" || trigger2.transform.GetChild(0).tag == "Healed"))//thermalOintment.GetComponent<Thermal_Ointment_Script>().GetHealed())
            {
                injury.RemoveTreatment();
                treatmentStarted = false;
                //thermalOintment.SetActive(false);
                //Destroy(thermalOintment);
                Destroy(burnWound);
                Destroy(trigger1);
                Destroy(trigger2);
                Destroy(this);
            }
        }
    }
}