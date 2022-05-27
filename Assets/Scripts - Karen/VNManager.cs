using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;
using System.IO;

public class VNManager : MonoBehaviour
{
    //Scripts needed
    [SerializeField] LineView2 lineViewer;
    [SerializeField] AudioManager audio;
    [SerializeField] SpriteRenderer fadeScreen;
    [SerializeField] DialogueRunner dialogue_runner;
    [SerializeField] CharacterManager characters;
    [SerializeField] jsonSaver storage;
    [SerializeField] VNSceneNumbers numbers;
    

    //fading speed
    [SerializeField] private float fadeSpeed;

    
    //Yarn scenes
    [SerializeField] YarnProject[] chapters;
    [SerializeField] string[] chapterTitles;


    [SerializeField] SpriteRenderer Background_Renderer;
    [SerializeField] SpriteRenderer loadingScreen;


    //VN backgrounds
    public Sprite[] Backgrounds;
    public Sprite[] LoadSprites;

    //saved scenes
    private static int savedScene = 0;
    private static int savedLine = 0;

    //VN JSON save file location
    string filepath { get { return Application.persistentDataPath + Path.DirectorySeparatorChar + "playerSave.json" ;} }

    //current scene number (set before starting scene)
    private static int current_scene = 0;

    //starting fade number
    private int currentFade = 0;

    //keep track of fade screen's color
    static Color fadeColor;

    static Color loadColor;

    //checks for a bad fade (user is a click maniac)
    private bool badFade = false;

    //starts based on current scene
    void Start()
    {
        //loadingScreen.activateLoading();
        loadingScreen.sprite = LoadSprites[1];
        
        //Sets the beginning fade screen on or off
        fadeColor = fadeScreen.color;
        current_scene = numbers.getCurrentScene();
        //Loads up the scene signified by current_scene
        //LoadScreen.SetActive(true);
        
        LoadScene(current_scene);
        loadingScreen.sprite = LoadSprites[0];
        
        //loadingScreen.deactivateLoading();
        //LoadScreen.gameObject.SetActive(false);
        //loadSave();
        //resets line tracker to be used for loading and saving stuffs
        lineViewer.resetLineNumber();
        
    }

    
    //to keep track of fading coroutines
    private Coroutine fading;

    //checks wether in process of fading
    private bool isFading {get{return fading != null;}}

    private Coroutine skipping;

    //checks wether in process of fading
    private bool isSkipping {get{return fading != null;}}


   
    // changes background of VN
    public void changeBG(int n){
        Background_Renderer.sprite = Backgrounds[n];
        return;
    }


    //*************************************
    //fades to black if 0, unfades if 1
    //Format in yarn:
    //<<fade ObjectName fadeDirection>>
    //*************************************
    [YarnCommand("fade")]
    public void Fade(int fadeNumber)
    {    
        lineViewer.setLineText(fadeNumber);
        //lineViewer.toggleFade(fadeNumber);
        if (fadeNumber == 1)
        {   
            currentFade = 1;
            fading = StartCoroutine(FadeFromBlack()); 

        }
        else
        {
            currentFade = 0;
            fading = StartCoroutine(FadeToBlack());

        }
        
    }

    //Sets the beginning fade
    [YarnCommand("StartFade")]
    public void startingFade(int fadeNumber)
    {
        Debug.Log("Starting Fade done");
        lineViewer.setLineText(fadeNumber);
        if (fadeNumber == 1)
        {
            currentFade = 1;
            fadeColor.a = 0;
            fadeScreen.color = fadeColor;
        }
        else
        {
            currentFade = 0;
            fadeColor.a = 1;
            fadeScreen.color = fadeColor;
        }
    }

