using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class GameplayDialogueManager : MonoBehaviour
{
    //Scripts needed
    [SerializeField] DialogueRunner dialogue_runner;
    [SerializeField] LineView lineViewer;
    

    
    //Yarn scenes
    [SerializeField] YarnProject[] chapters;
    [SerializeField] string[] dialogueTitles;

    [SerializeField] AudioManager audio;
    [SerializeField] CharacterManagerForGameplay cmfg;

    [SerializeField] TextMeshProUGUI dialoguetext;
    [SerializeField] TextMeshProUGUI nametext;

    [SerializeField] Image dialogueBox;
    [SerializeField] Image nameBox;
    [SerializeField] Image vnBackground;

    [SerializeField] GameObject ui;


    //saved scenes
    //GABE - set this, may need to meet up with Calvin + Karen to figure out the numbering system
    public static int chapterID = 0;
    public static int dialogueID = 0;


    //current scene number (set before starting scene)


    //starts based on current scene
    void Start()
    {        
        LoadDialogueSegments(chapterID, dialogueID);
        hideVN();
    }


    // //Continues the VN scene
    // void OnClick(InputValue value)
    // {
    //     if(value.isPressed)
    //     {
    //         //Debug.Log("Current fadeScreen color a 2: " + fadeScreen.color.a);
    //         //Debug.Log("meow");
    //         lineViewer.OnContinueClicked();
            
    //         //currLine++;
    //         //Debug.Log(currLine);
    //     }
    // }

    public void hideVN(){
        audio.muteAll();
        cmfg.hideMC();
        Color objectColor = dialogueBox.color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        dialogueBox.color = objectColor;

        objectColor = nameBox.color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        nameBox.color = objectColor;

        objectColor = vnBackground.color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        vnBackground.color = objectColor;

        ui.GetComponent<Canvas>().enabled = false;

        nametext.text = "";
        dialoguetext.text = "";
    }

    public void showVN(){
        audio.unmuteAll();
        cmfg.showMC();

        Color objectColor = dialogueBox.color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        dialogueBox.color = objectColor;

        objectColor = nameBox.color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        nameBox.color = objectColor;

        objectColor = vnBackground.color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0.75f);
        vnBackground.color = objectColor;

        ui.GetComponent<Canvas>().enabled = true;

    }



    //Loads scene specified by sceneNumber
    public void LoadDialogueSegments(int chapterID, int segmentID)
    {
        dialogue_runner.SetProject(chapters[chapterID]);
        dialogue_runner.StartDialogue(dialogueTitles[segmentID]);
    }
 
}
