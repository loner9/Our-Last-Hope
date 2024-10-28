using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminas : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;

    public void UseStamina(float amount)
    {
        if (stats.stamina >= amount)
        {
            stats.stamina = Mathf.Max(stats.stamina -= amount, 0);
        }
    }
}