    //Loads the save file saved by the user
    public void loadSave()
    {
        // loadingScreen.sprite = LoadSprites[1];
        // audio.muteAll();
        // characters.clearCharacters();
        // storage.LoadFromFile(filepath);
        // int saved_scene = 0;
        // if (storage.TryGetValue<float>("$saved_scene", out var output))
        // {
        //     //Debug.Log("output: {output}");
        //     savedScene = Convert.ToInt32(output);
        // }
        // int saved_line = 0;
        // if (storage.TryGetValue<float>("$saved_line", out var output2))
        // {
        //     //Debug.Log("output2: {output2}");
        //     savedLine = Convert.ToInt32(output2);
        // }
        // lineViewer.toggleTypewriter();
        // lineViewer.resetLineNumber();
        // dialogue_runner.Stop();
        // LoadScene(savedScene);
        //int temp_line = 0;
        StartCoroutine(traverseLines());
        Debug.Log("Saved Lines:" + savedLine);
        // if (currentFade == 1)
        // {
        //     Color objectColor = fadeScreen.color;
        //     objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        //     fadeScreen.color = objectColor;
        // }
        // else
        // {
        //     Color objectColor = fadeScreen.color;
        //     objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        //     fadeScreen.color = objectColor;
        // }
        // lineViewer.toggleTypewriter();
        // audio.unmuteAll();

        // loadingScreen.sprite = LoadSprites[0];
        //loadingScreen.deactivateLoading();
        //currLine = savedLine;
    }

    //saves progress
    public void saveLine()
    {
        storage.SetValue("$saved_scene", (float)current_scene);
        storage.SetValue("$saved_line", (float)(lineViewer.getLineNumber() * 2 + 2));
        storage.SaveToFile(filepath);
    }

    //stops fading Coroutine
    private void StopFading()
    {
        if(isFading)
        {
            StopCoroutine(fading);
            fading = null;
        }
    }

    private void stopSkipping()
    {
        if(isSkipping)
        {
            StopCoroutine(skipping);
            skipping = null;
        }
    }


    //Continues the VN scene
    void OnClick(InputValue value)
    {
        if (isFading && badFade)
        {
            StopFading();
            badFade = false;
            Debug.Log("Clicky");
        }
        if (!isFading)
        {   
            if(currentFade == 1 && fadeScreen.color.a > 0.01)
            {
                //Debug.Log("Bad fade detected");
                badFade = true;
                Color objectColor = fadeScreen.color;
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
                fadeScreen.color = objectColor;
                //StopFading();
                //Debug.Log("IsFading: " + isFading);
                //Debug.Log("Current fadeScreen color a 1: " + fadeScreen.color.a);
            }
            else if (currentFade == 0 && fadeScreen.color.a < 0.99)
            {  
                //Debug.Log("Bad fade detected");
                //badFade = true;
                Color objectColor = fadeScreen.color;
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
                fadeScreen.color = objectColor;
                //StopFading();
                //Debug.Log("IsFading: " + isFading);
                //Debug.Log("Current fadeScreen color a 1: " + fadeScreen.color.a);
            }
            else{
                if(value.isPressed)
                {
                    //Debug.Log("Current fadeScreen color a 2: " + fadeScreen.color.a);
                    audio.PlayClick();
                    //Debug.Log("meow");
                    lineViewer.OnContinueClicked();
                    
                    //currLine++;
                    //Debug.Log(currLine);
                }
            }
        }
    }


