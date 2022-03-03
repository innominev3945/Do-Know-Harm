using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodypartClass;
using PatientClass;
using InjuryClass;
using TreatmentClass;
//using WoundDressingClass;
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
    Bodypart leg;
    //Treatment dress;
    Treatment compress;
    Treatment burn;
    Treatment forceps;
    Treatment gauze;
    Injury laceration;
    Bodypart[] bodyparts;

    // Start is called before the first frame update
    void Start()
    {

        leg = Bodypart.MakeBodypartObject(this.gameObject, 0.2f, 1f); // Create a Bodypart object and add it to the gameObject - remember to pass in this.gameObject to the "constructor"

        laceration = new Injury(2f, this.gameObject.transform.position, 5f); // Create a Laceration injury and add it to the gameObject 


        //dress = WoundDressing.MakeWoundDressingObject(this.gameObject, laceration, 2880);
        //compress = ChestCompression.MakeChestCompressionObject(this.gameObject, laceration, 30, 0.5f, 0.1f);

        //laceration.AddTreatment(dress);
        //laceration.AddTreatment(/*dress*/ compress);
        forceps = ForcepsTreatment.MakeForcepsTreatmentObject(this.gameObject, laceration);
        burn = BurnTreatment.MakeBurnTreatmentObject(this.gameObject, laceration);
        gauze = GauzeTreatment.MakeGauzeTreatmentObject(this.gameObject, laceration);
        compress = ChestTreatment.MakeChestTreatmentObject(this.gameObject, laceration);

        laceration.AddTreatment(compress);
        laceration.AddTreatment(gauze);
        laceration.AddTreatment(forceps);
        laceration.AddTreatment(burn);


        leg.AddInjury(laceration); // Add the injury to the intended BodyPart - note that this can be done at any time; it doesn't necessarily need to be at the start of the program

        bodyparts = new Bodypart[1]; // Create an array of BodyParts (in this case only a single Bodypart) to be added to the Patient 

        bodyparts[0] = leg; // Add the Leg BodyPart to the array 

        hero = Patient.MakePatientObject(this.gameObject, bodyparts, 1.0f); // Add the array of BodyParts to the Patient when creating them (in a standard situation you'd have more BodyParts)

    }

    // Update is called once per frame
    void Update()
    {
        if (!laceration.GetBeingTreated())
            laceration.Treat();
        //Debug.Log("Health: " + hero.GetHealth());
        //Debug.Log(thermalOintment.GetComponent<Thermal_Ointment_Script>().GetHealed());
    }
}