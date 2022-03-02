using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;
//plays script

public class DialogueManager : MonoBehaviour
{
    //private SceneManager m_SceneManager;
    //DialogueSystem dialogue;

    DialogueSystem dialogue;
    CharacterManager character;
    VNManager vnmanager;
    AudioManager audioManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private float fadeSpeed;

    private string[] tempNumbers;
    
    private bool isBlack = true;

    //script stores text to be displayed
    List <string> script = new List<string>();
    //lineType stores what kind of line it is (music, bg, scene, name of cahracter, action, etc.)
    List <char> lineType = new List<char>();
    //speaking stores who is speaking
    List <string> speaking = new List<string>();

    [SerializeField] private TextAsset txtAsset;
    private string txt;

    int index = 0;

    bool readingSave = false;

    // Start is called before the first frame update (DELETE LATER, will need to start externally)
    void Start()
    {
        isBlack = true;
        dialogue = GetComponent<DialogueSystem>();
        character = GetComponent<CharacterManager>();
        vnmanager = GetComponent<VNManager>();
        audioManager = GetComponent<AudioManager>();
        txt = txtAsset.ToString();
        ReadTextFile();
        playLine();
    }

    void StartScene(TextAsset chapter)
    {
        isBlack = true;
        dialogue = GetComponent<DialogueSystem>();
        character = GetComponent<CharacterManager>();
        vnmanager = GetComponent<VNManager>();
        audioManager = GetComponent<AudioManager>();
        txt = txtAsset.ToString();
        ReadTextFile();
        playLine();
    }
    /**********************************************************************************************
                            READS TEXT FILE INTO 3 LISTS
    ***********************************************************************************************/
    
    void loadVNScene(int savedIndex, TextAsset chapterScript)
    {
        txt = chapterScript.ToString();
        readingSave = true;
        index = savedIndex;
        ReadTextFile();
        playLine();
    }
    
    private void ReadTextFile()
    {
        //string txt = this.TextFileAsset.text;

        bool addedAgain = false;
        
        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // split by newline

        int counter = 0;

        foreach (string line in lines)
        {
            
            //print (line);
            if(!addedAgain)
            {
                
                if (line.StartsWith("["))
                {
                    string special = line.Substring(1, line.IndexOf(']')- 1); //special = [Name] or [SFX]
                    string curr = line.Substring(line.IndexOf(']') + 1); //curr = nameofperson or sfx.mp3
                    //sound effect in following format:
                    //[SFX]sfx.mp3
                    //may add text to show user what sound effect it is
                    if (special == "SFX")
                    {
                        lineType.Add('S');
                        script.Add(curr);
                        speaking.Add(" ");
                    }
                    //character expression (facial expression in following format)
                    //[EXPRESSION]expressionType
                    else if (special == "EXPRESSION")
                    {
                        tempNumbers = curr.Split(null);
                        lineType.Add('A');
                        script.Add(tempNumbers[1]);
                        speaking.Add(tempNumbers[0]);
                    }
                    //thought in following format:
                    //[THOUGHT]
                    //thought dialouge... (may be multiple lines)
                    else if (special == "THOUGHT")
                    {
                        
                        lineType.Add('E');
                        script.Add(curr);
                        speaking.Add(" ");
                    }
                    //Music change in following format:
                    //[MUSIC]MusicName
                    else if(special == "MUSIC")
                    {
                        lineType.Add('M');
                        script.Add(curr);
                        speaking.Add(" ");
                    }
                    //Display Scene title
                    //Scene in following format:
                    //[SCENE]SceneName
                    else if(special == "SCENE")
                    {
                        lineType.Add('T');
                        script.Add(curr);
                        speaking.Add(" ");
                    }
                    //Character shows up
                    //character entrance in following format:
                    //[ENTER]0 or 1 (0 = MC, 1 = LI)
                    else if(special == "ENTER")
                    {
                        lineType.Add('N');
                        script.Add(curr);
                        speaking.Add(" ");
                    }
                    //Character exits
                    //character exit in following format:
                    //[EXIT]0 or 1 (0 = MC, 1 = LI)
                    else if (special == "EXIT")
                    {
                        lineType.Add('X');
                        script.Add(curr);
                        speaking.Add(" ");
                    }
                    else if (special == "REPLACE")
                    {
                        tempNumbers = curr.Split(null);
                        lineType.Add('R');
                        script.Add(tempNumbers[0]);
                        speaking.Add(tempNumbers[1]);
                    }
                    else if (special == "END")
                    {
                        lineType.Add('D');
                        script.Add("");
                        speaking.Add(" ");
                    }
                    else if (special == "BG")
                    {
                        lineType.Add('B');
                        script.Add(curr);
                        speaking.Add(speaking[speaking.Count-1]);
                    }else if (special == "FADE")
                    {
                        lineType.Add('F');
                        script.Add(curr);
                        speaking.Add(" ");
                        if(readingSave)
                        {
                            if (counter < index)
                            {
                                if(curr == "0")
                                {
                                    isBlack = true;
                                }
                                else
                                {
                                    isBlack = false;
                                }
                            }
                        }
                    }else if (special == "MC")
                    {
                        lineType.Add('Z');
                        script.Add(curr);
                        speaking.Add(" ");
                    }
                    //a speaker is speaking in following format
                    //[NAME]
                    //NAME's dialogue (may be multiple lines)
                    else
                    {
                        lineType.Add('E');
                        script.Add("");
                        speaking.Add(special);
                    }
                    
                    
                }
                //dialogue
                else if (!string.IsNullOrEmpty(line))
                {
                    script.Add(line);
                    lineType.Add('L');                   
                    speaking.Add(speaking[speaking.Count-1]);
                }
                
                else{
                    script.Add("");
                    lineType.Add('E'); //empty = new dialogue
                    speaking.Add(speaking[speaking.Count-1]);
                    addedAgain = true;
                    
                }
                addedAgain = true;
            }
       
            else
            {
                addedAgain = false;
            }
            counter++;
        }
    }

    
    int temp;
    int temp2;
    bool isLine = false; //used to make sure a line is shown after each input
    // Update is called once per frame


