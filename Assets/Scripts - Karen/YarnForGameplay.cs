using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class YarnForGameplay : MonoBehaviour
{
    public static bool[] JournalEntries;



    [SerializeField] AudioManager audioManager;

    [SerializeField] CharacterManagerForGameplay character;

    [SerializeField] LineView lineViewer;

     void OnClick(InputValue value)
    {
        
        if(value.isPressed)
        {
            //Debug.Log("meow");
            lineViewer.OnContinueClicked();
        }
    }



    //private SerializedProperty dialogueDisplay;
    static bool isFaded = true;

    //[SerializeField] private DialogueRunner dialogueRunner;




    //*************************************
    //used to play a sound effect
    //Format in yarn:
    //<<sfx ObjectName>>
    //or
    //<<SFX ObjectName>>
    //*************************************
    [YarnCommand("soundEffect")]
    public void setSoundEffect(int sfx_num)
    {
        audioManager.PlaySFX(sfx_num);
    }


    //*************************************
    //used to change character expression
    //Format in yarn:
    //<<expr ObjectName>>
    //or
    //<<EXPR ObjectName>>
    //*************************************
    [YarnCommand("Expression")]
    public void setExpr(int expression_number)
    {
        character.changeExpression(expression_number);
    }


  

    //*************************************
    //plays specified music as specified by MusicNumber
    //Format in yarn:
    //<<music ObjectName MusicNumber>>
    //*************************************
    [YarnCommand("Music")]
    public void changemusic(int music_number)
    {
        audioManager.PlayBGM(music_number);
    }
    
    //*************************************
    //used to play ambience as specified by AmbienceNumber
    //Format in yarn:
    //<<ambience ObjectName AmbienceNumber>>
    //*************************************
    [YarnCommand("Ambience")]
    public void changeambience(int ambience_number)
    {
        audioManager.PlayAmbience(ambience_number);
        return;
    }

    //*************************************
    //used to change or load a character in the MC spot as speciefied by MCNumber
    //Format in yarn:
    //<<mc ObjectName MCNumber>>
    //*************************************
    [YarnCommand("MC")]
    public void loadmc(int MC)
    {
        character.loadMC(MC);
        return;
    }




    [YarnCommand("Outfit")]
    public void changeoutfit(int outfit_number)
    {
        character.changeOutfit(outfit_number);
        return;
    }

    [YarnCommand("Accessory")]
    public void changeaccessory(int accessory_number)
    {
        character.changeAccessory(accessory_number);
        return;
    }

    [YarnCommand("EndScript")]
    public void endSegment()
    {
        //pass control back to the gameplay (disable self...)
        return;
    }



}

