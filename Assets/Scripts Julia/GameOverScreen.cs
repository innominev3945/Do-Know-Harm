using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " Patients Died";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void ExitButton()
    {
        //SceneManager.LoadScene("MainMenu");
    }

}
