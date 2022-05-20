using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 0f;
    public bool isDead = false;
    public bool hasWon = false;
    public HealthBar healthBar;
    public int numOfInjur = 2;
    // update numOfInjur at the beginning of each game!!
    public int injuredTime = 0;
    // players made mistakes --> add injuredTime
    public int healingTime = 0;
    // same concept as injuredTime, when player receives a health boost ...
    public WhitePhosphorus WP;
    private bool soundchange1, soundchange2, soundchange3, soundchangedeath, soundchangewon;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        soundchange1 = true;
        soundchange2 = false;
        soundchange3 = false;
        soundchangedeath = false;
        soundchangewon = false;
    }

    void Update()
    {
        if (numOfInjur == 0)
        {
            if (!soundchangewon)
            {
                SoundManagerScript.PlaySound("win");
                soundchangewon = true;
            }
            hasWon = true;
            healthBar.SetHealth(maxHealth);
            return;
        }

        //the following two lines make health decrease with time
        if (currentHealth > 0)
        {
            if (injuredTime > 0)
            {
                currentHealth -= 30 * Time.deltaTime;
                injuredTime--;
            }
            else if (healingTime > 0)
            {
                currentHealth += 30 * Time.deltaTime;
                healingTime--;
            }
            else
            {
                currentHealth -= 0.8f * Time.deltaTime * numOfInjur * WP.degree / 0.5f;
            }
            healthBar.SetHealth(currentHealth);

            if (currentHealth > 70f && soundchange1)
            {
                SoundManagerScript.PlaySound("heartbeat1");
                soundchange1 = false;
                soundchange2 = true;
                soundchange3 = true;
            }
            else if (currentHealth <= 70f && currentHealth > 30f && soundchange2)
            {
                SoundManagerScript.PlaySound("heartbeat2");
                soundchange1 = true;
                soundchange2 = false;
                soundchange3 = true;
            }
            else if (currentHealth <= 30f && currentHealth > 0f && soundchange3)
            {
                SoundManagerScript.PlaySound("heartbeat3");
                soundchange1 = true;
                soundchange2 = true;
                soundchange3 = false;
            }
        }
        else
        {
            if (!soundchangedeath)
            {
                SoundManagerScript.PlaySound("death");
                soundchangedeath = true;
            }
            isDead = true;
            return;
        }
        //link this to action!!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            injuredTime += 160;
        }
        //link this to action!!
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            healingTime += 160;
        }
        //link this to action!!
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            numOfInjur--;
        }
    }

    public void TakeDamage(int damage)
    {
        injuredTime += damage;
        //healthBar.SetHealth(currentHealth);
    }
    public void GetHeal(int heal)
    {
        //currentHealth += heal;
        healingTime += heal;
        //healthBar.SetHealth(currentHealth);
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}
