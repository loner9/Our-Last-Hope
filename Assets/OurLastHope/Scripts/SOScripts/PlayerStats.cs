using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats",menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
   [Header("Config")]
    public float maxHealth = 10f;
    public float health;
    public float maxStamina = 100f;
    public float stamina;

    public void resetPlayerStats()
    {
        health = maxHealth;
        stamina = maxStamina;
    }
}
