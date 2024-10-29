using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealths : MonoBehaviour, IDamagable
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;


    private void Update(){
        if (Input.GetKeyDown(KeyCode.P))
        {
            takeDamage(1f);
        }
    }
    public void takeDamage(float damage)
    {
        stats.health -= damage;
        if (stats.health <= 0 || stats.health == 0)
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

    private void playerDead()
    {
        Debug.Log("Player mati");
    }
}
