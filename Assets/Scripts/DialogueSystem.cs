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

    /*private Coroutine speaking;*/

    public bool isSpeaking {get{return speaking != null;}}
    
    void Awake()
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
            speechText.maxVisibleCharacters = visibleChars;
            StopSpeaking();
            return;
        }else if (isSpeaking && !isFaded)
        {
            visibleChars = maxChars;
            speechOnBlack.maxVisibleCharacters = visibleChars;
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
            
    }*/
}
    /*
    private SceneManager m_sceneManager;
    public ELEMENTS elements;
    //public static DialogueSystem instance; //used to located dialogue in other systems

    void Awake()
    {
        m_sceneManager = GameObject.FindObjectOfType<SceneManager>();
        //instance is only dialogue system in this scene
        //instance = this;
    }

    // Say something and show it on the speech box
    public void Say(string speech, string speaker = "")
    {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech, false, speaker));
    }

    // Add to what is already in speech box
    public void SayAdd(string speech, string speaker = "")
    {
        StopSpeaking();
        speechText.text = targetSpeech;
        speaking = StartCoroutine(Speaking(speech, true, speaker));
    }

    

    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }

    public bool isSpeaking {get{return speaking != null;}}
    [HideInInspector] public bool isWaitingForUserInput = false;

    public string targetSpeech = "";
    Coroutine speaking = null;
    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        speechPanel.SetActive(true);
        targetSpeech = speech;

        if (!additive)
            speechText.text = "";
        else
            targetSpeech = speechText.text + "\n" + targetSpeech;


        speakerNameText.text = DetermineSpeaker(speaker); //temporary

        isWaitingForUserInput = false;
        /* TEXT SPEED REVISIT!
        float textSpeedPos = 0f; // Overcomplicated way of implementing text speed, by loading more than 1 character
                                 //  per every frame
        int currentTextPos = 0;  // Stores position in loaded text
        while(speechText.text != targetSpeech)
        {
            textSpeedPos += m_sceneManager.textSpeed; // Add textSpeed # of characters
            while (currentTextPos < textSpeedPos)
            {
                speechText.text += targetSpeech[speechText.text.Length];
                currentTextPos++;
                if (speechText.text == targetSpeech) break; // Don't load too many characters after the end of the speech
            }
            if (!m_sceneManager.instantText) yield return new WaitForEndOfFrame(); // Does text flow instantly?
        }
        

        //text finished
        isWaitingForUserInput = true;
        while(isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopSpeaking();
    }*/

    //to figure out who the speaker is if needed
    /*
    string DetermineSpeaker(string s)
    {
        string retVAl = speakerNameText.text;
        if (s != speakerNameText.text && s != "")
            retVAl = (s.ToLower().Contains("narrator")) ? "" : s;
        return retVAl;
    }

    [System.Serializable]
    public class ELEMENTS
    {
        ///contains all dialogue related elements on the UI
        public GameObject speechPanel;
        public TMPro speakerNameText;
        public TMPro speechText;
    }

    public GameObject speechPanel {get{return elements.speechPanel;}}
    public TMPro speakerNameText {get{return elements.speakerNameText;}}
    public TMPro speechText {get{return elements.speechText;}}
}

}*/