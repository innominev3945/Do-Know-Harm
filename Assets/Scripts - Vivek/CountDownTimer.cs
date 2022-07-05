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
        //float health = player.GetCurrentHealth();
        countdownText.text = "VITALS: ";
    }

    // Update is called once per frame
    void Update()
    {
        //if (currentTime <= 0 || player.GetCurrentHealth() <= 0 || player.isDead)
        //{
        //    countdownText.text = "You've lost!:/";
        //    //switch to another scene since game over!!
        //}
        //else if (player.hasWon)
        //{
        //    countdownText.text = "You've won!";
        //}
        //else
        //{
            float health = player.GetCurrentHealth();
        //currentTime -= 1 * Time.deltaTime;
        if (player.numOfInjur == 0 && player.currentHealth != 0)
            countdownText.text = "VITALS: HEALED";
        else
            countdownText.text = "VITALS: " + health.ToString("0") + "%";
        //}
    }
}
