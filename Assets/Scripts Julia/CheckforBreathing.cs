using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class CheckforBreathing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    bool isBreathing = true;
    string breathingResult = "";

    //checks if hand tool is hovered over chest
    public bool isOverChest;

    //for detecting how long player has watched for breathing
    public float timeStart;
    bool timerActive = false;

    //in-game text showing if patient is breathing
    public GameObject showText;
    public Text textbox;
    public string monitoringBreathing = "Monitoring rise and fall of chest";

    // Start is called before the first frame update
    void Start()
    {
        showText.SetActive(false);      //text showing breathing result is hidden at start
        textbox.text = monitoringBreathing;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBreathing)
            breathingResult = "Patient is breathing.";
        else
            breathingResult = "Patient is not breathing.";

        //counts to five seconds before showing breathing result
        if (timerActive)
        {
            timeStart += Time.deltaTime;

            if (timeStart < 0.5)
                textbox.text = monitoringBreathing;
            if (timeStart < 1 && timeStart > 0.5)
                textbox.text = monitoringBreathing + ".";
            if (timeStart < 1.5 && timeStart > 1)
                textbox.text = monitoringBreathing + "..";
            if (timeStart < 2 && timeStart > 1.5)
                textbox.text = monitoringBreathing + "...";
            if (timeStart < 2.5 && timeStart > 2)
                textbox.text = monitoringBreathing;
            if (timeStart < 3 && timeStart > 2.5)
                textbox.text = monitoringBreathing + ".";
            if (timeStart < 3.5 && timeStart > 3)
                textbox.text = monitoringBreathing + "..";
            if (timeStart < 4 && timeStart > 3.5)
                textbox.text = monitoringBreathing + "...";
            if (timeStart < 4.5 && timeStart > 4)
                textbox.text = monitoringBreathing;
            if (timeStart > 4.5)
                textbox.text = breathingResult;
        }
        else
            showText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");

        isOverChest = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        Debug.Log("checking for breathing");

        //"Checking for breathing" indicator text is shown
        showText.SetActive(true);

        //starts timer to count how long player has been checking for breathing
        timerActive = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        //when player releases the left mouse button, timer for checking breathing is stopped and reset
        timerActive = false;
        timeStart = 0;

        //"Checking for breathing" indicator text is hidden
        showText.SetActive(false);

    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");

        /*if player moves hand tool away from patient's chest while watching for breathing,
        the timer recording how long player has monitored breathing stops and restarts*/
        isOverChest = false;
        timerActive = false;
        timeStart = 0;


    }
}
