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
    public int numOfInjur = 5;
    // update numOfInjur at the beginning of each game!!
    public int injuredTime = 0;
    // players made mistakes --> add injuredTime
    public int healingTime = 0;
    // same concept as injuredTime, when player receives a health boost ...

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (numOfInjur == 0)
        {
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
                currentHealth -= 0.8f * Time.deltaTime * numOfInjur;
            }
            healthBar.SetHealth(currentHealth);
        }
        else
        {
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

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    void GetHeal(float heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

}
