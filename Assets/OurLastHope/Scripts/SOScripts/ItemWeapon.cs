using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Weapon")]
public class ItemWeapon : InventoryItem
{
    [Header("Weapon")]
    public Weapon weapon;
    public bool isEquipped = false;
    public int currentAmmo = 0;
    
    public override bool Removable()
    {
        return false;
    }
    public override void RemoveItem()
    {
        Debug.Log("Cant remove items");
        return;
    }
}
