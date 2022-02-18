using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;



public class CheckPulse1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    public SpriteRenderer hand;
    public Sprite pulseHand;
    public Sprite defaultHand;
    public GameObject heartPanel;
    public VideoPlayer videoPlayer;

    public float timeStart;
    public Text textbox;
    public GameObject showText;
    bool timerActive = false;

    bool hasPulse = true;
    string pulseResult = "";


    void Start()
    {
        heartPanel.SetActive(false);
        showText.SetActive(false);

        textbox.text = "Checking pulse";
    }



    void Update()
    {
        if (hasPulse)
            pulseResult = "Pulse detected!";
        else
            pulseResult = "No pulse detected";

        if(timerActive)
        {
            timeStart += Time.deltaTime;

            if (timeStart < 0.5)
                textbox.text = "Checking pulse";
            if (timeStart < 1 && timeStart > 0.5)
                textbox.text = "Checking pulse.";
            if (timeStart < 1.5 && timeStart > 1)
                textbox.text = "Checking pulse..";
            if (timeStart < 2 && timeStart > 1.5)
                textbox.text = "Checking pulse...";
            if (timeStart < 2.5 && timeStart > 2)
                textbox.text = "Checking pulse";
            if (timeStart < 3 && timeStart > 2.5)
                textbox.text = "Checking pulse.";
            if (timeStart < 3.5 && timeStart > 3)
                textbox.text = "Checking pulse..";
            if (timeStart < 4 && timeStart > 3.5)
                textbox.text = "Checking pulse...";
            if (timeStart < 4.5 && timeStart > 4)
                textbox.text = "Checking pulse";
            if (timeStart > 4.5)
                textbox.text = pulseResult;
        }

    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
        hand.sprite = pulseHand;


    }

    public void OnPointerDown(PointerEventData eventData)
    {

        hand.sortingLayerName = "Default";
        hand.sortingOrder = 0;
       
        //probably not needed
       /* if (hasPulse)
        {
            heartPanel.SetActive(true);
            RenderTexture.active = videoPlayer.targetTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }
       */

        showText.SetActive(true);

        timerActive = true;



    }

    public void OnPointerUp(PointerEventData eventData)
    {
        hand.sortingLayerName = "Cursor";
        hand.sortingOrder = 0;
        heartPanel.SetActive(false);
        timerActive = false;

        showText.SetActive(false);

        timeStart = 0;

    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        hand.sprite = defaultHand;
        hand.sortingLayerName = "Cursor";
        hand.sortingOrder = 0;
        heartPanel.SetActive(false);

    }


}


