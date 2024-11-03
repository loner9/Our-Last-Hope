using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Weapon")]
public class ItemWeapon : InventoryItem
{
    [Header("Weapon")]
    public Weapon weapon;
}
