using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
//plays script

public class DialogueManager : MonoBehaviour
{
    //private SceneManager m_SceneManager;
    //DialogueSystem dialogue;
    static int chapterNumber = 0;

    //CHANGE AS CHAPTERS ADDED
    private int numChapters = 2;
    [SerializeField] private TextAsset[] txtAssets;
    DialogueSystem dialogue;
    CharacterManager character;
    VNManager vnmanager;
    AudioManager audioManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private float fadeSpeed;

    private string[] tempNumbers;
    private int numCharacters;
    
    private bool isBlack = true;
    private int MCnum;
    private int startingBGM;

    private int startingBG;

    private bool[] loadedCharacters;

    //script stores text to be displayed
    List <string> script = new List<string>();
    //lineType stores what kind of line it is (music, bg, scene, name of cahracter, action, etc.)
    List <char> lineType = new List<char>();
    //speaking stores who is speaking
    List <string> speaking = new List<string>();

    //[SerializeField] private TextAsset txtAsset;
    private string txt;

    int index = 0;

    bool readingSave = false;
    int temp;
    int temp2;
    bool isLine = false; //used to make sure a line is shown after each input
    // Update is called once per frame

    void Start()
    {
        isBlack = true;
        dialogue = GetComponent<DialogueSystem>();
        character = GetComponent<CharacterManager>();
        vnmanager = GetComponent<VNManager>();
        audioManager = GetComponent<AudioManager>();
        txt = txtAssets[chapterNumber].ToString();
        numCharacters = character.getCharacterNumber();
        loadedCharacters = new bool[numCharacters];
        for (int i = 0; i < numCharacters; i++)
        {
            loadedCharacters[i] = false;
        }
        ReadTextFile();
        playLine();
    }


    public void StartScene(int chapterNum)
    {
        chapterNumber = chapterNum;
        script.Clear();
        lineType.Clear();
        speaking.Clear();
        isBlack = true;
        dialogue = GetComponent<DialogueSystem>();
        character = GetComponent<CharacterManager>();
        vnmanager = GetComponent<VNManager>();
        audioManager = GetComponent<AudioManager>();
        txt = txtAssets[chapterNumber].ToString();
        ReadTextFile();
        
        
        playLine();
    }
    /**********************************************************************************************
                            READS TEXT FILE INTO 3 LISTS
    ***********************************************************************************************/
    
    public void loadVNScene(int savedIndex, int chapterNum)
    {
        chapterNumber = chapterNum;
        txt = txtAssets[chapterNumber].ToString();
        isBlack = false;
        script.Clear();
        lineType.Clear();
        speaking.Clear();
        dialogue = GetComponent<DialogueSystem>();
        character = GetComponent<CharacterManager>();
        vnmanager = GetComponent<VNManager>();
        audioManager = GetComponent<AudioManager>();

        numCharacters = character.getCharacterNumber();
        loadedCharacters = new bool[numCharacters];
        for (int i = 0; i < numCharacters; i++)
        {
            loadedCharacters[i] = false;
        }

        readingSave = true;
        index = savedIndex;
        ReadTextFile();
        character.loadMC(MCnum);
        Debug.Log(isBlack);
        if(!isBlack)
        {
            StartCoroutine(FadeFromBlack());
        }

        

        for (int i = 0; i < numCharacters; i++)
        {
            if (loadedCharacters[i])
            {
                character.loadCharacter(i);
            }
        }
        vnmanager.changeBG(startingBG);
        audioManager.PlayBGM(startingBGM);
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
                        if(counter < 3)
                        {
                            speaking.Add(" ");
                        }
                        else
                        {
                            speaking.Add(speaking[speaking.Count-1]);
                        }
                        if (readingSave)
                        {
                            startingBGM = Int32.Parse(curr);
                        }
                    }
                    else if(special == "AMBIENCE")
                    {
                        lineType.Add('M');
                        script.Add(curr);
                        speaking.Add(speaking[speaking.Count-1]);
                        if (readingSave)
                        {
                            startingBGM = Int32.Parse(curr);
                        }
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
                        speaking.Add(speaking[speaking.Count-1]);
                        if (readingSave)
                        {
                            temp = Int32.Parse(curr);
                            //Debug.Log(loadedCharacters);
                            loadedCharacters[temp] = true;
                        }
                    }
                    //Character exits
                    //character exit in following format:
                    //[EXIT]0 or 1 (0 = MC, 1 = LI)
                    else if (special == "EXIT")
                    {
                        lineType.Add('X');
                        script.Add(curr);
                        speaking.Add(speaking[speaking.Count-1]);
                        if (readingSave)
                        {
                            temp = Int32.Parse(curr);
                            //Debug.Log(temp);
                            loadedCharacters[temp] = false;
                        }
                    }
                    else if (special == "REPLACE")
                    {
                        tempNumbers = curr.Split(null);
                        lineType.Add('R');
                        script.Add(tempNumbers[0]);
                        speaking.Add(tempNumbers[1]);
                    }
                    //DELETE IF UNECESSARY
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
                        if(counter < 3)
                        {
                            speaking.Add(" ");
                        }
                        else
                        {
                            speaking.Add(speaking[speaking.Count-1]);
                        }
                        if (readingSave)
                        {
                            startingBG = Int32.Parse(curr);
                        }
                    }else if (special == "FADE")
                    {
                        lineType.Add('F');
                        script.Add(curr);
                        speaking.Add(speaking[speaking.Count-1]);
                        if(readingSave)
                        {
                            if ((counter/2) <= index)
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
                        speaking.Add(speaking[speaking.Count-1]);
                        if (readingSave)
                        {
                            MCnum = Int32.Parse(curr);
                        }
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
                    if(counter < 3)
                    {
                        speaking.Add(" ");
                    }
                    else
                    {
                        speaking.Add(speaking[speaking.Count-1]);
                    }
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
        if ((index >= (script.Count - 1))  && !dialogue.isSpeaking)
        {
            //CHANGE AS NECESSARY, IF STATEMENTS FOR GAMEPLAY
            chapterNumber++;
            if (chapterNumber == 2)
            {
                Debug.Log("Check");
            }
            Debug.Log("Step before scene call");
            
            if (chapterNumber < 2)
            {
                index = 0;
                Debug.Log("Scene Loading called");
                SceneManager.LoadScene("VNScene");
            }
            //END OF SCRIPT and there aren't any lines playing
            //*****************************************************************************
            //                         TO DO:   EXIT OUT OF SCENE
            //GetComponent<ToScene>().LoadSceneByName("Do Know Harm - Vivek");
            //******************************************************************************
            return;
        }

           
          
        if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
        {
            if(index >= ((script.Count - 1)))
            {
                dialogue.finishSpeaking(isBlack);
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
                else if(lineType[index] == 'C')
                {
                    temp = Int32.Parse(script[index]);
                    audioManager.PlayAmbience(temp);
                }
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
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
                else if (lineType[index] == 'E'){
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

    public int getSaveChapter()
    {
        return chapterNumber;
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
