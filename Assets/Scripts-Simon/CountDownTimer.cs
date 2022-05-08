using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float currentTime = 0f;
    public float startingTime;
    public Text countdownText;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        startingTime = 60f;
        currentTime = startingTime;
        countdownText.text = "Time Left: " + startingTime.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0 || player.GetCurrentHealth() <= 0 || player.isDead)
        {
            countdownText.text = "You've lost!:/";
            //switch to another scene since game over!!
        }
        else if (player.hasWon)
        {
            countdownText.text = "You've won!";
        }
        else
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = "Time Left: " + currentTime.ToString("0");
        }
    }
}