using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PatientManagerClass;
using PatientClass;
using System;
using TMPro;

namespace ButtonManagerClass
{
    public class ButtonManager : MonoBehaviour
    {
        private Button button;
        public Tuple<Patient, Sprite> patient;

        // Start is called before the first frame update
        void Start()
        {
            Button button = this.gameObject.GetComponent<Button>();
            button.onClick.AddListener(TaskOnClick);
        }

        private void Update()
        {
            if (patient != null)
            {
                string health = patient.Item1.GetHealth().ToString();
                this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = health;
            }
        }

        void TaskOnClick()
        {
            GameObject.Find("PatientManager").GetComponent<PatientManager>().SwitchPatient(this);
        }
    }

}