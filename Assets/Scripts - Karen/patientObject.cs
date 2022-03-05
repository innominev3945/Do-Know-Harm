//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptable Objects/Patient")]
public class patientObject : ScriptableObject
{
    [SerializeField] private TextAsset[] txtAssets;
    //[SerializeField] private string[] injuries;
    [SerializeField] private bool[] isInjury_notText;
    private int numTextAssets;

    private int numInjuries;

    private int currentText = 0;

    private int currentInjury = 0;

    private int total_text_and_injury;

    private int currentIndex;

    private void Awake() 
    {
        numTextAssets = txtAssets.Length;
        //numInjuries = Injuries.Length;
        //total_text_and_injury = numInjuries + numTextAssets;
    }

    //CALL THIS BEFORE CALLING IS INJURY
    public bool isEnd()
    {
        if(currentIndex == total_text_and_injury - 2)
        {
            return true;
        }
        return false;
    }

    //CHECKS IF NEXT THING TO DO IS AN INJURY TREATMENT
    //TRUE IF INJURY IS NEXT
    public bool isInjury()
    {
        currentIndex++;
        return(isInjury_notText[currentIndex-1]);
    }

    //CALL THIS FUNCTION IF isInjury returns true
    //use string to make an injury play
    /*public string getNextInjuryName()
    {
        currentInjury++;
        return (injuries[currentInjury-1]);
    }*/

    //CALL THIS FUNCTION IF isInjury returns false
    //SEND THE RESULT OF THIS TEXT ASSET INTO THE DIALOGUE MANAGER FOR GAMEPLAY playText(TextAsset) - Karen will update it later
    public TextAsset getText()
    {
        currentIndex++;
        return(txtAssets[currentIndex-1]);
    }

    //SEND THE RESULT OF THIS TEXT ASSET INTO THE DIALOGUE MANAGER FOR GAMEPLAY playText(TextAsset) - Karen will update it later
    public TextAsset get_Intro_text()
    {
        return txtAssets[0];
    }

    //CALL THIS FUNCTION IF PATIENT HAS DIED
    //SEND THIS INTO DIALOGUE MANAGER FOR GAMEPLAY endPatient(TextAsset) - Karen will update it later
    //When text finishes, the patient should be marked as completed (and dead or alive marked for later purposes)
    public TextAsset getFailText()
    {
        return(txtAssets[numTextAssets-1]);
    }

    //CALL THIS FUNCTION IF isEnd() returns true and patient hasn't died
    //SEND THIS INTO DIALOGUE MANAGER FOR GAMEPLAY endPatient(TextAsset) - Karen will update it later
    //When text finishes, the patient should be marked as completed (and dead or alive marked for later purposes)

    public TextAsset getSuccessText()
    {
        return(txtAssets[numTextAssets-2]);
    }



    //private void OnEnable() {}

    //private void OnDisable() {}

    //private void OnDestroy() {}
}
