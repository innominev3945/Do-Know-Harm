using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatientManagerClass;

public class SyretteHitboxScript : MonoBehaviour
{
    [SerializeField] private GameObject patientManager;
    private bool syretteOnCooldown;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        syretteOnCooldown = patientManager.GetComponent<PatientManager>().BoostHealthOnCooldown();
    }

    public void SyretteOnHit()
    {
        if (!syretteOnCooldown)
        {
            patientManager.GetComponent<PatientManager>().BoostPatientHealth(10f);
        }
    }

    public bool BoostHealthOnCooldown()
    {
        return syretteOnCooldown;
    }
}
