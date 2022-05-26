/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
    //[SerializeField] private Canvas canvas;
    //private RectTransform rectTransform;
    //private CanvasGroup canvasGroup;

    public enum ItemType
    {
        Syrette,
        Forceps,
        Gauze
    }
    //public bool isused = false;

    public ItemType itemType;
    public int amount = 1;
 
    //public void Update()
    //{
    //    if (isused == true)
    //    {
    //        canvasGroup.alpha = 0f;
    //        return;
    //    }
    //}
    //public Sprite GetSprite()
    //{
    //    return GetSprite(itemType);
    //}

    //public static Sprite GetSprite(ItemType itemType)
    //{
    //    switch (itemType)
    //    {
    //        default:
    //        case ItemType.Syrette: return ItemAssets.Instance.s_Syrette;
    //        case ItemType.Forceps: return ItemAssets.Instance.s_Forceps;
    //        case ItemType.Gauze: return ItemAssets.Instance.s_Gauze;
    //        //case ItemType.Coin: return ItemAssets.Instance.s_Coin;
    //        //case ItemType.Medkit: return ItemAssets.Instance.s_Medkit;

    //        //case ItemType.ArmorNone: return ItemAssets.Instance.s_ArmorNone;
    //        //case ItemType.Armor_1: return ItemAssets.Instance.s_Armor_1;
    //        //case ItemType.Armor_2: return ItemAssets.Instance.s_Armor_2;
    //        //case ItemType.HelmetNone: return ItemAssets.Instance.s_HelmetNone;
    //        //case ItemType.Helmet: return ItemAssets.Instance.s_Helmet;
    //        //case ItemType.Sword_1: return ItemAssets.Instance.s_Sword_1;
    //        //case ItemType.Sword_2: return ItemAssets.Instance.s_Sword_2;
    //    }
    //}

    //public override string ToString()
    //{
    //    return itemType.ToString();
    //}

    ////public CharacterEquipment.EquipSlot GetEquipSlot()
    ////{
    ////    switch (itemType)
    ////    {
    ////        default:
    ////        case ItemType.ArmorNone:
    ////        case ItemType.Armor_1:
    ////        case ItemType.Armor_2:
    ////            return CharacterEquipment.EquipSlot.Armor;
    ////        case ItemType.HelmetNone:
    ////        case ItemType.Helmet:
    ////            return CharacterEquipment.EquipSlot.Helmet;
    ////        case ItemType.SwordNone:
    ////        case ItemType.Sword:
    ////        case ItemType.Sword_1:
    ////        case ItemType.Sword_2:
    ////            return CharacterEquipment.EquipSlot.Weapon;
    ////    }
    ////}

}
