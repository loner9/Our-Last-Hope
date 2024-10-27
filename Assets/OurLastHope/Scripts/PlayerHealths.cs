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

    private void playerDead()
    {
        Debug.Log("Player mati");
    }
}
