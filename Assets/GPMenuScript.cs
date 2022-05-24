using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject escapeMenuBackground;
    [SerializeField] private bool menuOpen;
    [SerializeField] private bool optionsOpen;

    // Start is called before the first frame update
    void Start()
    {
        menuOpen = false;
        optionsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEscapeMenu()
    {
        if (!menuOpen)
        {
            escapeMenu.SetActive(true);
            escapeMenuBackground.SetActive(true);
            menuOpen = true;
        }
        else
        {
            if (!optionsOpen)
            {
                escapeMenu.SetActive(false);
                escapeMenuBackground.SetActive(false);
                menuOpen = false;
                Debug.Log("bruh");
            }
        }
    }

    public void ToggleOptionsMenu()
    {
        if (menuOpen)
        {
            if (!optionsOpen)
            {
                escapeMenu.SetActive(false);
                optionsMenu.SetActive(true);
                optionsOpen = true;
            }
            else
            {
                escapeMenu.SetActive(true);
                optionsMenu.SetActive(false);
                optionsOpen = false;
            }
        }
    }

    public void Escape()
    {
        if (menuOpen)
        {
            if (optionsOpen)
            {
                ToggleOptionsMenu();
            }
            else
            {
                Debug.Log("closing escape menu");
                ToggleEscapeMenu();
            }
        }
        else
        {
            ToggleEscapeMenu();
        }
    }
}
