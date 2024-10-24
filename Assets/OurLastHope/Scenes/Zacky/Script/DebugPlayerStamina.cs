using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPlayerStamina : MonoBehaviour
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
        //Panggil Debug
        HandleInputDebug();
    }

    //Function untuk menangani KeyCode Debug
    private void HandleInputDebug()
    {
        //Hold 'R' untuk running
        if (Input.GetKey(KeyCode.R))
        {
            SetState(PlayerState.Running);
            Debug.Log("Player mulai running, stamina akan berkurang.");
        }

        //'D' untuk dodge
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentStamina >= dodgeStaminaCost)
            {
                Dodge();
                Debug.Log("Player dodge, stamina berkurang 10 poin.");
            }
            else
            {
                Debug.Log("Tidak cukup stamina untuk dodge");
            }
        }

        //'S' untuk lihat current stamina
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Current Stamina Player adalah: " + currentStamina);
        }

        //'T' untuk lihat info stamina bar
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("StaminaBar fill amount adalah: " + (currentStamina / maxStamina));
        }

        //Lepas 'R' untuk kembali walking
        if (Input.GetKeyUp(KeyCode.R))
        {
            SetState(PlayerState.Walking);
            Debug.Log("Player kembali ke walking.");
        }
    }

    private void HandleStates()
    {
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
