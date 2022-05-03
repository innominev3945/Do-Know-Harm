using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Wound : MonoBehaviour, IDropHandler
{
    public Player player;
    public ToolItem Item1;
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool isHealed = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Update()
    {
        if (isHealed == true)
        {
            canvasGroup.alpha = 0f;
            return;
        }

    }


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag == GameObject.Find("SyretteItem"))
        {
            Item1.isused = true;
            player.GetHeal(500);
            player.numOfInjur--;//can be changed
            isHealed = true;
            canvasGroup.alpha = 0f;
        }

    }
}
