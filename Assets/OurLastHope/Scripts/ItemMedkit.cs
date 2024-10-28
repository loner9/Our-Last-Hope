using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemMedkit", menuName ="Items/Medkit")]
public class ItemMedkit : InventoryItem
{
    [Header("Config")]
    public float HealthValue;

    public override bool UseItem()
    {
        // GameManager.Instance.Player.PlayerHealth.
        return true;
    }
}
