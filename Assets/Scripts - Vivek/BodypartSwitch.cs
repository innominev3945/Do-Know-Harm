using System.Collections;
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

        public void SwitchLegs()
        {
            float z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(this.gameObject.GetComponent<PatientFunctionality>().legs.GetLocation().x, this.gameObject.GetComponent<PatientFunctionality>().legs.GetLocation().y, z);
            AbortAllTreatments();
            this.gameObject.GetComponent<PatientFunctionality>().legs.TreatInjuries();
        }

        public void SwitchArms()
        {
            float z = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(this.gameObject.GetComponent<PatientFunctionality>().arms.GetLocation().x, this.gameObject.GetComponent<PatientFunctionality>().arms.GetLocation().y, z);
            AbortAllTreatments();
            this.gameObject.GetComponent<PatientFunctionality>().arms.TreatInjuries();
        }

        public void AbortAllTreatments()
        {
            this.gameObject.GetComponent<PatientFunctionality>().head.StopInjuries();
            this.gameObject.GetComponent<PatientFunctionality>().chest.StopInjuries();
            this.gameObject.GetComponent<PatientFunctionality>().arms.StopInjuries();
            this.gameObject.GetComponent<PatientFunctionality>().legs.StopInjuries();
        }
    }
}