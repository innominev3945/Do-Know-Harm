using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class VNManager : MonoBehaviour
{
    [SerializeField] LineView2 lineViewer;
    
    [SerializeField] AudioManager audio;
    [SerializeField] SpriteRenderer fadeScreen;
    [SerializeField] private float fadeSpeed;

    private int currentFade = 0;

    static Color fadeColor;

    private bool badFade = false;

    void Start()
    {
        fadeColor = fadeScreen.color;
        //StartCoroutine(FadeToBlack());
    }

    public Sprite[] Backgrounds;

    private Coroutine fading;

    private bool isFading {get{return fading != null;}}

    [SerializeField] SpriteRenderer Background_Renderer;
    // Start is called before the first frame update
    public void changeBG(int n){
        Background_Renderer.sprite = Backgrounds[n];
        return;
    }

    // [YarnCommand("custom_wait")]
    // public void CustomWait()
    // {
    //     waiting = StartCoroutine(waitz());
    // }

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

    // IEnumerator waitz(){
    //     float waitTime = 5.0f;
    //     yield return new WaitForSeconds(waitTime);
    //     StopWaiting();
    // }

    private void StopFading()
    {
        if(isFading)
        {
            StopCoroutine(fading);
            fading = null;
        }
    }



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
                Debug.Log("Bad fade detected");
                badFade = true;
                Color objectColor = fadeScreen.color;
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
                fadeScreen.color = objectColor;
                //StopFading();
                Debug.Log("IsFading: " + isFading);
                Debug.Log("Current fadeScreen color a 1: " + fadeScreen.color.a);
            }
            else if (currentFade == 0 && fadeScreen.color.a < 0.99)
            {  
                Debug.Log("Bad fade detected");
                //badFade = true;
                Color objectColor = fadeScreen.color;
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
                fadeScreen.color = objectColor;
                //StopFading();
                Debug.Log("IsFading: " + isFading);
                Debug.Log("Current fadeScreen color a 1: " + fadeScreen.color.a);
            }
            else{
                if(value.isPressed)
                {
                    Debug.Log("Current fadeScreen color a 2: " + fadeScreen.color.a);
                    audio.PlayClick();
                    //Debug.Log("meow");
                    lineViewer.OnContinueClicked();
                }
            }
        }
    }

    IEnumerator FadeToBlack(){
        Debug.Log("Faded to black");
        // while (fadeScreen.color.a < 1)
        // {
        //     Color objectColor = fadeScreen.color;
        //     float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

        //     objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        //     fadeScreen.color = objectColor;
        //     yield return null;
        // }
        //Color c = fadeScreen.color;
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime* fadeSpeed)
        {
            fadeColor.a = alpha;
            fadeScreen.color = fadeColor;
            yield return null;
        }
        StopFading();
    }

    IEnumerator FadeFromBlack(){

        Debug.Log("Faded from black");
        // while (fadeScreen.color.a > 0)
        // {
        //     Color objectColor = fadeScreen.color;
        //     float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

        //     objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        //     fadeScreen.color = objectColor;
        //     yield return null;
        // }
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime* fadeSpeed)
        {
            fadeColor.a = alpha;
            fadeScreen.color = fadeColor;
            yield return null;
        }
        
        Debug.Log("Completed fade with fadeScreen.color.a: " + fadeScreen.color.a);
        StopFading();
    }
    // Update is called once per frame
    
}
