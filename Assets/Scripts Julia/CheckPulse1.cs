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
    bool hasPulse = true;
    string pulseResult = "";

    //hand tool
    public SpriteRenderer hand;
    public Sprite pulseHand;
    public Sprite defaultHand;

    //checks if hand is over carotid artery
    public bool isOverArtery;

    //for detecting how long pulse is being taken
    public float timeStart;
    bool timerActive = false;

    //in-game text showing pulse result
    public GameObject showText;
    public Text textbox;





    void Start()
    {

        hand.sprite = defaultHand;      //set to default hand tool at start

        showText.SetActive(false);      //text showing pulse result is hidden at start
        textbox.text = "Checking pulse";        


    }



    void Update()
    {
       //result text that will show if pulse is or is not detected
        if (hasPulse)
            pulseResult = "Pulse detected!";
        else
            pulseResult = "No pulse detected.";

        //counts to five seconds before showing pulse result
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
        } else
            showText.SetActive(false);


    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");

        isOverArtery = true;

        hand.sprite = pulseHand;    //if cursor hovers over neck, hand sprite changes from default to pulse-checking position

        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //when the player holds down on the left mouse button, sprite sorting order changes so that the "hand" sprite appears under the "head" sprite
        //makes it appear as if the medic's fingers are under the patient's jaw when checking pulse
        //hand.sortingLayerName = "Default";   
        //hand.sortingOrder = 0;              
       
        //"Checking pulse" indicator text is shown
        showText.SetActive(true);

        //starts timer to count how long player has been checking the pulse
        if (isOverArtery)
            timerActive = true;
        else
            timerActive = false;



    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //when player releases the left mouse button after holding it down, the "hand" sprite returns to it's normal layer, appearing over the "head" sprite
        //hand.sortingLayerName = "Cursor";
        //hand.sortingOrder = 0;

        //when player releases the left mouse button, timer for checking pulse is stopped and reset
        timerActive = false;
        timeStart = 0;

        //"Checking pulse" indicator text is hidden
        showText.SetActive(false);



    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");

        isOverArtery = false;
        timerActive=false;
        timeStart = 0;

        //when the player moves the cursor so that it's no longer hovering over the patient's neck, the hand tool returns to it's default hand sprite
        hand.sprite = defaultHand;

        //ensures the hand tool is on the top-most layer
        //hand.sortingLayerName = "Cursor";
        //hand.sortingOrder = 0;


    }


}


