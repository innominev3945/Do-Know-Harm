using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Wound : MonoBehaviour, IDropHandler
{
    public Player player;
    public SyretteItem syretteItem;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            syretteItem.isused = true;
            player.GetHeal(500);
        }
    }
}
