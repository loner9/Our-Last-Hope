using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats Stats;
    public PlayerStats StatsHid => Stats;
    public PlayerHealths PlayerHealth { get; private set; }

    public void resetPlayer(){
        Stats.resetPlayerStats();
    }
}
