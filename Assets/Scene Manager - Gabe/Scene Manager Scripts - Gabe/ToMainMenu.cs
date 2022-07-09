using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PatientManagerClass;

public class ToMainMenu : MonoBehaviour
{
    // attach this to a button and set the onclick attribute appropriately to send the game to the main menu

    public void LoadMainMenu()
    {
        Debug.Log("Sending to Main Menu");
        Loader.Load(Loader.Scene.MainMenu);
    }
}
