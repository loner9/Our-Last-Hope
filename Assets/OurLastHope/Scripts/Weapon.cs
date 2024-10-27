using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Ranged,
    Melee
}

[CreateAssetMenu(fileName = "Weapon_")]
public class Weapon : ScriptableObject
{
    [Header("Config")]
    public Sprite Icon;
    public WeaponType WeaponType;
    public float Damage;
}
