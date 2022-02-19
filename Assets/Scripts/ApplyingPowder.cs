using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class ApplyingPowder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public SpriteRenderer packet;
    public Sprite pouringPacket;
    public Sprite tiltingPacket;
    public Sprite defaultPacket;

    public Transform baseDot;   //variable for dot prefab -> storing in variable to instantiate it

    public static string toolType;  //tracks what tool you're using, like powder packet

    public bool isTreatingWound = false;
    public bool isOverWound = false;

    // Start is called before the first frame update
    void Start()
    {
        packet.sprite = defaultPacket;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);          //finds mouse position

        if (isTreatingWound)
        {
            packet.sprite = pouringPacket;
            Instantiate(baseDot, mousePosition, baseDot.rotation);
        }
    
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
        packet.sprite = tiltingPacket;

        isOverWound = true;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isTreatingWound = true;

        packet.sprite = pouringPacket;


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTreatingWound = false;

        if (isOverWound)
            packet.sprite = tiltingPacket;


    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        Debug.Log("Cursor Exiting " + name + " GameObject");
        isTreatingWound = false;
        packet.sprite = defaultPacket;


    }









}

