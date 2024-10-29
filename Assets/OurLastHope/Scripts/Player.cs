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

    private void Awake(){
        playerHealths = GetComponent<PlayerHealths>();
        playerStaminas = GetComponent<PlayerStaminas>();
    }
    
    private void Update(){
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(medkit.UseItem())
            {
                Debug.Log("Use Medkit");
            }
        }
    }

    

    public void resetPlayer(){
        Stats.resetPlayerStats();
    }
}