    void OnClick(InputValue value)
    {
        //END OF SCRIPT
        
        if (value.isPressed)
        {
            playLine();
        }
        
            
    }

    private void playLine()
    {
        if (index >= (script.Count - 1)  && !dialogue.isSpeaking)
            {

                //END OF SCRIPT and there aren't any lines playing
                //*****************************************************************************
                //                         TO DO:   EXIT OUT OF SCENE
                //******************************************************************************
                return;
            }

           
            if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
            {
                if(index >= script.Count)
                {
                    //TODO: EXIT SCENE
                    return;
                }
                while (!isLine){
                    
                    if (lineType[index] == 'F')
                    {
                        //isLine = true;
                        temp = Int32.Parse(script[index]);
                        if(temp == 0)
                        {
                            StartCoroutine(FadeToBlack());
                            isBlack = true;
                            
                        }
                        else
                        {
                            StartCoroutine(FadeFromBlack());
                            isBlack = false;
                            dialogue.FadedSay(" ");
                        }
                    }
                    //play sound effect
                    //TODO: SOUND SYSTEM
                    if (lineType[index] == 'S')
                    {
                        temp = Int32.Parse(script[index]);
                        audioManager.PlaySFX(temp);
                        
                        //m_SceneManager.playSFX(temp);
                    }
                    //dialogue/text to display
                    else if (lineType[index] == 'L')
                    {
                        audioManager.PlayClick();   
                        if (isBlack)
                        {
                            if(lineType[index-1] == 'E')
                            {
                                
                                dialogue.FadedSay(script[index]);
                                isLine = true;
                            }else
                            {
                                
                                dialogue.FadedSayAdd(script[index]);
                                isLine = true;
                            } 
                        }
                        else{
                            if(lineType[index-1] == 'E')
                            {
                                if(speaking[index] == " ")
                                {
                                    //name disappear
                                    dialogue.Say(script[index], speaking[index]);
                                    isLine = true;
                                }
                                else
                                {
                                    //nameplate appear if not already there
                                    dialogue.Say(script[index], speaking[index]);
                                    isLine = true;
                                }
                                //clear speech box and output line
                                dialogue.Say(script[index], speaking[index]);
                                isLine = true;
                                
                            }else
                            {
                                dialogue.SayAdd(script[index], speaking[index]);
                                isLine = true;
                            }
                        }                        
                        
                    }
                    //character expression
                    else if (lineType[index] == 'A')
                    {
                        temp = Int32.Parse(speaking[index]);
                        temp2 = Int32.Parse(script[index]);
                        //changes character number temp to expression number temp2
                        character.changeExpression(temp, temp2);
                    }
                    //display title from script
                    else if (lineType[index] == 'T')
                    {
                        //m_SceneManager.DisplaySceneTitle(script[index]);
                    }
                    //enter character
                    else if (lineType[index] == 'N')
                    {
                        temp = Int32.Parse(script[index]);
                        character.loadCharacter(temp);
                        
                        //m_SceneManager.LoadCharacter(temp);
                    }
                    //exit character
                    else if (lineType[index] == 'X')
                    {
                        temp = Int32.Parse(script[index]);
                        
                        character.unloadCharacter(temp);
                        //m_SceneManager.UnloadCharacter(temp);
                    }
                    //replace character
                    else if (lineType[index] == 'R')
                    {
                        temp = Int32.Parse(script[index]);
                        temp2 = Int32.Parse(speaking[index]);
                        character.replaceCharacter(temp, temp2);
                    }
                    //end scene
                    else if (lineType[index] == 'D')
                    {
                        print("END HAPPENED.");
                        //m_SceneManager.UnloadAllBackgrounds();
                        //m_SceneManager.UnloadAllCharacters();
                        //m_SceneManager.UnloadAllUI();

                    }
                    //change music
                    else if(lineType[index] == 'M')
                    {
                        temp = Int32.Parse(script[index]);
                        audioManager.PlayBGM(temp);
                    }
                    //load background
                    else if (lineType[index] == 'B')
                    {
                        temp = Int32.Parse(script[index]);
                        
                        vnmanager.changeBG(temp);
                    //Load main character
                    }else if (lineType[index] == 'Z')
                    {
                        
                        temp = Int32.Parse(script[index]);
                        character.loadMC(temp);
                    }
                    index++;
                }   
                isLine = false;  
            }else if(dialogue.isSpeaking){
                
                dialogue.finishSpeaking(isBlack);
            }
            return;
    }


    public int getSaveLocation()
    {
        return index;
    }


    IEnumerator FadeFromBlack(){
        
        
        while (spriteRenderer.color.a > 0)
        {
            Color objectColor = spriteRenderer.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            spriteRenderer.color = objectColor;
            yield return null;
        }
    }

    IEnumerator FadeToBlack(){
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
