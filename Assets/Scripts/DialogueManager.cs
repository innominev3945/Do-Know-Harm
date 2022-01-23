using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//plays script

public class DialogueManager : MonoBehaviour
{
    private SceneManager m_SceneManager;
    DialogueSystem dialogue;

    //script stores text to be displayed
    new List <string> script = new List<string>();
    //lineType stores what kind of line it is (music, bg, scene, name of cahracter, action, etc.)
    new List <char> lineType = new List<char>();
    //speaking stores who is speaking
    new List <string> speaking = new List<string>();

    [SerializeField] private TextAsset txtAsset;
    private string txt;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = DialogueSystem.instance;
        txt = txtAsset.ToString();
        ReadTextFile();
    }
    /**********************************************************************************************
                            READS TEXT FILE INTO 3 LISTS
    ***********************************************************************************************/
    
    private void ReadTextFile()
    {
        //string txt = this.TextFileAsset.text;

        bool addedAgain = false;
        
        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // split by newline

        int counter = 0;

        foreach (string line in lines)
        {
            addedAgain = false;
            
            if(!string.IsNullOrEmpty(line))
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
                        lineType.Add('A');
                        script.Add(curr);
                        speaking.Add(speaking[speaking.Count-1]);
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
                else
                {
                    script.Add(line);
                    lineType.Add('L');                   
                    speaking.Add(speaking[speaking.Count-1]);
                }
                
            //separate new text box by empty line
            }            
            else if (counter % 2 == 0)
            {
                script.Add("");
                lineType.Add('E'); //empty = new dialogue
                speaking.Add(speaking[speaking.Count-1]);
            }
            counter++;
        }
    }

    int index = 0;
    int temp;
    bool isLine = false; //used to make sure a line is shown after each input
    // Update is called once per frame
    void Update()
    {
        /**********************************************************************************************
                            Next lines/things to do in VN based on mouse button click
    ***********************************************************************************************/
        if (Input.GetMouseButtonDown(0))
        {
            isLine = false;
            while (!isLine){
                if (!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
                {
                    //END OF SCRIPT
                    if (index >= script.Count)
                    {
                        return;
                    }
                    //play sound effect
                    if (lineType[index] == 'S')
                    {
                        temp = Int32.Parse(script[index]);
                        print("SFX: ");
                        print(temp);
                        
                        //m_SceneManager.playSFX(temp);
                    }
                    //dialogue/text to display
                    else if (lineType[index] == 'L')
                    {
                        isLine = true;
                        if(lineType[index-1] == 'E')
                        {
                            if(speaking[index] == " ")
                            {
                                //nameplate diappear
                                dialogue.Say(script[index], speaking[index]);
                            }
                            else
                            {
                                //nameplate appear if not already there
                                dialogue.Say(script[index], speaking[index]);
                            }
                            //clear speech box and output line
                            dialogue.Say(script[index], speaking[index]);
                            
                        }else
                        {
                            dialogue.SayAdd(script[index], speaking[index]);
                        }                        
                        
                    }
                    //character expression
                    else if (lineType[index] == 'A')
                    {
                        temp = Int32.Parse(script[index]);
                        if(speaking[index] == "Toa")
                        {
                            print("TOA EXPRESSION: ");
                            print(temp);
                            //TODO: CHARACTER FACIAL FEATURE CHANGE FROM CHARACTER MANAGER
                        }else //STUART
                        {
                            print("STUART EXPRESSION: ");
                            print(temp);
                            //TODO: CHARCTER FACIAL FEATURE CHANGE FROM CHARACTER MANAGER
                        }
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
                        print("ENTER CHARACTER NUMBER: ");
                        print(temp);
                        //m_SceneManager.LoadCharacter(temp);
                    }
                    //exit character
                    else if (lineType[index] == 'X')
                    {
                        temp = Int32.Parse(script[index]);
                        print("EXIT CHARACTER: ");
                        print(temp);
                        //m_SceneManager.UnloadCharacter(temp);
                    }
                    //end scene
                    else if (lineType[index] == 'D')
                    {
                        print("END HAPPENED.");
                        //m_SceneManager.UnloadAllBackgrounds();
                        //m_SceneManager.UnloadAllCharacters();
                        //m_SceneManager.UnloadAllUI();

                    }
                    else if (lineType[index] == 'B')
                    {
                        temp = Int32.Parse(script[index]);
                        print("BG LOADED: ");
                        print(temp);
                        //m_SceneManager.LoadBackground(temp);
                    }
                    
                    
                                                     
                    index++;
                    
                }
            }
        }
    }
}