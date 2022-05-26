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
using BurnTreatmentClass;
using ButtonManagerClass;
using System;
using UnityEngine.UI;
using TMPro;

namespace PatientManagerClass
{
    public class PatientManager : MonoBehaviour
    {
        private string[] bodypartEnums = { "Head", "Chest", "Left Leg", "Right Leg", "Left Arm", "Right Arm" };

        [SerializeField] VNVariableStorage numDeaths; // Storage for VN
        private bool[] patientStatus; // Mapping of the status (true = alive, false = dead) for all patients 

        private float damptime = 0.3f;
        private Vector3 velocity = Vector3.zero;

        // Tuple: (1) Patient Object, (2) Patient Sprite, (3) Unique Patient ID
        public Tuple<Patient, Sprite, int> currentPatient; // Patient on current screen
        //[SerializeField] Tuple<Patient, Sprite, int> currentPatient; // Patient on current screen
        [SerializeField] Bodypart[] bodyparts; // Bodypart of the current patient on screen

        private Tuple<Patient, Sprite, int>[] patients; // Collection of "current" patients in rotation
        private ButtonManager[] buttons; // Collection of the buttons used to switch between current patients in rotation

        private Queue<Tuple<Patient, Sprite, int>> nextPatients; // Patients that will enter after current patients are either healed or die
        
        private TextMeshProUGUI patientInjuryText;
        private TextMeshProUGUI healthText;
        private TextMeshProUGUI queueText; 

        private bool transitioning = false;