    //Fades fadeScreen to opaque
    IEnumerator FadeToBlack(){
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime* fadeSpeed)
        {
            fadeColor.a = alpha;
            fadeScreen.color = fadeColor;
            yield return null;
        }
        StopFading();
    }

    //Fades fadeScreen to transparent
    IEnumerator FadeFromBlack(){

        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime* fadeSpeed)
        {
            fadeColor.a = alpha;
            fadeScreen.color = fadeColor;
            yield return null;
        }
        
        StopFading();
    }

    IEnumerator FadeFromLoad(){
        Debug.Log("Fade from load called");
        for (float alpha = 1; alpha >= 0; alpha -= 0.2f)
        {
            loadColor.a = alpha;
            loadingScreen.color = loadColor;
            yield return null;
        }
        
        StopFading();
    }



    //Traverses through to load a scene
    IEnumerator traverseLines()
    {
        loadingScreen.sprite = LoadSprites[1];
        audio.muteAll();
        characters.clearCharacters();
        storage.LoadFromFile(filepath);
        int saved_scene = 0;
        if (storage.TryGetValue<float>("$saved_scene", out var output))
        {
            //Debug.Log("output: {output}");
            savedScene = Convert.ToInt32(output);
        }
        int saved_line = 0;
        if (storage.TryGetValue<float>("$saved_line", out var output2))
        {
            //Debug.Log("output2: {output2}");
            savedLine = Convert.ToInt32(output2);
        }
        lineViewer.toggleTypewriter();
        lineViewer.resetLineNumber();
        dialogue_runner.Stop();
        LoadScene(savedScene);
        int temp_line = 0;
        yield return new WaitForSeconds(0.5f);
        //yield return prepareSkip();
        Debug.Log("B");
        Debug.Log("Traverse Line's savedLine: " + savedLine);
        for (int i = 0; i < savedLine; i++)
        {
            //Debug.Log(i);
            lineViewer.OnContinueClicked();
            yield return new WaitForSeconds(0.06f);
        }
        Debug.Log("C");

         if (currentFade == 1)
        {
            Color objectColor = fadeScreen.color;
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
            fadeScreen.color = objectColor;
        }
        else
        {
            Color objectColor = fadeScreen.color;
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
            fadeScreen.color = objectColor;
        }
        lineViewer.toggleTypewriter();
        audio.unmuteAll();

        loadingScreen.sprite = LoadSprites[0];
        //loadingScreen.deactivateLoading();
        //currLine = savedLine;
        yield return new WaitForSeconds(0.5f);
    }

    // IEnumerator skipLines()
    // {
        
    //     yield return traverseLines();
    //     Debug.Log("C");
    //     if (currentFade == 1)
    //     {
    //         Color objectColor = fadeScreen.color;
    //         objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
    //         fadeScreen.color = objectColor;
    //     }
    //     else
    //     {
    //         Color objectColor = fadeScreen.color;
    //         objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
    //         fadeScreen.color = objectColor;
    //     }
    //     lineViewer.toggleTypewriter();
    //     audio.unmuteAll();

    //     loadingScreen.sprite = LoadSprites[0];
    //     yield return new WaitForSeconds(0.1f);
    // }

    // IEnumerator prepareSkip()
    // {
    //     Debug.Log("A");
    //     loadingScreen.sprite = LoadSprites[1];
    //     audio.muteAll();
    //     characters.clearCharacters();
    //     storage.LoadFromFile(filepath);
    //     int saved_scene = 0;
    //     Debug.Log("Ded");
    //     if (storage.TryGetValue<float>("$saved_scene", out var output))
    //     {
    //         //Debug.Log("output: {output}");
    //         savedScene = Convert.ToInt32(output);
    //     }
    //     int saved_line = 0;
    //     if (storage.TryGetValue<float>("$saved_line", out var output2))
    //     {
    //         //Debug.Log("output2: {output2}");
    //         savedLine = Convert.ToInt32(output2);
    //     }
    //     Debug.Log("Alive");
    //     lineViewer.toggleTypewriter();
    //     lineViewer.resetLineNumber();
    //     dialogue_runner.Stop();
    //     Debug.Log("Deeeeed");
    //     LoadScene(savedScene);
    //     Debug.Log ("hahahaha");
    //     yield return null;
    // }

    IEnumerator setLoadScreen(float alpha)
    {
        Debug.Log("Attempted to turn on load screen");
        loadColor.a = alpha;
        loadingScreen.color = loadColor;
        yield return new WaitForSeconds(0.2f);
    }

    public void setAuto()
    {
        lineViewer.toggleAuto();
    }
    

    //Loads scene specified by sceneNumber
    public void LoadScene(int sceneNumber)
    {
        //DialogueRunner dialogue_runner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        //dialogue_runner.yarnProject = chapters[sceneNumber];
        //dialogue_runner.startNode = chapterTitles[sceneNumber];
        dialogue_runner.SetProject(chapters[sceneNumber]);
        dialogue_runner.StartDialogue(chapterTitles[sceneNumber]);
        //LoadScreen.gameObject.SetActive(false);
    }


    //ExitSce
    [YarnCommand("endScene")]
    public void endScene()
    {
        Debug.Log("meow");
        //EXIT YARN FUNCTION
        //END OF SCRIPT and there aren't any lines playing
            //*****************************************************************************
            //                         TO DO:   EXIT OUT OF SCENE
            GetComponent<ToScene>().LoadSceneDepends();
            //******************************************************************************
        //Or something
    }
 
}
