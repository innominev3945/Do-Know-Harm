using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BodypartClass;
using PatientClass;
using InjuryClass;
using LacerationClass;
using UnityEngine.EventSystems;

// Driver program demonstrating Patient, Bodypart, Injury, and Laceration
// To use it, click on the sprite and move your mouse around it 8 times 
public class PatientFunctionality : MonoBehaviour
{
    Patient hero;
    Bodypart leg;
    Laceration laceration; 

    // Start is called before the first frame update
    void Start()
    {
        
        leg = Bodypart.MakeBodypartObject(this.gameObject, 0.2f, 1f); // Create a Bodypart object and add it to the gameObject - remember to pass in this.gameObject to the "constructor"
        
        laceration = Laceration.MakeLacerationObject(this.gameObject, 1f, this.gameObject.transform.position, 2880); // Create a Laceration injury and add it to the gameObject 
        
        leg.AddInjury(laceration); // Add the injury to the intended BodyPart - note that this can be done at any time; it doesn't necessarily need to be at the start of the program

        Bodypart[] bodyparts = new Bodypart[1]; // Create an array of BodyParts (in this case only a single Bodypart) to be added to the Patient 

        bodyparts[0] = leg; // Add the Leg BodyPart to the array 

        hero = Patient.MakePatientObject(this.gameObject, bodyparts, 1.0f); // Add the array of BodyParts to the Patient when creating them (in a standard situation you'd have more BodyParts)
        
        laceration.Treat(); // In this case, the Injury treatment is being directly started through the program, though in a gameplay it would be more likely for the user to actually select on the Injury 
        // to start its treatment 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Health: " + hero.GetHealth());
    }
}
