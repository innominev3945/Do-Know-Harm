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
using ButtonManagerClass;
using System;
using UnityEngine.UI;
using TMPro;

namespace PatientManagerClass
{
    public class PatientManager : MonoBehaviour
    {
        private string[] bodypartEnums = { "Head", "Chest", "Left Leg", "Right Leg", "Left Arm", "Right Arm" };

        private float damptime = 0.3f;
        private Vector3 velocity = Vector3.zero;


        [SerializeField] Tuple<Patient, Sprite> currentPatient; // Patient on current screen
        [SerializeField] Bodypart[] bodyparts;

        private Tuple<Patient, Sprite>[] patients; // Patient, sprite string pair
        private ButtonManager[] buttons; // Collection of the buttons used to switch between patients 

        private Queue<Tuple<Patient, Sprite>> nextPatients; // Patients that will enter after current patients are either healed or die
        private TextMeshProUGUI patientInjuryText;
        private TextMeshProUGUI healthText;


        // Start is called before the first frame update
        void Start()
        {
            nextPatients = new Queue<Tuple<Patient, Sprite>>();
            buttons = new ButtonManager[4];
            currentPatient = null;
            bodyparts = null;
            patients = new Tuple<Patient, Sprite>[5];
            patientInjuryText = GameObject.Find("Injury Information").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            healthText = GameObject.Find("Health").transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            for (int i = 0; i < patients.Length; i++)
                patients[i] = null;

            for (int i = 0; i < 4; i++)
            {
                buttons[i] = GameObject.Find("Patient" + i).GetComponent<ButtonManager>();
                buttons[i].patient = null;
            }


            Init();

            foreach (Tuple<Patient, Sprite> patient in nextPatients)
            {
                patient.Item1.AbortTreatments();
                patient.Item1.PauseDamage();
            }

            for (int i = 0; i < 5 && nextPatients.Count != 0; i++)
            {
                patients[i] = nextPatients.Peek();
                patients[i].Item1.UnpauseDamage();
                if (i != 0)
                    buttons[i - 1].patient = patients[i];
                nextPatients.Dequeue();
            }

            if (patients[0] != null)
            {
                currentPatient = patients[0];
                bodyparts = currentPatient.Item1.GetBodyparts();
                UpdateText();
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPatient.Item2;
                gameObject.transform.GetChild(0).transform.position = new Vector3(0, 0, 5);
            }


            currentPatient.Item1.StartTreatments();

            ViewHead();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentPatient.Item1 != null)
                healthText.text = currentPatient.Item1.GetHealth().ToString();
            if ((currentPatient.Item1.GetHealed() || currentPatient.Item1.GetHealth() == 0) && nextPatients.Count != 0)
            {
                Debug.Log("Switching Patient");
                for (int i = 0; i < patients.Length; i++)
                {
                    if (patients[i] == currentPatient)
                    {
                        patients[i] = nextPatients.Peek();
                        break;
                    }
                }
                Destroy(currentPatient.Item1);
                currentPatient = nextPatients.Peek();
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPatient.Item2;
                gameObject.transform.GetChild(0).transform.position = new Vector3(0, 0, 5);
                bodyparts = currentPatient.Item1.GetBodyparts();
                UpdateText();
                currentPatient.Item1.UnpauseDamage();
                currentPatient.Item1.StartTreatments();
                ViewHead();
                nextPatients.Dequeue();
            }
            for (int i = 0; i < patients.Length; i++)
            {
                if (patients[i] != currentPatient && ((patients[i].Item1.GetHealed() || patients[i].Item1.GetHealth() == 0) && nextPatients.Count != 0))
                {
                    Debug.Log("Switching Patient");
                    foreach (ButtonManager button in buttons)
                    {
                        if (button.patient == patients[i])
                        {
                            button.patient = nextPatients.Peek();
                            break;
                        }
                    }
                    Destroy(patients[i].Item1);
                    patients[i] = nextPatients.Peek();
                    patients[i].Item1.UnpauseDamage();
                    nextPatients.Dequeue();
                }
            }
        }

