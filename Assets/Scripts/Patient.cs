using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patient : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    [SerializeField] private HealthBar healthbar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }*/
    
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
}
