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
    public static bool[] JournalEntries;
    [SerializeField] SpriteRenderer fadeScreen;

    [SerializeField] private float fadeSpeed;

    [SerializeField] AudioManager audioManager;

    [SerializeField] CharacterManager character;

    [SerializeField] VNSaveFile saver;

    [SerializeField] VNManager vnManager;

    [SerializeField] LineView2 lineViewer;

    [SerializeField] TextMeshProUGUI fadedText;

    [SerializeField] TextMeshProUGUI unfadedText;

    //private SerializedProperty dialogueDisplay;
    static bool isFaded = true;

    //[SerializeField] private DialogueRunner dialogueRunner;


    //*************************************
    //used to fade a screen
    //Format in yarn:
    //<<fade ObjectName>>
    //*************************************
    [YarnCommand("fade")]
    public void Fade(int fadeNumber)
    {
        //LineView.Update();

        //dialogueDisplay = serializedObject.FindProperty("lineText");

        //EditorGUI.PropertyField(TextMeshProUGUI, dialogueDisplay);

        lineViewer.setLineText(fadeNumber);
        //lineViewer.toggleFade(fadeNumber);
        if (isFaded && fadeNumber == 1)
        {   
            //lineViewer.GetComponent<LineView>().LineText = unfadedText;
            //lineViewer.GetComponent<LineText>() = unfadedText;
            //lineViewer.lineText = unfadedText;
            StartCoroutine(FadeFromBlack()); 
        }
        else if (!isFaded && fadeNumber == 0)
        {
            //lineViewer.GetComponent<LineText>() = fadedText;
            lineViewer.lineText = fadedText;
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
        return;
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
        return;
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

    [YarnCommand("unlock")]
    public void unlockEntry(int entry_id)
    {
        JournalEntries[entry_id] = true;
        return;
    }

    [YarnCommand("bg")]
    public void changeBackground(int bg_id)
    {
        vnManager.changeBG(bg_id);
        return;
    }


    [YarnCommand("outfit")]
    public void changeOutfit(int character_number, int outfit_number)
    {
        character.changeOutfit(character_number, outfit_number);
        return;
    }

    [YarnCommand("change_outfit")]
    public void changeOutfits(int character_number, int outfit_number)
    {
        character.changeOutfit(character_number, outfit_number);
        return;
    }

    //[YarnCommand("")]


    [YarnCommand("addPage")]
    public void addPage(int n)
    {
        saver.unlockPage(n);
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


