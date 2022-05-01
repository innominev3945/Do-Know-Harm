using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Wound : MonoBehaviour, IDropHandler
{
    public Player player;
    public ToolItem Item1;

    //public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    //public class OnItemDroppedEventArgs : EventArgs
    //{
    //    public Item item;
    //}

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag == Item1)
        {
            Item1.isused = true;
            player.GetHeal(500);
        }
    }
}
