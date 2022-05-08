/*
//Built off Unity TMPro scrolling text example and Stella Studio's Visual Novel tutorial
//
//Modified by: Karen Jin
//Last Modified:
//Reveals text one by one or reveals the rest of the text on additional click

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text speechText;
    [SerializeField] private TMP_Text speakerName;

    [SerializeField] private TMP_Text speechOnBlack;
    

    //public static DialogueSystem instance;
    //private bool addictive = false;
    //private int visibleChars;
    //private int maxChars;
    private int visibleChars;
    private int maxChars;
    private Coroutine speaking;
    //private TMP_Text targetText;
    // Start is called before the first frame update

    //TEXT SPEED between 0.0f(fast) and 0.2f(slow)
    [SerializeField] float textSpeed = 0.0f;
    public bool isWaitingForUserInput = false;

    /*private Coroutine speaking;

    public bool isSpeaking {get{return speaking != null;}}
    
    /*    void Awake()
    {
        //instance = this;
        //Say(lines, speaker);
        //speechText = GetComponent<TMP_Text>();
    }
     // 

    
    public void FadedSay(string lines)
    {
        StopSpeaking();
        speaking = StartCoroutine(SpeakLine(lines, " ", false, speechOnBlack));

    }

    public void FadedSayAdd(string lines)
    {
        StopSpeaking();
        speaking = StartCoroutine(SpeakLine(lines, " ", true, speechOnBlack));
    }


    public void Say(string lines, string speaker)
    {
        StopSpeaking();
        speaking = StartCoroutine(SpeakLine(lines, speaker, false, speechText));
    }  

    public void SayAdd(string lines, string speaker)
    {
        StopSpeaking();
        speaking = StartCoroutine(SpeakLine(lines, speaker, true, speechText));
    }

    private void StopSpeaking()
    {
        if(isSpeaking)
        {
            StopCoroutine(speaking);
            speaking = null;
        }
    }

    

    private void DetermineSpeaker(string speaker)
    {
        string currSpeaker = speakerName.text;
        if (currSpeaker != speaker)
        {
            speakerName.text = speaker;
        }
    }

    public void finishSpeaking(bool isFaded)
    {
        if (isSpeaking && isFaded)
        {
            visibleChars = maxChars;
            speechOnBlack.maxVisibleCharacters = visibleChars;
            speechOnBlack.ForceMeshUpdate();
            StopSpeaking();
            return;
        }else if (isSpeaking && !isFaded)
        {
            visibleChars = maxChars;
            speechText.maxVisibleCharacters = visibleChars;
            speechText.ForceMeshUpdate();
            StopSpeaking();
            return;
        }
    }
    IEnumerator SpeakLine(string lines, string speaker, bool addictive, TMP_Text speechBox)
    {
        speechBox.ForceMeshUpdate();

        DetermineSpeaker(speaker);
        
        
        
        if (!addictive)
        {
            visibleChars = 0;
            maxChars = lines.Length;
            speechBox.text = lines;
        }else
        {
            visibleChars = speechBox.text.Length;
            
            speechBox.text = speechBox.text + "\n" + lines;
            maxChars = speechBox.text.Length;
        }
        isWaitingForUserInput = false;
        
        
        
        while (visibleChars <= maxChars)
        {
            speechBox.maxVisibleCharacters = visibleChars;
            visibleChars++;
            yield return new WaitForSeconds(textSpeed);
        }
        isWaitingForUserInput = true;
        while(isWaitingForUserInput)
            yield return new WaitForEndOfFrame();
        StopSpeaking();
    }

    
    /*void Update()
    {        
            
    }
}
    */