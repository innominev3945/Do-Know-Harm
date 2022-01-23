using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//displays text


public class DialogueSystem : MonoBehaviour
{
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
        */

        //text finished
        isWaitingForUserInput = true;
        while(isWaitingForUserInput)
            yield return new WaitForEndOfFrame();

        StopSpeaking();
    }

    //to figure out who the speaker is if needed
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
        public Text speakerNameText;
        public Text speechText;
    }

    public GameObject speechPanel {get{return elements.speechPanel;}}
    public Text speakerNameText {get{return elements.speakerNameText;}}
    public Text speechText {get{return elements.speechText;}}
}