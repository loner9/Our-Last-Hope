using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealths : MonoBehaviour, IDamagable
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;


    // private void Update(){
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         takeDamage(1f);
    //     }
    // }
    public void takeDamage(float damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            playerDead();
        }
    }

    public void RestoreHealth(float health)
    {
        stats.health += health;
        if (stats.health > stats.maxHealth)
        {
            stats.health = stats.maxHealth;
        }
    }

    public bool CanRestoreHealth()
    {
        return stats.health > 0 && stats.health < stats.maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemyHand"))
        {
            if (other.gameObject.name.Equals("reg"))
            {
                Debug.Log("reg");
                takeDamage(1.2f);
            }
            else if (other.gameObject.name.Equals("fas"))
            {
                Debug.Log("fas");
                takeDamage(0.7f);
            }
            else if (other.gameObject.name.Equals("strong"))
            {
                Debug.Log("strong");
                takeDamage(2f);
            }
        }
    }

    private void playerDead()
    {
        Debug.Log("Player mati");
        GameManager.Instance.GameOver();
    }
}
