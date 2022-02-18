using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;



public class CheckPulse2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject startingHand;
    public SpriteRenderer hand;
    public Sprite pulseHand;
    public Sprite defaultHand;
    public GameObject heartPanel;
    public VideoPlayer videoPlayer;

    void Start()
    {
        heartPanel.SetActive(false);
        startingHand.SetActive(false);
    }

    void Update()
    {
        startingHand.SetActive(true);
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
        heartPanel.SetActive(true);
        RenderTexture.active = videoPlayer.targetTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        hand.sortingLayerName = "Cursor";
        hand.sortingOrder = 0;
        heartPanel.SetActive(false);

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

