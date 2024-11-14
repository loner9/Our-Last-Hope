using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats Stats;
    public PlayerStats StatsHid => Stats;
    public PlayerHealths playerHealths {get; private set;}

    public PlayerStaminas playerStaminas {get; private set;}

    [Header("Test")]
    public ItemMedkit medkit;
    public ItemStamina itemStamina;   

    private PlayerControls controls;

    public PlayerControls Controls => controls;

    public PlayerAim aim {get; private set;}

    private bool isInventoryOpen = false;
    public bool IsInventoryOpen => isInventoryOpen;
    [SerializeField]
    private Transform inventoryTransform;
    private void Awake(){
        playerHealths = GetComponent<PlayerHealths>();
        playerStaminas = GetComponent<PlayerStaminas>();
        aim = GetComponentInChildren<PlayerAim>();
        
        controls = new PlayerControls();
    }

    private void Update(){
        
    }

    public void resetPlayer(){
        Stats.resetPlayerStats();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
