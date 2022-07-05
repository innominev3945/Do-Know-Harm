using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class WhitePhosphorus : MonoBehaviour, IDropHandler
{
    public Player player;
    public Forceps Forceps;
    public Sprite degree1;
    public Sprite degree2;
    public Sprite degree3;
    private float timer;
    public int degree;
    private bool ishealed;
    [SerializeField] private Canvas canvas;
    //public static ToolItem Instance { get; private set; }
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Start()
    {
        ishealed = false;
        degree = 1;
        timer = 0f;
    }
    public void Update()
    {
        if(ishealed == true)
        {
            canvasGroup.alpha = 0f;
            return;
        }
        else
        {
            timer += 1 * Time.deltaTime;
            changeImages();
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        Debug.Log(eventData.pointerDrag);
        if (eventData.pointerDrag == GameObject.Find("Forceps"))
        {
            Forceps.isused = true;
            ishealed = true;
            player.GetHeal(300);
            player.numOfInjur--;
            canvasGroup.alpha = 0f;
        }
    }

    private void changeImages()
    {
        if (timer < 5.0)
        {
            GetComponent<Image>().sprite = degree1;
            degree = 1;
            return;
        }
        else if (timer < 10.0)
        {
            GetComponent<Image>().sprite = degree2;
            degree = 2;
            return;
        }
        else
        {
            GetComponent<Image>().sprite = degree3;
            degree = 3;
            return;
        }
    }
}
