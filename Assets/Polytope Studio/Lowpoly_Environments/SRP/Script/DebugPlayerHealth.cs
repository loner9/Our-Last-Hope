using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPlayerHealth : MonoBehaviour
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

    private void Update()
    {
        // Input KeyCode untuk debug

        // Tekan 'D' untuk menerima damage (misal, 10 damage)
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(10f);
            Debug.Log("Damage Taken 10. Health tersisa: " + currentHealth);
        }

        // Tekan 'H' untuk melihat current health Player
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Current Health Player adalah: " + currentHealth);
        }

        // Tekan 'B' untuk melihat info HealthBar
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("HealthBar fillamount adalah: " + (currentHealth / maxHealth));
        }

        // Tekan 'K' untuk memaksa Player mati (health = 0)
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentHealth = 0;
            PlayerDeath();
            Debug.Log("Player Dead Health = " + currentHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        //kena damage health berkurang
        currentHealth -= damage;

        //Cek untuk memastikan health tidak bisa negatif
        currentHealth = Mathf.Clamp(currentHealth, 0,maxHealth);

        //update HealthBar setiap kali health berkurang
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        //Sesuaikan fill amount pada HealthBar sesuai proporsi CurrentHealth terhadap MaxHealth
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    private void PlayerDeath()
    {
        //isi player behaviour saat mati
        Debug.Log("Player Mati");
    }
}
