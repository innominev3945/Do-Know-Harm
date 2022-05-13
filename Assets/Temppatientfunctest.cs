using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using PatientClass;
using BodypartClass;
using InjuryClass;
using TreatmentClass;
using ForcepsTreatmentClass;
using GauzeTreatmentClass;
using DressTreatmentClass;
using ChestTreatmentClass;
using DressTreatmentClass;
using BodypartSwitchClass;
using TourniquetTreatmentClass;

public class Temppatientfunctest : MonoBehaviour
{
    // Start is called before the first frame update
    Patient patient;
    public Bodypart head;
    public Bodypart chest;
    public Bodypart legs;
    public Bodypart arms;
    [SerializeField] float patientHealth;
    [SerializeField] float headHealth;
    [SerializeField] float chestHealth;
    [SerializeField] float legsHealth;
    [SerializeField] float armsHealth;

    // Initializes patient and bodyparts with their respective locations; in the future, generate levels via information from a file (the sprites, injuries, etc)
    private void Start()
    {
        /* Initialize Bodyparts and Patient Here */
        head = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(0).gameObject, 0.7f, 1f, new Vector2(-0.5f, 1f));
        chest = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(1).gameObject, 0.5f, 1f, new Vector2(-0.7f, -6.6f));
        legs = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(2).gameObject, 0.2f, 1f, new Vector2(1f, -17.1f));
        arms = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(3).gameObject, 0.2f, 1f, new Vector2(-1f, -0.7f));

        Bodypart[] bodyparts = { head, chest, legs, arms };
        patient = Patient.MakePatientObject(this.gameObject, bodyparts, 1f);

        /* Create injuries with their respective treatments here*/

        Injury laceration2 = new Injury(2f, arms.GetLocation());
        laceration2.AddTreatment(TourniquetTreatment.MakeTourniquetTreatment(this.gameObject.transform.GetChild(3).gameObject, laceration2));
        laceration2.AddTreatment(ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject.transform.GetChild(3).gameObject, laceration2));
        arms.AddInjury(laceration2);

        Injury chestCompression = new Injury(2f, chest.GetLocation());
        chestCompression.AddTreatment(ChestTreatment.MakeChestTreatmentObject(this.gameObject.transform.GetChild(0).gameObject, chestCompression));
        chest.AddInjury(chestCompression);

        //this.gameObject.GetComponent<BodypartSwitch>().SwitchArms();
        arms.TreatInjuries();
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
