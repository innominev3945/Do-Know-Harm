using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatientClass;
using BodypartClass;
using InjuryClass;
using TreatmentClass;
using ForcepsTreatmentClass;
using GauzeTreatmentClass;
using DressTreatmentClass;
using ChestTreatmentClass;
using System;

namespace PatientManagerClass
{
    public class PatientManager : MonoBehaviour
    {
        private float damptime = 0.3f;
        private Vector3 velocity = Vector3.zero;


        [SerializeField] Tuple<Patient, Sprite> currentPatient; // Patient on main screen
        [SerializeField] Bodypart[] bodyparts;

        private Tuple<Patient, Sprite>[] patients; // Patient, sprite string pair
        private int[] buttonMappings; // Maps a given patient to one of the four buttons 

        private Queue<Tuple<Patient, Sprite>> nextPatients;

        // Start is called before the first frame update
        void Start()
        {
            nextPatients = new Queue<Tuple<Patient, Sprite>>();

            currentPatient = null;
            bodyparts = null;


            patients = new Tuple<Patient, Sprite>[4];
            for (int i = 0; i < patients.Length; i++)
                patients[i] = null;

            buttonMappings = new int[4];
            for (int i = 0; i < buttonMappings.Length; i++)
                buttonMappings[i] = -1;

            Init();

            for (int i = 0; i < 5 && nextPatients.Count != 0; i++)
            {
                patients[i] = nextPatients.Peek();
                nextPatients.Dequeue();
            }

            if (patients[0] != null)
            {
                currentPatient = patients[0];
                bodyparts = currentPatient.Item1.GetBodyparts();
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPatient.Item2;
                gameObject.transform.GetChild(0).transform.position = new Vector3(0, 0, 5);
            }

            for (int i = 1; i < 5 && patients[i] != null; i++)
            {
                buttonMappings[i - 1] = i;
            }

            foreach (Tuple<Patient, Sprite> patient in patients)
            {
                if (patient != null && patient != currentPatient)
                    patient.Item1.AbortTreatments();
            }

            currentPatient.Item1.StartTreatments();

            ViewHead();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SwitchPatient(string buttonNum)
        {
            int mapping = int.Parse(buttonNum.Substring(7));
            Debug.Log(mapping);


            int i;
            for (i = 0; i < patients.Length; i++)
            {
                if (currentPatient.Item1 == patients[i].Item1)
                    break;
            }

            int pos = buttonMappings[mapping];
            if (pos == -1)
                return;

            Tuple<Patient, Sprite> pair = patients[pos];
            if (pair == null)
                return;


            currentPatient.Item1.AbortTreatments();
            currentPatient = pair;
            bodyparts = currentPatient.Item1.GetBodyparts();
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPatient.Item2;
            gameObject.transform.GetChild(0).transform.position = new Vector3(0, 0, 5);
            currentPatient.Item1.StartTreatments();
            ViewHead();

            buttonMappings[mapping] = i;
        }

        public void ViewHead()
        {
            float z = Camera.main.transform.position.z;
            Bodypart head = bodyparts[0];
            Camera.main.transform.position = new Vector3(head.GetLocation().x, head.GetLocation().y, z);
            currentPatient.Item1.AbortTreatments();
            head.TreatInjuries();
        }

        public void ViewChest()
        {
            float z = Camera.main.transform.position.z;
            Bodypart chest = bodyparts[1];
            Camera.main.transform.position = new Vector3(chest.GetLocation().x, chest.GetLocation().y, z);
            currentPatient.Item1.AbortTreatments();
            chest.TreatInjuries();
            bodyparts[4].TreatInjuries();
            bodyparts[5].TreatInjuries();
        }

        public void ViewLegs()
        {
            float z = Camera.main.transform.position.z;
            Bodypart leftLeg = bodyparts[2];
            Bodypart rightLeg = bodyparts[3];
            Camera.main.transform.position = new Vector3((leftLeg.GetLocation().x + rightLeg.GetLocation().x) / 2, leftLeg.GetLocation().y, z);
            currentPatient.Item1.AbortTreatments();
            leftLeg.TreatInjuries();
            rightLeg.TreatInjuries();
        }
        
        // Temporary method to initialize patients - in the future, create a read from file procedure to take data about a patient and initialize it
        private void Init() 
        {
            /* Creating First Patient */
            Bodypart[] parts1 = new Bodypart[6];
            parts1[0] = Bodypart.MakeBodypartObject(this.gameObject, 0.7f, 1f, new Vector2(0f, 4.5f)); // Head
            parts1[1] = Bodypart.MakeBodypartObject(this.gameObject, 0.5f, 1f, new Vector2(0f, -2f)); // Chest
            parts1[2] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-0.5f, -12)); // Left leg 
            parts1[3] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(0.5f, -12)); // Right leg 
            parts1[4] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f)); // Left arm
            parts1[5] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f)); // Right arm


            Injury laceration = new Injury(2f, new Vector2(parts1[2].GetLocation().x + 1.6f, parts1[2].GetLocation().y));
            laceration.AddTreatment(ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject, laceration));
            parts1[2].AddInjury(laceration);

            nextPatients.Enqueue(new Tuple<Patient, Sprite>(Patient.MakePatientObject(this.gameObject, parts1, 1f), Resources.Load<Sprite>("MaleBody")));
            

            /* Creating Second Patient */
            Bodypart[] parts2 = new Bodypart[6];
            parts2[0] = Bodypart.MakeBodypartObject(this.gameObject, 0.7f, 1f, new Vector2(0f, 4.5f)); // Head
            parts2[1] = Bodypart.MakeBodypartObject(this.gameObject, 0.5f, 1f, new Vector2(0f, -2f)); // Chest
            parts2[2] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-0.5f, -12)); // Left leg 
            parts2[3] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(0.5f, -12)); // Right leg 
            parts2[4] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f)); // Left arm
            parts2[5] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f)); // Right arm

            Injury chestCompression = new Injury(2f, new Vector2(parts2[1].GetLocation().x, parts2[1].GetLocation().y + 3f));
            chestCompression.AddTreatment(ChestTreatment.MakeChestTreatmentObject(this.gameObject, chestCompression));
            parts2[1].AddInjury(chestCompression);

            nextPatients.Enqueue(new Tuple<Patient, Sprite>(Patient.MakePatientObject(this.gameObject, parts2, 1f), Resources.Load<Sprite>("MaleBody")));


        }

        /*
        public void SwitchHead()
        {
            float z = Camera.main.transform.position.z;
            Bodypart head = bodyparts[0];
            Camera.main.transform.position = new Vector3(head.GetLocation().x, head.GetLocation().y, z);
            currentPatient.AbortTreatments();
            head.TreatInjuries();
        }

        public void SwitchChest()
        {
            float z = Camera.main.transform.position.z;
            Bodypart chest = bodyparts[1];
            Camera.main.transform.position = new Vector3(chest.GetLocation().x, chest.GetLocation().y, z);
            currentPatient.AbortTreatments();
            chest.TreatInjuries();
        }

        public void SwitchLeftLeg()
        {
            float z = Camera.main.transform.position.z;
            Bodypart leftLeg = bodyparts[2];
            Camera.main.transform.position = new Vector3(leftLeg.GetLocation().x, leftLeg.GetLocation().y, z);
            currentPatient.AbortTreatments();
            leftLeg.TreatInjuries();
        }

        public void SwitchRightLeg()
        {
            float z = Camera.main.transform.position.z;
            Bodypart rightLeg = bodyparts[3];
            Camera.main.transform.position = new Vector3(rightLeg.GetLocation().x, rightLeg.GetLocation().y, z);
            currentPatient.AbortTreatments();
            rightLeg.TreatInjuries();
        }

        public void SwitchLeftArm()
        {
            float z = Camera.main.transform.position.z;
            Bodypart leftArm = bodyparts[4];
            Camera.main.transform.position = new Vector3(leftArm.GetLocation().x, leftArm.GetLocation().y, z);
            currentPatient.AbortTreatments();
            leftArm.TreatInjuries();
        }

        public void SwitchRightArm()
        {
            float z = Camera.main.transform.position.z;
            Bodypart rightArm = bodyparts[5];
            Camera.main.transform.position = new Vector3(rightArm.GetLocation().x, rightArm.GetLocation().y, z);
            currentPatient.AbortTreatments();
            rightArm.TreatInjuries();
        }
        */
    }
}