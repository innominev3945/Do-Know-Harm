using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TapeDestroy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    //the TapeDestroy script is attached to the tape prefab, used to destroy the prefab whenever the player releases the left mouse button after dragging the tape


    public GameObject destroyedTape; //tape prefab that will be destroyed

    public GameObject topWrapLeg;
    public GameObject midWrapLeg;
    public GameObject lowWrapLeg;

    public bool isOverTopLeg;
    public bool isOverMidLeg;
    public bool isOverLowLeg;

    //private Sprite DefaultHand, PulseHand;


    //COORDINATES OF TOP RIGHT BANDAGE
    public float MinXTopLeg = 0.3f;
    public float MaxXTopLeg = 1;
    public float MinYTopLeg = -1.4f;
    public float MaxYTopLeg = -1;

    //COORDINATES OF MIDDLE RIGHT BANDAGE
    public float MinXMidLeg = 0.26f;
    public float MaxXMidLeg = 1.17f;
    public float MinYMidLeg = -2.85f;
    public float MaxYMidLeg = -2.45f;

    //COORDINATES OF LOWER RIGHT BANDAGE
    public float MinXLowLeg = 0.26f;
    public float MaxXLowLeg = 1.17f;
    public float MinYLowLeg = -3.77f;
    public float MaxYLowLeg = -3.4f;

    // Start is called before the first frame update
    void Start()
    {
        isOverTopLeg = false;

        //DefaultHand = Resources.Load<Sprite>("DefaultHand");
        //PulseHand = Resources.Load<Sprite>("PulseHand");
        //rend.sprite = DefaultHand;
    }

    void Update()
    {



    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {



    }

    public void OnPointerDown(PointerEventData eventData)
    {



        /*
        if (transform.position.x >= MinX && transform.position.x <= MaxX && transform.position.y >= MinY && transform.position.y <= MaxY)
        {


        }
        */


    }

    public void OnDrag(PointerEventData eventData)
    {

        if (transform.position.x >= MinXTopLeg && transform.position.x <= MaxXTopLeg && transform.position.y >= MinYTopLeg && transform.position.y <= MaxYTopLeg)
        {
            Debug.Log("Over upper leg");
            isOverTopLeg = true;
        }
        else
        {
            Debug.Log("Not over upper leg");
            isOverTopLeg = false;
        }


        if (transform.position.x >= MinXMidLeg && transform.position.x <= MaxXMidLeg && transform.position.y >= MinYMidLeg && transform.position.y <= MaxYMidLeg)
        {
            Debug.Log("Over mid leg");
            isOverMidLeg = true;
        }
        else
        {
            Debug.Log("Not over mid leg");
            isOverMidLeg = false;
        }


        if (transform.position.x >= MinXLowLeg && transform.position.x <= MaxXLowLeg && transform.position.y >= MinYLowLeg && transform.position.y <= MaxYLowLeg)
        {
            Debug.Log("Over lower leg");
            isOverLowLeg = true;
        }
        else
        {
            Debug.Log("Not over lower leg");
            isOverLowLeg = false;
        }


    }

    public void OnPointerUp(PointerEventData eventData)
    {

        /*
        if (transform.position.x >= MinX && transform.position.x <= MaxX && transform.position.y >= MinY && transform.position.y <= MaxY)
        {

            topBandageSprite.enabled = false;

        }
        */
        if (isOverTopLeg)
        {
            Debug.Log("Applied top bandage");
            Instantiate(topWrapLeg, new Vector3(0.63f, -1.2113f, 0), Quaternion.identity); 
        }
        else if (isOverMidLeg)
        {
            Debug.Log("Applied bottom bandage");
            Instantiate(midWrapLeg, new Vector3(0.6731f, -2.6096f, 0), Quaternion.identity); //creates a tape object for player to drag and drop
        }
        else if (isOverLowLeg)
        {
            Debug.Log("Applied lower bandage");
            Instantiate(lowWrapLeg, new Vector3(0.7115f, -3.57f, 0), Quaternion.identity); //creates a tape object for player to drag and drop
        }




        Destroy(destroyedTape, 0);


    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {

    }
}
