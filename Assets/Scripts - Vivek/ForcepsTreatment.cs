using InjuryClass;
using TreatmentClass;
using ForeignObjectClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MouseReactionScript;

namespace ForcepsTreatmentClass
{
    public class ForcepsTreatment : Treatment
    {
        //private GameObject forceps;
        private GameObject foreignObject;
        private GameObject bleedingWound;
        private GameObject disposal;

        public static ForcepsTreatment MakeForcepsTreatmentObject(GameObject ob, Injury inj)
        {
            ForcepsTreatment ret = ob.AddComponent<ForcepsTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;


            //ret.forceps = Instantiate((UnityEngine.Object)Resources.Load("Forceps"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bleedingWound = Instantiate((UnityEngine.Object)Resources.Load("BleedingWound"), new Vector3(ret.injury.GetLocation().x, ret.injury.GetLocation().y, 0), Quaternion.identity) as GameObject;
            ret.bleedingWound.transform.parent = ob.transform;
            ret.foreignObject = Instantiate((UnityEngine.Object)Resources.Load("ForeignObject"), new Vector3(ret.injury.GetLocation().x, ret.injury.GetLocation().y, -1), Quaternion.identity) as GameObject;
            ret.foreignObject.transform.parent = ob.transform;
            ret.foreignObject.GetComponent<Foreign_Object_Script>().SetWound(ret.bleedingWound);

            ret.disposal = Instantiate((UnityEngine.Object)Resources.Load("Foreign Object Disposal"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            return ret;
        }

        // Start is called before the first frame update
        void Start()
        {
            //StopTreatment();
        }

        public override void StartTreatment()
        {
            treatmentStarted = true;
            //forceps.SetActive(true);
            foreignObject.SetActive(true);
            bleedingWound.SetActive(true);
            disposal.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            //forceps.SetActive(false);
            foreignObject.SetActive(false);
            bleedingWound.SetActive(false);
            disposal.SetActive(false);
        }

        public override void ShowInjury()
        {
            foreignObject.SetActive(true);
            bleedingWound.SetActive(true);
            disposal.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && foreignObject.GetComponent<Foreign_Object_Script>().GetHealed())
            {
                injury.RemoveTreatment();
                Camera.main.GetComponent<SFXPlaying>().SFXinjuryClear();
                //forceps.SetActive(false);
                //Destroy(forceps);
                foreignObject.SetActive(false);
                Destroy(foreignObject);
                Destroy(bleedingWound);
                Destroy(disposal);
                Destroy(this);
            }
        }
    }
}