        public void SwitchPatient(ButtonManager btn)
        {
            Tuple<Patient, Sprite> tmp = btn.patient;
            if (tmp == null)
                return;
            currentPatient.Item1.AbortTreatments();
            btn.patient = currentPatient;
            currentPatient = tmp;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPatient.Item2;
            gameObject.transform.GetChild(0).transform.position = new Vector3(0, 0, 5);
            currentPatient.Item1.StartTreatments();
            bodyparts = currentPatient.Item1.GetBodyparts();
            UpdateText();
            ViewHead();
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


            Injury laceration = new Injury(2f, new Vector2(parts1[2].GetLocation().x + 1.6f, parts1[2].GetLocation().y), "Forceps");
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

            Injury chestCompression = new Injury(2f, new Vector2(parts2[1].GetLocation().x, parts2[1].GetLocation().y + 3f), "Chest Compression");
            chestCompression.AddTreatment(ChestTreatment.MakeChestTreatmentObject(this.gameObject, chestCompression));
            parts2[1].AddInjury(chestCompression);
            Injury gze = new Injury(3f, new Vector2(parts2[0].GetLocation().x, parts2[0].GetLocation().y + 1f), "Gauze");
            gze.AddTreatment(GauzeTreatment.MakeGauzeTreatmentObject(this.gameObject, gze));
            parts2[0].AddInjury(gze);

            nextPatients.Enqueue(new Tuple<Patient, Sprite>(Patient.MakePatientObject(this.gameObject, parts2, 1f), Resources.Load<Sprite>("MaleBody")));

            /* Creating Third Patient */
            Bodypart[] parts3 = new Bodypart[6];
            parts3[0] = Bodypart.MakeBodypartObject(this.gameObject, 0.7f, 1f, new Vector2(0f, 4.5f)); // Head
            parts3[1] = Bodypart.MakeBodypartObject(this.gameObject, 0.5f, 1f, new Vector2(0f, -2f)); // Chest
            parts3[2] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-0.5f, -12)); // Left leg 
            parts3[3] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(0.5f, -12)); // Right leg 
            parts3[4] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f)); // Left arm
            parts3[5] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f)); // Right arm

            Injury woundDress = new Injury(2f, new Vector2(parts3[0].GetLocation().x, parts3[0].GetLocation().y + 1f), "Wound Dress");
            woundDress.AddTreatment(DressTreatment.MakeDressTreatmentObject(this.gameObject, woundDress));
            parts3[0].AddInjury(woundDress);

            nextPatients.Enqueue(new Tuple<Patient, Sprite>(Patient.MakePatientObject(this.gameObject, parts3, 1f), Resources.Load<Sprite>("MaleBody")));

            /* Creating Fourth Patient */
            Bodypart[] parts4 = new Bodypart[6];
            parts4[0] = Bodypart.MakeBodypartObject(this.gameObject, 0.7f, 1f, new Vector2(0f, 4.5f)); // Head
            parts4[1] = Bodypart.MakeBodypartObject(this.gameObject, 0.5f, 1f, new Vector2(0f, -2f)); // Chest
            parts4[2] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-0.5f, -12)); // Left leg 
            parts4[3] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(0.5f, -12)); // Right leg 
            parts4[4] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f)); // Left arm
            parts4[5] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f)); // Right arm

            Injury frcps = new Injury(2f, new Vector2(parts4[4].GetLocation().x + 2.5f, parts4[4].GetLocation().y + 6f), "Forceps");
            frcps.AddTreatment(ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject, frcps));
            parts4[4].AddInjury(frcps);

            nextPatients.Enqueue(new Tuple<Patient, Sprite>(Patient.MakePatientObject(this.gameObject, parts4, 1f), Resources.Load<Sprite>("MaleBody")));

            /* Creating Fifth Patient */
            Bodypart[] parts5 = new Bodypart[6];
            parts5[0] = Bodypart.MakeBodypartObject(this.gameObject, 0.7f, 1f, new Vector2(0f, 4.5f)); // Head
            parts5[1] = Bodypart.MakeBodypartObject(this.gameObject, 0.5f, 1f, new Vector2(0f, -2f)); // Chest
            parts5[2] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-0.5f, -12)); // Left leg 
            parts5[3] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(0.5f, -12)); // Right leg 
            parts5[4] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f)); // Left arm
            parts5[5] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f)); // Right arm

            Injury bndg = new Injury(2f, new Vector2(parts5[3].GetLocation().x + 0.5f, parts5[3].GetLocation().y), "Wound Dress");
            bndg.AddTreatment(DressTreatment.MakeDressTreatmentObject(this.gameObject, bndg));
            parts5[3].AddInjury(bndg);

            nextPatients.Enqueue(new Tuple<Patient, Sprite>(Patient.MakePatientObject(this.gameObject, parts5, 1f), Resources.Load<Sprite>("MaleBody")));

            /* Creating Sixth Patient */
            Bodypart[] parts6 = new Bodypart[6];
            parts6[0] = Bodypart.MakeBodypartObject(this.gameObject, 0.7f, 1f, new Vector2(0f, 4.5f)); // Head
            parts6[1] = Bodypart.MakeBodypartObject(this.gameObject, 0.5f, 1f, new Vector2(0f, -2f)); // Chest
            parts6[2] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-0.5f, -12)); // Left leg 
            parts6[3] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(0.5f, -12)); // Right leg 
            parts6[4] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f)); // Left arm
            parts6[5] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f)); // Right arm

            Injury gauze = new Injury(2f, new Vector2(parts6[3].GetLocation().x + 0.5f, parts6[3].GetLocation().y), "Gauze");
            gauze.AddTreatment(GauzeTreatment.MakeGauzeTreatmentObject(this.gameObject, gauze));
            parts6[3].AddInjury(gauze);

            nextPatients.Enqueue(new Tuple<Patient, Sprite>(Patient.MakePatientObject(this.gameObject, parts6, 1f), Resources.Load<Sprite>("MaleBody")));
        }

        private void UpdateText()
        {
            if (currentPatient != null)
            {
                string information = "";
                for (int i = 0; i < bodyparts.Length; i++)
                {
                    if (!bodyparts[i].GetHealed())
                    {
                        information += bodypartEnums[i] + ":\n";
                        List<string> names = bodyparts[i].GetInjuryNames();
                        foreach (string name in names)
                            information += name + "\n";
                    }
                }
                patientInjuryText.text = information;
            }
        }
    }
}