using InjuryClass;
using TreatmentClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// see: Forceps_script, Foreign_object_script, injury, ForcepsTreatment, PatientFunctionality, Tourniquet, TourniquetTab


namespace TourniquetTreatmentClass
{
    public class TourniquetTreatment : Treatment
    {
        private GameObject tourniquet;
        private GameObject bleedingWound;

        public static TourniquetTreatment MakeTourniquetTreatment(GameObject obj, Injury inj)
        {
            TourniquetTreatment ret = obj.AddComponent<TourniquetTreatment>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = inj;

            ret.tourniquet = Instantiate((UnityEngine.Object)Resources.Load("Tourniquet2"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            ret.bleedingWound = Instantiate((UnityEngine.Object)Resources.Load("BleedingWound"), ret.injury.GetLocation(), Quaternion.identity) as GameObject;
            
            return ret;
        }

        public override void StartTreatment()
        {
            treatmentStarted = true;
            tourniquet.SetActive(true);
            bleedingWound.SetActive(true);
        }

        public override void StopTreatment()
        {
            treatmentStarted = false;
            tourniquet.SetActive(false);
            bleedingWound.SetActive(false);
        }
    }

    public class Tourniquet_Script : MonoBehaviour // make this in another script? probably
    {
        private bool isSet;
        private bool isTightening;
        private bool finished;
        private bool mousePressed;
        // may need gameobject variable for tab

        public void setTourniquet(Vector2 position)
        {

        }

        public void ClickTourniquet(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                mousePressed = true;
            }
            else if (context.canceled)
            {
                mousePressed = false;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            GameObject arm = collision.gameObject;
            if(arm.tag == "limb")
            {
                transform.position = new Vector2(arm.transform.position.x, this.transform.position.y);
                transform.rotation = arm.transform.rotation;

                float heightDiff = transform.position.y - arm.transform.position.y;
                Vector3 eulerAngles = arm.transform.rotation.eulerAngles;
                float xDiff = Mathf.Tan(Mathf.Deg2Rad * eulerAngles.z) * heightDiff;
                transform.position = new Vector2(arm.transform.position.x - xDiff, transform.position.y);
            }
        }

    }
}

