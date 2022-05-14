using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatientDeathLimit : MonoBehaviour
{

    public GameOverScreen GameOverScreen;

    int patientsDead = 0;
    int maxPatientsDead = 5;

    bool[] patientsAlive = {true, true, true, true, true};

    private void Start()
    {
        
    }

    private void Update()
    {
        checkPatientDeathToll();
    }

    public void checkPatientDeathToll()
    {
        patientsDead = 0;

        for (int i = 0; i<patientsAlive.Length; i++)
        {
            if(patientsAlive[i] == false)
            {
                patientsDead++;
            }

            //Debug.Log(patientsDead);

            if (patientsDead >= maxPatientsDead)
            {
                GameOver();
            }
        }
    }

    //used soley for testing GameOverScreen
    public void killAllPatients()
    {
        for (int i = 0; i < patientsAlive.Length; i++)
        {
            patientsAlive[i] = false;
        }

    }   

    //called whenever player loses
    public void GameOver()
    {
        GameOverScreen.Setup(maxPatientsDead);
    }

    //Deugging purposes
    public void OnPointerDown(PointerEventData eventData)
    {
        killAllPatients();
    }

    public void HideButton()
    {
        gameObject.SetActive(false);
    }
}
