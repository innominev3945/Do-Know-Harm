using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ActivateBandage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
  
    public SpriteRenderer topBandageSprite;
    public GameObject appliedTape;

    //Location for tape to be over for it to be applied
    public float MinX = 0.3f;
    public float MaxX = 1;
    public float MinY = -1.4f;
    public float MaxY = -1;

    // Start is called before the first frame update
    void Start()
    {
        topBandageSprite.enabled = false;
        Debug.Log("bandage sprite disabled on start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("Entered bandage");

        if (appliedTape.transform.position.x >= MinX && appliedTape.transform.position.x <= MaxX && appliedTape.transform.position.y >= MinY && appliedTape.transform.position.y <= MaxY)
        {
    
            Debug.Log("tape in correct position");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp called");
        
        if (appliedTape.transform.position.x >= MinX && appliedTape.transform.position.x <= MaxX && appliedTape.transform.position.y >= MinY && appliedTape.transform.position.y <= MaxY)
        {

            topBandageSprite.enabled = true;
            Debug.Log("tape in correct position and bandage sprite enabled");
        }



    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {



    }

}
