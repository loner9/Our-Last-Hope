using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminas : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private PlayerStats stats;

    public float CurrentStamina { get; private set; }

    private void Start()
    {
        ResetStamina();
    }

    public void UseStamina(float amount)
    {
        
        stats.stamina -= amount;
        stats.stamina = Mathf.Clamp(stats.stamina, 0.0f, stats.maxStamina);
        // stats.stamina = Mathf.Max(stats.stamina -= amount, 0.0f);
        CurrentStamina = stats.stamina;
    }

    public void RecoverStaminaUpdate(float amount)
    {
        stats.stamina += amount;
        stats.stamina = Mathf.Clamp(stats.stamina, 0.0f, stats.maxStamina);
        CurrentStamina = stats.stamina;
    }

    public void RecoverStaminaOrdinary(float amount)
    {
        stats.stamina += amount;
        stats.stamina = Mathf.Clamp(stats.stamina, 0.0f, stats.maxStamina);
        CurrentStamina = stats.stamina;
    }
    
    public bool CanRecoverStamina()
    {
        return stats.stamina > 0 && stats.stamina < stats.maxStamina;
    }

    public void ResetStamina()
    {
        CurrentStamina = stats.maxStamina;
    }
}
