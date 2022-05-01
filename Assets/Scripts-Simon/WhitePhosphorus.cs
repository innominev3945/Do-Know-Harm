using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class WhitePhosphorus : MonoBehaviour, IDropHandler
{
    public Player player;
    public Forceps Forceps;
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
    //public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    //public class OnItemDroppedEventArgs : EventArgs
    //{
    //    public Item item;
    //}
    public void Start()
    {
        ishealed = false;
    }
    public void Update()
    {
        if(ishealed == true)
        {
            canvasGroup.alpha = 0f;
            return;
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
            player.GetHeal(50);
            player.numOfInjur--;
        }
    }
}
