using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class YarnCommands : MonoBehaviour
{
    [SerializeField] SpriteRenderer fadeScreen;
    [SerializeField] private float fadeSpeed;

    [SerializeField] AudioManager audioManager;

    [SerializeField] CharacterManager character;

    static bool isFaded = true;

    //[SerializeField] private DialogueRunner dialogueRunner;


    //*************************************
    //used to fade a screen
    //Format in yarn:
    //<<fade ObjectName>>
    //*************************************
    [YarnCommand("fade")]
    public void Fade()
    {
        if (isFaded)
        {   
            StartCoroutine(FadeFromBlack()); 
        }
        else
        {
            StartCoroutine(FadeToBlack());
            
        }
    }

    //*************************************
    //used to play a sound effect
    //Format in yarn:
    //<<sfx ObjectName>>
    //or
    //<<SFX ObjectName>>
    //*************************************
    [YarnCommand("sfx")]
    public void setSFX(int sfx_num)
    {
        audioManager.PlaySFX(sfx_num);
    }
    [YarnCommand("SFX")]
    public void setsfx(int sfx_num)
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
    [YarnCommand("expr")]
    public void setExpression(int character_number, int expression_number)
    {
        character.changeExpression(character_number, expression_number);
    }
    [YarnCommand("EXPR")]
    public void setexpression(int character_number, int expression_number)
    {
        character.changeExpression(character_number, expression_number);
    }

    //*************************************
    //used to load in a character (characters change positions for the new character)
    //Format in yarn:
    //<<enter ObjectName CharacterNumber>>
    //*************************************
    [YarnCommand("enter")]
    public void enterCharacter(int character_number)
    {
        character.loadCharacter(character_number);
    }

    //*************************************
    //used to unload a character (characters change position to accommodate for lost character)
    //Format in yarn:
    //<<exit ObjectName CharacterNumber>>
    //*************************************
    [YarnCommand("exit")]
    public void exitCharacter(int character_number)
    {
        character.unloadCharacter(character_number);
    }

    //*************************************
    //replaces the old character with the new character (positions remain the same)
    //Format in yarn:
    //<<replace ObjectName OldCharacterNumber NewCharacterNumber>>
    //*************************************
    [YarnCommand("replace")]
    public void replaceCharacter(int old_character, int new_character)
    {
        character.replaceCharacter(old_character, new_character);
    }

    //*************************************
    //plays specified music as specified by MusicNumber
    //Format in yarn:
    //<<music ObjectName MusicNumber>>
    //*************************************
    [YarnCommand("music")]
    public void changeMusic(int music_number)
    {
        audioManager.PlayBGM(music_number);
    }
    
    //*************************************
    //used to play ambience as specified by AmbienceNumber
    //Format in yarn:
    //<<ambience ObjectName AmbienceNumber>>
    //*************************************
    [YarnCommand("ambience")]
    public void changeAmbience(int ambience_number)
    {
        audioManager.PlayAmbience(ambience_number);
    }

    //*************************************
    //used to change or load a character in the MC spot as speciefied by MCNumber
    //Format in yarn:
    //<<mc ObjectName MCNumber>>
    //*************************************
    [YarnCommand("mc")]
    public void loadMC(int MC)
    {
        character.loadMC(MC);
    }

    //Fades sprite renderer to transparent
    IEnumerator FadeFromBlack(){

        while (fadeScreen.color.a > 0)
        {
            Color objectColor = fadeScreen.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeScreen.color = objectColor;
            yield return null;
        }
    }

    //Fades sprite renderer to opaque
    IEnumerator FadeToBlack(){
        while (fadeScreen.color.a < 1)
        {
            Color objectColor = fadeScreen.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            fadeScreen.color = objectColor;
            yield return null;
        }
    }
}


