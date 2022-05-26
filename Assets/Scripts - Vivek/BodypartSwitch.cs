/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BodypartSwitchClass
{
    public class BodypartSwitch : MonoBehaviour
    {

        private float damptime = 0.3f;
        private Vector3 velocity = Vector3.zero;

        public void SwitchHead()
        {
            float z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(this.gameObject.GetComponent<PatientFunctionality>().head.GetLocation().x, this.gameObject.GetComponent<PatientFunctionality>().head.GetLocation().y, z);
            AbortAllTreatments();
            this.gameObject.GetComponent<PatientFunctionality>().head.TreatInjuries();
        }

        public void SwitchChest()
        {
            float z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(this.gameObject.GetComponent<PatientFunctionality>().chest.GetLocation().x, this.gameObject.GetComponent<PatientFunctionality>().chest.GetLocation().y, z);
            AbortAllTreatments();
            this.gameObject.GetComponent<PatientFunctionality>().chest.TreatInjuries();
        }

        public void SwitchLeftLeg()
        {
            float z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(this.gameObject.GetComponent<PatientFunctionality>().leftLeg.GetLocation().x, this.gameObject.GetComponent<PatientFunctionality>().leftLeg.GetLocation().y, z);
            AbortAllTreatments();
            this.gameObject.GetComponent<PatientFunctionality>().leftLeg.TreatInjuries();
        }

        public void SwitchRightLeg()
        {
            float z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(this.gameObject.GetComponent<PatientFunctionality>().rightLeg.GetLocation().x, this.gameObject.GetComponent<PatientFunctionality>().rightLeg.GetLocation().y, z);
            AbortAllTreatments();
            this.gameObject.GetComponent<PatientFunctionality>().rightLeg.TreatInjuries();
        }

        public void SwitchLeftArm()
        {
            float z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(this.gameObject.GetComponent<PatientFunctionality>().leftArm.GetLocation().x, this.gameObject.GetComponent<PatientFunctionality>().leftArm.GetLocation().y, z);
            AbortAllTreatments();
            this.gameObject.GetComponent<PatientFunctionality>().leftArm.TreatInjuries();
        }

        public void SwitchRightArm()
        {
            float z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(this.gameObject.GetComponent<PatientFunctionality>().rightArm.GetLocation().x, this.gameObject.GetComponent<PatientFunctionality>().rightArm.GetLocation().y, z);
            AbortAllTreatments();
            this.gameObject.GetComponent<PatientFunctionality>().rightArm.TreatInjuries();
        }

        public void AbortAllTreatments()
        {
            this.gameObject.GetComponent<PatientFunctionality>().head.StopInjuries();
            this.gameObject.GetComponent<PatientFunctionality>().chest.StopInjuries();
            this.gameObject.GetComponent<PatientFunctionality>().leftLeg.StopInjuries();
            this.gameObject.GetComponent<PatientFunctionality>().rightLeg.StopInjuries();
            this.gameObject.GetComponent<PatientFunctionality>().leftArm.StopInjuries();
            this.gameObject.GetComponent<PatientFunctionality>().rightArm.StopInjuries();
        }
    }
}*/