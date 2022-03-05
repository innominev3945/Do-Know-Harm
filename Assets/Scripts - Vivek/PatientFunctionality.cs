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
    //Treatment dress;
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

        laceration = new Injury(2f, new Vector2(1.4f, -3.29f), 5f); // Create a Laceration injury and add it to the gameObject 
        compressionInj = new Injury(0.1f, new Vector2(-0.08f, -2.7f), 1f);
        burnInj = new Injury(0.1f, new Vector2(-0.13f, -3.79f), 1f);



        //dress = DressTreatment.MakeDressTreatmentObject(this.gameObject, laceration);
        forceps = ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject, laceration);
        burn = BurnTreatment.MakeBurnTreatmentObject(this.gameObject, burnInj);
        gauze = GauzeTreatment.MakeGauzeTreatmentObject(this.gameObject, laceration);
        compress = ChestTreatment.MakeChestTreatmentObject(this.gameObject, compressionInj);

        //laceration.AddTreatment(dress);
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
        if (!laceration.GetBeingTreated())
            laceration.Treat();
        if (laceration.GetInjurySeverity() == 0)
            burnInj.Treat();
        if (burnInj.GetInjurySeverity() == 0)
            compressionInj.Treat();
        //Debug.Log("Health: " + hero.GetHealth());
        //Debug.Log(thermalOintment.GetComponent<Thermal_Ointment_Script>().GetHealed());
    }
}