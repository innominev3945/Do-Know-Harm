using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    //[SerializeField] Canvas UICanvas; //save UI Canvas for enabling/disabling PLUS saving performance
    [SerializeField] Canvas[] menus; //consider as book pages from front to back
    int menuCount = 0; //count number of items in menus
    
    bool canvasToggle = false;

    //MAKE IT A SINGLETON (https://gamedevbeginner.com/singletons-in-unity-the-right-way/)
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this) //if there is another instance
        {
            Destroy(gameObject); //destroy myself
        }
        else
        {
            _instance = this; //set myself as the instance
        }

        //CANVAS INFO
        //on by default
        canvasToggle = true;
        ToggleSettingsMenu(canvasToggle);

        menuCount = menus.Length; //store the length of the menus
    }


    //SCRIPTS
    public void OnSettingsButton() //listens to buttons in actions
    {
        canvasToggle = !canvasToggle; //toggle canvas on/off
        ToggleSettingsMenu(canvasToggle);
    }

    private void ToggleSettingsMenu(bool toggle) //toggles the entire settings menu
    {
        if (toggle) //if toggle is true, turn on the settings menu
        {
            //only relevant parts are open
            menus[0].enabled = true; //journal bg open
            menus[1].enabled = true; //pause panel open
        }
        else //if toggle is false, close the settings menu
        {
            foreach (Canvas canvasComp in menus)
            {
                canvasComp.enabled = false; //set everything in menus to canvas off
            }
        }
    }

    //FUNCTIONS FOR DIFFERENT MENUS

    public void OpenPage(int pageNum) //open page in journal (actual journal pages should be its own subset of an array separate from bigger array of menus)
    {
        CloseAll(); //close everything above the background layer
        menus[pageNum].enabled = true;
    }

    private void CloseAll()
    {
        for (int i = 1; i < menuCount; ++i)
        {
            menus[i].enabled = false; //close everything above the background layer
        }
    }
}
