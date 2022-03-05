//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptable Objects/Patient")]
public class patientObject : ScriptableObject
{
    //stores intro script, ending script
    [SerializeField] private TextAsset[] txtAssets;
    //[SerializeField] private string[] injuries;
    [SerializeField] private injuryObjects[] injuries;

    //[SerializeField] private string[] injuryLocations;
    [SerializeField] private bool[] completed_injuries;
    private int numTextAssets;
    private int numInjuries;

    private int currentText = 0;

    private int currentInjury = 0;

    private int total_text_and_injury;

    private int numComplete;

    private int currentIndex;

    private async void Awake() 
    {
        numTextAssets = txtAssets.Length;
        numInjuries = injuries.Length;
        numComplete = numInjuries;
        //total_text_and_injury = numInjuries + numTextAssets;
    }


    public int getNumInjuries()
    {
        return numInjuries;
    }
    //CHECKS IF NEXT THING TO DO IS AN INJURY TREATMENT
    //TRUE IF INJURY IS NEXT
    public injuryObjects getInjuryType(int n)
    {
        //completed_injuries[n] = true;
        return injuries[n];
    }


    public injuryObjects startInjury(int n)
    {
        numComplete--;
        completed_injuries[n] = true;
        return injuries[n];
    }

    public bool injuryStatus(int n)
    {
        return completed_injuries[n];
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

    public int getNumInjuriesLeft()
    {
        return numComplete;
    }

    //private void OnEnable() {}

    //private void OnDisable() {}

    //private void OnDestroy() {}
}
