//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptable Objects/Patient")]
public class injuryObjects : ScriptableObject
{
    [SerializeField] private TextAsset[] txtAssets;
    //[SerializeField] private string[] procedures;
    [SerializeField] private bool[] isAProcedure;
    private int numTextAssets;

    [SerializeField] private string injuryLocation;
    [SerializeField] private string[] procedureNames;

    private int numProcedures;

    private int currentText = 0;

    private int currentProcedure = 0;

    private int total_text_and_injury;

    private int currentIndex = 0;

    private void Awake() 
    {
        numTextAssets = txtAssets.Length;
        //numProcedures = procedures.Length;
        //total_text_and_injury = numProcedures + numTextAssets;
    }

    //CALL THIS BEFORE CALLING IS INJURY
    public bool isEnd()
    {
        if(currentIndex == total_text_and_injury - 1)
        {
            return true;
        }
        return false;
    }

    //CHECKS IF NEXT THING TO DO IS AN INJURY TREATMENT
    //TRUE IF INJURY IS NEXT
    public bool isProcedure()
    {
        currentIndex++;
        return(isAProcedure[currentIndex-1]);
    }

    //CALL THIS FUNCTION IF isProcedure returns true
    //use string to make an injury play
    /*public string getNextInjuryName()
    {
        currentProcedure++;
        return (procedures[currentProcedure-1]);
    }*/

    //CALL THIS FUNCTION IF isProcedure returns false
    //SEND THE RESULT OF THIS TEXT ASSET INTO THE DIALOGUE MANAGER FOR GAMEPLAY playText(TextAsset) - Karen will update it later
    public TextAsset getText()
    {
        //currentIndex++;
        currentText++;
        return(txtAssets[currentText-1]);
    }

    public string getProcedureName()
    {
        //currentIndex++;
        currentProcedure++;
        return(procedureNames[currentProcedure-1]);
    }
    //SEND THE RESULT OF THIS TEXT ASSET INTO THE DIALOGUE MANAGER FOR GAMEPLAY playText(TextAsset) - Karen will update it later
    /*public TextAsset get_Intro_text()
    {
        return txtAssets[0];
    }
    */
    //CALL THIS FUNCTION IF isEnd() returns true and patient hasn't died
    //SEND THIS INTO DIALOGUE MANAGER FOR GAMEPLAY playText(TextAsset) - Karen will update it later
    //When text finishes, the patient should be marked as completed (and dead or alive marked for later purposes)

    /*public TextAsset getSuccessText()
    {
        return(txtAssets[numTextAssets-1]);
    }*/



    //private void OnEnable() {}

    //private void OnDisable() {}

    //private void OnDestroy() {}
}
