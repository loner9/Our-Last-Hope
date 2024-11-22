using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Reference Health
    public float maxHealth = 100f;
    public float currentHealth;

    //Reference HealthBar UI
    public Image healthBar;

    private void Start()
    {
        //Mulai game currentHealth sama dengan maxHealth
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        //kena damage health berkurang
        currentHealth -= damage;

        //Cek untuk memastikan health tidak bisa negatif
        currentHealth = Mathf.Clamp(currentHealth, 0,maxHealth);

        //update HealthBar setiap kali health berkurang
        UpdateHealthBar();

        //player health 0? player mati
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void UpdateHealthBar()
    {
        //Sesuaikan proporsi fillamount currentHealth terhadap maxHealth pada HealthBar
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    private void PlayerDeath()
    {
        //isi player behaviour saat mati
        Debug.Log("Player Mati");
    }   
}
