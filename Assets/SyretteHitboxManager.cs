using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyretteHitboxManager : MonoBehaviour
{
    [SerializeField] private GameObject mhitboxLarm;
    [SerializeField] private GameObject mhitboxRarm;
    [SerializeField] private GameObject mhitboxAbdomen;
    [SerializeField] private GameObject mhitboxLleg;
    [SerializeField] private GameObject mhitboxRleg;

    [SerializeField] private GameObject fhitboxLarm;
    [SerializeField] private GameObject fhitboxRarm;
    [SerializeField] private GameObject fhitboxAbdomen;
    [SerializeField] private GameObject fhitboxLleg;
    [SerializeField] private GameObject fhitboxRleg;

    private bool isMale;

    // Start is called before the first frame update
    void Start()
    {
        isMale = true;

        fhitboxAbdomen.SetActive(false);
        fhitboxLarm.SetActive(false);
        fhitboxLleg.SetActive(false);
        fhitboxRarm.SetActive(false);
        fhitboxRleg.SetActive(false);

        mhitboxAbdomen.SetActive(false);
        mhitboxLarm.SetActive(false);
        mhitboxLleg.SetActive(false);
        mhitboxRarm.SetActive(false);
        mhitboxRleg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMaleHitboxes()
    {
        isMale = true;

        fhitboxAbdomen.SetActive(false);
        fhitboxLarm.SetActive(false);
        fhitboxLleg.SetActive(false);
        fhitboxRarm.SetActive(false);
        fhitboxRleg.SetActive(false);

        mhitboxAbdomen.SetActive(true);
        mhitboxLarm.SetActive(true);
        mhitboxLleg.SetActive(true);
        mhitboxRarm.SetActive(true);
        mhitboxRleg.SetActive(true);
    }

    public void SetFemaleHitboxes()
    {
        isMale = false;

        mhitboxAbdomen.SetActive(false);
        mhitboxLarm.SetActive(false);
        mhitboxLleg.SetActive(false);
        mhitboxRarm.SetActive(false);
        mhitboxRleg.SetActive(false);

        fhitboxAbdomen.SetActive(true);
        fhitboxLarm.SetActive(true);
        fhitboxLleg.SetActive(true);
        fhitboxRarm.SetActive(true);
        fhitboxRleg.SetActive(true);
    }

    public void SwitchGenderHitboxes()
    {
        if (isMale)
        {
            mhitboxAbdomen.SetActive(false);
            mhitboxLarm.SetActive(false);
            mhitboxLleg.SetActive(false);
            mhitboxRarm.SetActive(false);
            mhitboxRleg.SetActive(false);

            fhitboxAbdomen.SetActive(true);
            fhitboxLarm.SetActive(true);
            fhitboxLleg.SetActive(true);
            fhitboxRarm.SetActive(true);
            fhitboxRleg.SetActive(true);
        }
        else
        {
            fhitboxAbdomen.SetActive(false);
            fhitboxLarm.SetActive(false);
            fhitboxLleg.SetActive(false);
            fhitboxRarm.SetActive(false);
            fhitboxRleg.SetActive(false);

            mhitboxAbdomen.SetActive(true);
            mhitboxLarm.SetActive(true);
            mhitboxLleg.SetActive(true);
            mhitboxRarm.SetActive(true);
            mhitboxRleg.SetActive(true);
        }

        isMale = !isMale;
    }
}
