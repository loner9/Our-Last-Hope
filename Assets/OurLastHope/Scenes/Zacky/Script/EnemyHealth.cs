using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Reference Enemy Health
    public float maxHealth = 100f;
    public float currentHealth;

    //Untuk Cek apa enemy dead?
    private bool isDead = false;

    private void Start()
    {
        //Mulai game currentHealth sama dengan maxHealth
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        //jika status dead, maka tidak menerima damage lagi
        if (isDead) return;

        //kena damage health berkurang
        currentHealth -= damage;
        //Cek untuk memastikan health tidak bisa negatif
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        if (isDead)
        {
            //Set status jadi Dead
            isDead = true;
            Debug.Log("EnemyDeath");

            //Animasi EnemyDeath disini ?

            //Coroutine untuk delay 
            StartCoroutine(DestroyAfterDelay(5f));
        }

        //Coroutine untuk hapus object setelah delay
        IEnumerator DestroyAfterDelay(float delay)
        {
            //tunggu delay
            yield return new WaitForSeconds(delay);

            //hapus gameObject
            Destroy(gameObject);
        }
    }
}
