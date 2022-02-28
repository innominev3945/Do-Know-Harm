using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
public class DialogueManagerForGameplay : MonoBehaviour
{

    //script stores text to be displayed
    List <string> script = new List<string>();
    //lineType stores what kind of line it is (music, bg, scene, name of cahracter, action, etc.)
    //speaking stores who is speaking
    List <string> speaker = new List<string>();

    bool linePlayed;

    [SerializeField] private TextAsset txtAsset;

    [SerializeField] private float fadeSpeed;
    private string txt;

     public Sprite[] characters;

    private int index = 0;

    [SerializeField] SpriteRenderer CharacterRenderer;

    // Start is called before the first frame update
    void Start()
    {
        txt = txtAsset.ToString();
        ReadTextFile();
        
    }
    /**********************************************************************************************
                            READS TEXT FILE INTO 3 LISTS
    ***********************************************************************************************/
    
    
    private void ReadTextFile()
    {
        //string txt = this.TextFileAsset.text;
        
        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // split by newline

        foreach (string line in lines)
        {
            
            //print (line);
            if(!string.IsNullOrEmpty(line))
            {
                
                if (line.StartsWith("["))
                {
                    string special = line.Substring(1, line.IndexOf(']')- 1); //special = [Name] or [SFX]
                    string curr = line.Substring(line.IndexOf(']') + 1); //curr = nameofperson or sfx.mp3

                    speaker.Add(special);
                    script.Add(curr);
                }
            
                //adds previous speaker as current speaker
                else{
                    script.Add(line);     
                    speaker.Add(speaker[speaker.Count-1]);
                   
                    
                }                
            }  
        }
    }

    public void playLine()
    {
        if (index >= script.Count)
        {
            return;
        }
        linePlayed = false;
        if (isSpeaking)
        {
            finishSpeaking();
        }
        while(!linePlayed)
        {
            if(!string.IsNullOrEmpty(script[index]))
            {
                linePlayed = true;
                Say(script[index], speaker[index]);
                if (speaker[index] == "Hannah")
                {
                    CharacterRenderer.sprite = characters[0];
                }
                else
                {
                    CharacterRenderer.sprite = characters[1];
                }
            }
            index++;
        }
        return;
    }
    /************************************************************************************************/
    //link the TMPro for the name of who's speaking with this
    [SerializeField] private TMP_Text speakerName;
    //link the TMPro for the text that they're saying with this
    [SerializeField] private TMP_Text speechText;

    //public static DialogueSystem instance;
    //private bool addictive = false;
    //private int visibleChars;
    //private int maxChars;
    private int visibleChars;
    private int maxChars;
    private Coroutine speaking;
    //private TMP_Text targetText;


    //TEXT SPEED between 0.0f(fast) and 0.2f(slow)
    [SerializeField] float textSpeed = 0.0f;
    public bool isWaitingForUserInput = false;
    public bool isSpeaking {get{return speaking != null;}}
    
    public void Say(string lines, string speaker)
    {
        StopSpeaking();
        speaking = StartCoroutine(SpeakLine(lines, speaker));
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
        //ADDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD MC APPEARANCe
    }

    public void finishSpeaking()
    {
        if (isSpeaking)
        {
            visibleChars = maxChars;
            speechText.maxVisibleCharacters = visibleChars;
            speechText.ForceMeshUpdate();
            StopSpeaking();
            return;
        }
    }

    //Puts rolling text in text box yada yada yada
    IEnumerator SpeakLine(string lines, string speaker)
    {
        speechText.ForceMeshUpdate();

        DetermineSpeaker(speaker);

        visibleChars = 0;
        maxChars = lines.Length;
        speechText.text = lines;

        isWaitingForUserInput = false;

        while (visibleChars <= maxChars)
        {
            speechText.maxVisibleCharacters = visibleChars;
            visibleChars++;
            yield return new WaitForSeconds(textSpeed);
        }
        isWaitingForUserInput = true;
        while(isWaitingForUserInput)
            yield return new WaitForEndOfFrame();
        StopSpeaking();
    }

    IEnumerator FadeOut(SpriteRenderer spriteRenderer){
        while (spriteRenderer.color.a > 0)
        {
            Color objectColor = spriteRenderer.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            spriteRenderer.color = objectColor;
            yield return null;
        }
    }

    IEnumerator FadeIn(SpriteRenderer spriteRenderer){
        while (spriteRenderer.color.a < 1)
        {
            Color objectColor = spriteRenderer.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            spriteRenderer.color = objectColor;
            yield return null;
        }
    }
}
