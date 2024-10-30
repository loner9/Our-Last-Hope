using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    //damage setiap enemy
    public float enemyDamage = 20f;

    //inflict function damage ke player 
    public void AttackPlayer(PlayerHealth playerHealth)
    {
        //saat enemy attack player, panggil TakeDamage function pada PlayerHealth
        playerHealth.TakeDamage(enemyDamage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //enemy collide dengan player
        if (collision.gameObject.CompareTag("Player"))
        {
            //panggil function PlayerHealth untuk diambil component
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                //panggil function AttackPlayer untuk kasih damage
                AttackPlayer(playerHealth);
            }
        }
    }
}
