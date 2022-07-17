using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;
using System.IO;

public class GameplayDialogueManager : MonoBehaviour
{
    //Scripts needed
    [SerializeField] DialogueRunner dialogue_runner;
    [SerializeField] LineView lineViewer;
    

    
    //Yarn scenes
    [SerializeField] YarnProject[] chapters;
    [SerializeField] string[] dialogueTitles;


    //saved scenes
    //GABE - set this, may need to meet up with Calvin + Karen to figure out the numbering system
    public static int chapterID = 0;
    public static int dialogueID = 0;


    //current scene number (set before starting scene)


    //starts based on current scene
    void Start()
    {        
        LoadDialogueSegments(chapterID, dialogueID);
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


    

    //Loads scene specified by sceneNumber
    public void LoadDialogueSegments(int chapterID, int segmentID)
    {
        dialogue_runner.SetProject(chapters[chapterID]);
        dialogue_runner.StartDialogue(dialogueTitles[segmentID]);
    }
 
}
