using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    //Reference stamina player
    public float maxStamina = 100f;
    public float currentStamina;

    //UI untuk stamina bar
    public Image staminaBar;
    //Reference pengurangan stamina
    public float staminaDrainRate = 2f;
    public float dodgeStaminaCost = 10f;

    //State Player
    private enum PlayerState { Walking, Running, Dodging }
    private PlayerState currentState;

    private void Start()
    {
        //Saat start stamina penuh
        currentStamina = maxStamina;
        //Default State
        currentState = PlayerState.Walking;
        //Tampilkan stamina bar saat ini
        UpdateStaminaBar();
    }

    private void Update()
    {
        //Atur states player sesuai input
        HandleStates();
        //Selalu update UI stamina bar
        UpdateStaminaBar();
    }

    private void HandleStates()
    {
        if (Input.GetKey(KeyCode.LeftControl) && currentStamina > 0)
        {
            SetState(PlayerState.Running);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= dodgeStaminaCost)
        {
            SetState(PlayerState.Dodging);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            SetState(PlayerState.Walking);
        }

        //Konsumsi stamina sesuai state
        switch (currentState)
        {
            case PlayerState.Running:
                DrainStamina(staminaDrainRate * Time.deltaTime);
                break;
            case PlayerState.Dodging:
                Dodge();
                SetState(PlayerState.Walking);
                break;
            case PlayerState.Walking:
                break;
        }

        //Cek stamina habis
        if (currentStamina <= 0)
        {
            //Jika stamina habis state akan walking permanen hingga terisi stamina lagi
            currentStamina = 0;
            SetState(PlayerState.Walking);
        }
    }

    //Function untuk mengatur state
    private void SetState(PlayerState newState)
    {
        currentState = newState;
    }

    //Function untuk mengurangi stamina secara bertahap
    public void DrainStamina(float amount)
    {
        currentStamina -= amount;
        //Cek stamina tidak dibawah 0
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateStaminaBar();
    }

    //Function untuk dodge
    public void Dodge()
    {
        currentStamina -= dodgeStaminaCost;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        UpdateStaminaBar();
    }

    //Function untuk Update StaminaBar UI
    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = currentStamina / maxStamina;
        }
    }
}
