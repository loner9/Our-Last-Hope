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

    private bool isInventoryOpen = false;
    [SerializeField]
    private Transform inventoryTransform;
    private void Awake(){
        playerHealths = GetComponent<PlayerHealths>();
        playerStaminas = GetComponent<PlayerStaminas>();

        
        controls = new PlayerControls();

        controls.UI.Inventory.performed += ctx => ToggleInventory();
    }

    private void ToggleInventory()
    {
        Debug.Log("Toggle Inventory");
        if (!isInventoryOpen)
        {
            inventoryTransform.gameObject.SetActive(true);
        }else{
            inventoryTransform.gameObject.SetActive(false);
        }

        isInventoryOpen = !isInventoryOpen;
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(medkit.UseItem())
            {
                Debug.Log("Use Medkit");
            }

            if(itemStamina.UseItem())
            {
                Debug.Log("Use Stamina item");
            }
        }
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
