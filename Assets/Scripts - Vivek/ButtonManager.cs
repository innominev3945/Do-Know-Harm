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
        public Tuple<Patient, Sprite, int> patient;

        // Start is called before the first frame update
        void Start()
        {
            Button button = this.gameObject.GetComponent<Button>();
            button.onClick.AddListener(TaskOnClick);
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon1");
        }

        private void Update()
        {
            if (patient != null)
            {
                float health = patient.Item1.GetHealth();
                if (health <= 0)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon10");
                else if (health > 0 && health <= 11.1f)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon9");
                else if (health > 11.1f && health <= 22.2f)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon8");
                else if (health > 22.2f && health <= 33.3f)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon7");
                else if (health > 33.3f && health <= 44.4f)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon6");
                else if (health > 44.4f && health <= 55.5f)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon5");
                else if (health > 55.5f && health <= 66.6f)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon4");
                else if (health > 66.6f && health <= 77.7f)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon3");
                else if (health > 77.7f && health <= 88.8f)
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon2");
                else // if health >= 88.8f
                    this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/Icon1");
            }
        }

        void TaskOnClick()
        {
            GameObject.Find("PatientManager").GetComponent<PatientManager>().SwitchPatient(this);
        }
    }

}