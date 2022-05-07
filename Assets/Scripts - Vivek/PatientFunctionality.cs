/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodypartClass;
using PatientClass;
using InjuryClass;
using TreatmentClass;
using DressTreatmentClass;
using ChestTreatmentClass;
using BurnTreatmentClass;
using ForcepsTreatmentClass;
using GauzeTreatmentClass;
using UnityEngine.EventSystems;

// Driver program demonstrating Patient, Bodypart, Injury, and Laceration
// To use it, click on the sprite and move your mouse around it 8 times 

public class PatientFunctionality : MonoBehaviour
{
    Patient hero;
    Bodypart chest;
    Treatment dress;
    Treatment compress;
    Treatment burn;
    Treatment forceps;
    Treatment gauze;
    Injury laceration;
    Injury compressionInj;
    Injury burnInj;
    Bodypart[] bodyparts;

    // Start is called before the first frame update
    void Start()
    {
        chest = Bodypart.MakeBodypartObject(this.gameObject, 0.2f, 1f); // Create a Bodypart object and add it to the gameObject - remember to pass in this.gameObject to the "constructor"

        laceration = new Injury(2f, new Vector2(1.4f, -2.29f), 5f); // Create a Laceration injury and add it to the gameObject 
        compressionInj = new Injury(0.1f, new Vector2(-0.08f, -2.7f), 1f);
        burnInj = new Injury(0.1f, new Vector2(-0.13f, -3.79f), 1f);

        

        dress = DressTreatment.MakeDressTreatmentObject(this.gameObject, laceration);
        forceps = ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject, laceration);
        burn = BurnTreatment.MakeBurnTreatmentObject(this.gameObject, burnInj);
        gauze = GauzeTreatment.MakeGauzeTreatmentObject(this.gameObject, laceration);
        compress = ChestTreatment.MakeChestTreatmentObject(this.gameObject, compressionInj);

        laceration.AddTreatment(dress);
        laceration.AddTreatment(forceps);
        laceration.AddTreatment(gauze);
        compressionInj.AddTreatment(compress);
        burnInj.AddTreatment(burn);


        chest.AddInjury(laceration); // Add the injury to the intended BodyPart - note that this can be done at any time; it doesn't necessarily need to be at the start of the program
        chest.AddInjury(burnInj);
        chest.AddInjury(compressionInj);

        bodyparts = new Bodypart[1]; // Create an array of BodyParts (in this case only a single Bodypart) to be added to the Patient 

        bodyparts[0] = chest; // Add the Chest BodyPart to the array 
        
        hero = Patient.MakePatientObject(this.gameObject, bodyparts, 1.0f); // Add the array of BodyParts to the Patient when creating them (in a standard situation you'd have more BodyParts)
    }

    // Update is called once per frame
    void Update()
    {
        if (!compressionInj.GetBeingTreated())
            compressionInj.Treat();
        if (!laceration.GetBeingTreated())
            laceration.Treat();
        if (!burnInj.GetBeingTreated())
            burnInj.Treat();
        //burnInj.Treat();
        /*
        if (!laceration.GetBeingTreated())
            laceration.Treat();
        if (laceration.GetInjurySeverity() == 0)
            burnInj.Treat();
        if (burnInj.GetInjurySeverity() == 0)
            compressionInj.Treat();
        */
//Debug.Log("Health: " + hero.GetHealth());
//Debug.Log(thermalOintment.GetComponent<Thermal_Ointment_Script>().GetHealed());
//  }
//}
/*
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

public class PatientFunctionality : MonoBehaviour
{
    Patient patient;
    public Bodypart head;
    public Bodypart chest;
    public Bodypart leftLeg;
    public Bodypart rightLeg;
    public Bodypart leftArm;
    public Bodypart rightArm;
    [SerializeField] float patientHealth;
    [SerializeField] float headHealth;
    [SerializeField] float chestHealth;
    [SerializeField] float leftLegHealth;
    [SerializeField] float rightLegHealth;
    [SerializeField] float leftArmHealth;
    [SerializeField] float rightArmHealth;

    // Initializes patient and bodyparts with their respective locations; in the future, generate levels via information from a file (the sprites, injuries, etc)
    private void Start()
    {
        /* Initialize Bodyparts and Patient Here */
/*
        head = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(0).gameObject, 0.7f, 1f, new Vector2(-0.5f, 2f));
        chest = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(1).gameObject, 0.5f, 1f, new Vector2(-0.7f, -6.6f));
        leftLeg = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(2).gameObject, 0.1f, 1f, new Vector2(-4.7f, -17.1f));
        rightLeg = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(3).gameObject, 0.1f, 1f, new Vector2(3.7f, -17.1f));
        leftArm = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(4).gameObject, 0.1f, 1f, new Vector2(-6.1f, -6.9f));
        rightArm = Bodypart.MakeBodypartObject(this.gameObject.transform.GetChild(5).gameObject, 0.1f, 1f, new Vector2(4.7f, -6.9f));

        Bodypart[] bodyparts = { head, chest, leftLeg, rightLeg, leftArm, rightArm };
        patient = Patient.MakePatientObject(this.gameObject, bodyparts, 1f);

        /* Create injuries with their respective treatments here*/
/*
        Injury laceration = new Injury(2f, new Vector2(leftLeg.GetLocation().x + 1.3f, leftLeg.GetLocation().y));
        laceration.AddTreatment(ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject.transform.GetChild(2).gameObject, laceration));
        laceration.AddTreatment(GauzeTreatment.MakeGauzeTreatmentObject(this.gameObject.transform.GetChild(2).gameObject, laceration));
        laceration.AddTreatment(DressTreatment.MakeDressTreatmentObject(this.gameObject.transform.GetChild(2).gameObject, laceration));
        leftLeg.AddInjury(laceration);

        Injury chestCompression = new Injury(2f, chest.GetLocation());
        chestCompression.AddTreatment(ChestTreatment.MakeChestTreatmentObject(this.gameObject.transform.GetChild(0).gameObject, chestCompression));
        chest.AddInjury(chestCompression);

        //this.gameObject.GetComponent<BodypartSwitch>().SwitchHead();
    }




}*/