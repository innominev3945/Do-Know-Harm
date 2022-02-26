using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 0f;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        currentHealth -= 1 * Time.deltaTime;
        healthBar.SetHealth(currentHealth);
        //link this to action!!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20f);
        }
        //link this to action!!
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetHeal(10f);
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
