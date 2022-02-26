using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour //inherit from ui manager
{   
    [Header("Scene Management Names")]
    [SerializeField] string MainMenuSceneName = "Main Menu"; //change to index if needed

    public void Settings()
    {
       UIManager.Instance.OpenPage(2); //open settings menu
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName); //load main menu
    }

    public void QuitGame()
    {
        Application.Quit(); //quit the applcation
    }
}
