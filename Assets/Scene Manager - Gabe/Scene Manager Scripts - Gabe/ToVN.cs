using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToVN : MonoBehaviour
{
    // attach this to a button and set the onclick attribute appropriately to send the game to the VN scene

    public void LoadVisualNovel()
    {
        Debug.Log("Sending to VN");
        Loader.Load(Loader.Scene.VNDummy);
    }
}