        private bool levelComplete;
        // Start is called before the first frame update
        void Start()
        {
            // Initialize all variables
            nextPatients = new Queue<Tuple<Patient, Sprite, int>>();
            buttons = new ButtonManager[4];
            currentPatient = null;
            bodyparts = null;
            patients = new Tuple<Patient, Sprite, int>[5];
            patientInjuryText = GameObject.Find("Injury Information").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            healthText = GameObject.Find("Health").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            queueText = GameObject.Find("Queue").transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            for (int i = 0; i < patients.Length; i++)
                patients[i] = null;

            for (int i = 0; i < 4; i++)
            {
                buttons[i] = GameObject.Find("Patient" + i).GetComponent<ButtonManager>();
                buttons[i].patient = null;
            }

            // Adds the current level's patients to the nextPatient queue
            Init();
            patientStatus = new bool[nextPatients.Count];
            for (int i = 0; i < patientStatus.Length; i++)
                patientStatus[i] = true;

            foreach (Tuple<Patient, Sprite, int> patient in nextPatients)
            {
                patient.Item1.AbortTreatments();
                patient.Item1.PauseDamage();
            }

            for (int i = 0; i < 5 && nextPatients.Count != 0; i++)
            {
                patients[i] = nextPatients.Peek();
                patients[i].Item1.UnpauseDamage();
                if (i != 0)
                {
                    buttons[i - 1].patient = patients[i];
                    buttons[i - 1].UpdateNumberIcon();
                }
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
            Debug.Log("Number of Injuries: " + currentPatient.Item1.GetNumInjuries());
            if (currentPatient.Item1 != null)
                //healthText.text = currentPatient.Item1.GetHealth().ToString();

            queueText.text = "Upcoming Patients: " + nextPatients.Count;

            // Handles switching out patients if they are either fully healed of their injuries or are dead
            // Switching out current patient 
            if ((currentPatient.Item1.GetHealed() || currentPatient.Item1.GetHealth() == 0) && nextPatients.Count != 0 && !transitioning)
            {
                transitioning = true;
                currentPatient.Item1.AbortTreatments();
                //gameObject.GetComponent<SaveDeathTransition>().currentPatientDeath(gameObject.transform.GetChild(0).gameObject);
                Debug.Log("current patient dead or healed");
                Debug.Log("Switching Patient");
                if (currentPatient.Item1.GetHealed())
                {
                    currentPatient.Item1.DestroyTreatmentObjects();
                    gameObject.GetComponent<SaveDeathTransition>().currentPatientSaved(this.gameObject);
                    Debug.Log("current patient dead or healed");
                    Debug.Log("Switching Patient");
                    currentPatient.Item1.PauseDamage();
                }
                else
                {
                    currentPatient.Item1.DestroyTreatmentObjects();
                    patientStatus[currentPatient.Item3] = false;
                    gameObject.GetComponent<SaveDeathTransition>().currentPatientDeath(this.gameObject);
                    Debug.Log("current patient dead or healed");
                    Debug.Log("Switching Patient");
                    currentPatient.Item1.PauseDamage();
                }
            } // If the current patient is healed, but there are no next patients to bring in, update the current patient's text to indicate that they're fully healed  
            else if (currentPatient.Item1.GetHealed() && nextPatients.Count == 0)
            { 
                UpdateText();
            } // If the current patient is dead, but there are no next patients to bring in, update the current patient's text to indicate that they're dead 
            else if (currentPatient.Item1.GetHealth() == 0 && nextPatients.Count == 0)
            {
                currentPatient.Item1.DestroyTreatmentObjects();
                patientStatus[currentPatient.Item3] = false;
                UpdateText();
            }
            // Switching out non-current patients
            for (int i = 0; i < patients.Length; i++)
            {
                if (patients[i] != currentPatient && ((patients[i].Item1.GetHealed() || patients[i].Item1.GetHealth() == 0) && nextPatients.Count != 0))
                {
                    foreach (ButtonManager button in buttons)
                    {
                        if (button.patient == patients[i])
                        {
                            button.patient = nextPatients.Peek();
                            button.UpdateNumberIcon();
                            break;
                        }
                    }
                    if (patients[i].Item1.GetHealth() == 0)
                        patientStatus[patients[i].Item3] = false;
                    patients[i].Item1.DestroyTreatmentObjects();
                    Destroy(patients[i].Item1);
                    patients[i] = nextPatients.Peek();
                    patients[i].Item1.UnpauseDamage();
                    nextPatients.Dequeue();
                }
                else if (patients[i] != currentPatient && patients[i].Item1.GetHealed())
                    patients[i].Item1.DestroyTreatmentObjects();
                else if (patients[i] != currentPatient && patients[i].Item1.GetHealth() == 0)
                {
                    patients[i].Item1.DestroyTreatmentObjects();
                    patientStatus[patients[i].Item3] = false;
                }
            }
        }

        public void PatientSaveDeathTransitionHelper2() // called by MoveSavedPatient of SaveDeathTransition script when saved patient is moved offscreen
        {
            //StartCoroutine(PatientSaveDeathTransitionHelper3());
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
            //yield return new WaitForSeconds(0.6f);
            transitioning = false;
        }

        IEnumerator SwitchPatientHelper(ButtonManager btn)
        {
            gameObject.GetComponent<SaveDeathTransition>().PatientSwitchTransition(0.6f);
            yield return new WaitForSeconds(0.6f);
            Tuple<Patient, Sprite, int> tmp = btn.patient;
            currentPatient.Item1.AbortTreatments();
            btn.patient = currentPatient;
            btn.UpdateNumberIcon();
            currentPatient = tmp;
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPatient.Item2;
            gameObject.transform.GetChild(0).transform.position = new Vector3(0, 0, 5);
            currentPatient.Item1.StartTreatments();
            bodyparts = currentPatient.Item1.GetBodyparts();
            UpdateText();
            ViewHead();
            yield return new WaitForSeconds(0.6f);
            transitioning = false;
        }

        public void SetNumDeaths()
        {
            int numDeadPatients = 0;
            foreach (bool status in patientStatus)
                if (!status)
                    numDeadPatients++;
            numDeaths.setNumberValue("$patientDeaths", (float)numDeadPatients);
            numDeaths.Save();
        }

        public bool GetLevelComplete()
        {
            if (nextPatients.Count == 0)
            {
                foreach (Tuple<Patient, Sprite, int> patient in patients)
                {
                    if (!(patient.Item1.GetHealed() || patient.Item1.GetHealth() == 0))
                        return false;
                }
                return true;
            }
            return false;
        }

        public void SwitchPatient(ButtonManager btn)
        {
            if (btn.patient.Item1.GetHealed())
            {
                Debug.Log("Can't Switch, Patient Already Healed");
                return;
            }
            if (btn.patient.Item1.GetHealth() == 0)
            {
                Debug.Log("Can't Switch, Patient is Dead");
                return;
            }
            if (!transitioning)
            {
                transitioning = true;
                Tuple<Patient, Sprite, int> tmp = btn.patient;
                if (tmp == null)
                    return;
                StartCoroutine(SwitchPatientHelper(btn));
            }
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
            laceration.AddTreatment(ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject, laceration, 0f));
            parts1[2].AddInjury(laceration);

            nextPatients.Enqueue(new Tuple<Patient, Sprite, int>(Patient.MakePatientObject(this.gameObject, parts1, 1f), Resources.Load<Sprite>("MaleBody"), 0));
            

            /* Creating Second Patient */
            Bodypart[] parts2 = new Bodypart[6];
            parts2[0] = Bodypart.MakeBodypartObject(this.gameObject, 0.7f, 1f, new Vector2(0f, 4.5f)); // Head
            parts2[1] = Bodypart.MakeBodypartObject(this.gameObject, 0.5f, 1f, new Vector2(0f, -2f)); // Chest
            parts2[2] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-0.5f, -12)); // Left leg 
            parts2[3] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(0.5f, -12)); // Right leg 
            parts2[4] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f)); // Left arm
            parts2[5] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f)); // Right arm

            Injury chestCompression = new Injury(5f, new Vector2(parts2[1].GetLocation().x, parts2[1].GetLocation().y + 3f), "Chest Compression");
            chestCompression.AddTreatment(ChestTreatment.MakeChestTreatmentObject(this.gameObject, chestCompression));
            parts2[1].AddInjury(chestCompression);
            Injury gze = new Injury(5f, new Vector2(parts2[0].GetLocation().x, parts2[0].GetLocation().y + 1f), "Gauze");
            gze.AddTreatment(GauzeTreatment.MakeGauzeTreatmentObject(this.gameObject, gze));
            parts2[0].AddInjury(gze);

            nextPatients.Enqueue(new Tuple<Patient, Sprite, int>(Patient.MakePatientObject(this.gameObject, parts2, 1f), Resources.Load<Sprite>("MaleBody"), 1));

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

            Injury burn = new Injury(1f, new Vector2(parts2[1].GetLocation().x, parts3[1].GetLocation().y + 1f), "Burn Treatment");
            burn.AddTreatment(BurnTreatment.MakeBurnTreatmentObject(this.gameObject, burn));
            parts3[1].AddInjury(burn);

            nextPatients.Enqueue(new Tuple<Patient, Sprite, int>(Patient.MakePatientObject(this.gameObject, parts3, 1f), Resources.Load<Sprite>("MaleBody"), 2));

            /* Creating Fourth Patient */
            Bodypart[] parts4 = new Bodypart[6];
            parts4[0] = Bodypart.MakeBodypartObject(this.gameObject, 0.7f, 1f, new Vector2(0f, 4.5f)); // Head
            parts4[1] = Bodypart.MakeBodypartObject(this.gameObject, 0.5f, 1f, new Vector2(0f, -2f)); // Chest
            parts4[2] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-0.5f, -12)); // Left leg 
            parts4[3] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(0.5f, -12)); // Right leg 
            parts4[4] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f)); // Left arm
            parts4[5] = Bodypart.MakeBodypartObject(this.gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f)); // Right arm

            Injury frcps = new Injury(2f, new Vector2(parts4[4].GetLocation().x + 2.5f, parts4[4].GetLocation().y + 6f), "Forceps");
            frcps.AddTreatment(ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject, frcps, 180f));
            parts4[4].AddInjury(frcps);

            nextPatients.Enqueue(new Tuple<Patient, Sprite, int>(Patient.MakePatientObject(this.gameObject, parts4, 1f), Resources.Load<Sprite>("MaleBody"), 3));

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

            nextPatients.Enqueue(new Tuple<Patient, Sprite, int>(Patient.MakePatientObject(this.gameObject, parts5, 1f), Resources.Load<Sprite>("MaleBody"), 4));

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

            nextPatients.Enqueue(new Tuple<Patient, Sprite, int>(Patient.MakePatientObject(this.gameObject, parts6, 1f), Resources.Load<Sprite>("MaleBody"), 5));
        }

        private void UpdateText()
        {
            if (currentPatient != null && !currentPatient.Item1.GetHealed())
            {
                if (currentPatient.Item1.GetHealth() != 0)
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
                else
                    patientInjuryText.text = "Dead";
            }
            else if (currentPatient != null && currentPatient.Item1.GetHealed())
                patientInjuryText.text = "Healed";
        }
    }
}