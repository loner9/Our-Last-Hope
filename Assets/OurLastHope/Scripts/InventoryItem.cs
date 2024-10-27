using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    KeyItem,
    Note
}

[CreateAssetMenu(menuName = "ScriptableObjects/InventoryItem")]

public class InventoryItem : ScriptableObject
{
    [Header("Config")]
    public string ID;
    public Sprite Icon;
    public string Name;
    [TextArea] public string Description;

    [Header("Info")]
    public ItemType ItemType;
    public bool IsConsumable;
    public bool IsStackable;
    public int MaxStack;

    [HideInInspector] public int MaxQuantity;

    public InventoryItem CopyItem()
    {
        InventoryItem instance = Instantiate(this);
        return instance;
    }

    public virtual bool UseItem()
    {
        return true;
    }

    public virtual bool EquipItem()
    {
        return true;
    }

    public virtual bool RemoveItem()
    {
        return true;
    }

}
