using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Canvas UICanvas; //save UI Canvas for enabling/disabling PLUS saving performance
    [SerializeField] GameObject[] menus; //0 pause, 1 settings (1) [later include save settings, other settings menus if needed]
    bool canvasOn = false;

    [Header("Scene Management Names")]
    [SerializeField] string MainMenuSceneName = "Main Menu"; //edit to index if needed

    public void OnSettingsButton()
    {
        canvasOn = !canvasOn;
        UICanvas.enabled = canvasOn;
        //figure out how to open and close canvas on button press
    }

    private void Start()
    {
        canvasOn = UICanvas.enabled;
    }

    public void MainMenu() //back to game main menu
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void PauseMenu() //toggle pause menu
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        menus[0].SetActive(true);
    }

    public void Settings() //open settings
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        menus[1].SetActive(true);
    }
    public void ToggleCanvas() //toggle UI canvas on and off
    {
        canvasOn = !canvasOn;
        UICanvas.enabled = canvasOn;
        //figure out how to open and close canvas on button press
    }
}
