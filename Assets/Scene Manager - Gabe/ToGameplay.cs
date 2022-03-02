using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGameplay : MonoBehaviour
{

    // attach this to a button and set the onclick attribute appropriately to send the game to the GP scene

    public void LoadGameplay()
    {
        Debug.Log("Sending to Gameplay");
        Loader.Load(Loader.Scene.GPdummy);
    }
}
