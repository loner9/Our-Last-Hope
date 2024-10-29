using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemStamina", menuName ="Items/Stamina")]
public class ItemStamina : InventoryItem
{
    [Header("Config")]
    public float StaminaValue;

    public override bool UseItem()
    {
        if (GameManager.Instance.Player.playerStaminas.CanRecoverStamina())
        {
            GameManager.Instance.Player.playerStaminas.RecoverStaminaOrdinary(StaminaValue);
            return true;
        }

        return false;
    }
}
