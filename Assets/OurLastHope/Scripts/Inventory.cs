using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [Header("Config")]
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItem[] inventoryItems;

    public InventoryItem[] InventoryItems => inventoryItems;
    public int InventorySize => inventorySize;

    [Header("Testing")]
    public InventoryItem testItem;
    private int lastEquipedIndex = -1;
    public int currentAmmo = 0;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
        VerifyItemsForDraw();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(testItem, 30);
        }
    }

    public void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0) return;
        List<int> indexes = CheckItemStock(item.ID);
        if (item.IsStackable && indexes.Count > 0)
        {
            foreach (int index in indexes)
            {
                int maxStack = item.MaxStack;
                if (inventoryItems[index].Quantity < maxStack)
                {
                    inventoryItems[index].Quantity += quantity;
                    if (inventoryItems[index].Quantity > maxStack)
                    {
                        int diff = inventoryItems[index].Quantity - maxStack;
                        inventoryItems[index].Quantity = maxStack;
                        AddItem(item, diff);
                    }

                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                    return;
                }
            }
        }

        int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity;
        AddItemFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;
        if (remainingAmount > 0)
        {
            AddItem(item, remainingAmount);
        }
    }

    public void UseItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].ItemType != ItemType.Consumable) return;
        if (inventoryItems[index].UseItem())
        {
            DecreaseItemStock(index);
        }
    }

    public int ConsumeAmmo(int index)
    {
        int ammo = 0;
        if (inventoryItems[index] is ItemWeapon itemWeapon)
        {
            if (itemWeapon.currentAmmo > 0)
            {
                ammo = itemWeapon.currentAmmo -= 1;
            }
        }

        return ammo;
    }

    public int ReloadAmmo(string item, int quantity, int index)
    {
        List<int> indexes = CheckAmmoAvailable(item);
        int currentAmmo = 0;
        int quantityToUse = 0;
        Debug.Log(indexes.Count);
        if (indexes.Count > 0)
        {
            if (inventoryItems[index] is ItemWeapon weap)
            {
                currentAmmo = weap.currentAmmo;
            }

            int ammoDiff = Mathf.Abs(currentAmmo - quantity);
            quantityToUse = ammoDiff >= inventoryItems[indexes[0]].Quantity ? inventoryItems[indexes[0]].Quantity : ammoDiff;
            DecreaseItemStockByQuantity(indexes[0], quantityToUse);
            if (inventoryItems[index] is ItemWeapon itemWeapon)
            {
                itemWeapon.currentAmmo += quantityToUse;
                currentAmmo = itemWeapon.currentAmmo;
                Debug.Log("CURRENT AMMO " + itemWeapon.currentAmmo);
            }

            return currentAmmo;
        }

        return currentAmmo;
    }

    public void RemoveItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].Removable())
        {
            inventoryItems[index].RemoveItem();
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        }

    }

    public void EquipItem(int index)
    {
        if (inventoryItems[index] == null) return;
        if (inventoryItems[index].ItemType != ItemType.Weapon) return;
        WeaponVisualController.OnWeaponChange(inventoryItems[index].ID);
        if (inventoryItems[index] is ItemWeapon itemWeapon)
        {
            string weaponType = itemWeapon.weapon.WeaponType.ToString();
            currentAmmo = itemWeapon.currentAmmo;
            WeaponManager.OnWeaponTypeChanged(weaponType);
            Debug.Log("weaponDetail : " + itemWeapon.weapon.Damage);
            WeaponManager.OnWeaponChanged(itemWeapon.ID, itemWeapon.weapon.MagazineSize, index, itemWeapon.weapon.Damage);
            if (lastEquipedIndex != -1 && lastEquipedIndex != index)
            {
                if (inventoryItems[lastEquipedIndex] is ItemWeapon weap)
                {
                    weap.isEquipped = false;
                }
            }


            if (lastEquipedIndex == index)
            {
                // already equiped -> unequip
                lastEquipedIndex = -1;
                itemWeapon.isEquipped = false;
                WeaponManager.OnWeaponTypeChanged("unarmed");
                WeaponVisualController.OnWeaponChange("unarmed");
            }
            else
            {
                lastEquipedIndex = index;
                itemWeapon.isEquipped = true;
            }

        }

    }

    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    private void DecreaseItemStock(int index)
    {
        inventoryItems[index].Quantity--;
        if (inventoryItems[index].Quantity <= 0)
        {
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        }
        else
        {
            InventoryUI.Instance.DrawItem(inventoryItems[index], index);
        }
    }

    private void DecreaseItemStockByQuantity(int index, int quantity)
    {
        inventoryItems[index].Quantity -= quantity;
        if (inventoryItems[index].Quantity <= 0)
        {
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        }
        else
        {
            InventoryUI.Instance.DrawItem(inventoryItems[index], index);
        }
    }

    private List<int> CheckItemStock(String itemId)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].ID == itemId)
            {
                itemIndexes.Add(i);
            }
        }

        return itemIndexes;
    }

    public List<int> CheckAmmoAvailable(string itemId)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].ID.ToLower().Contains(itemId.ToLower()) && inventoryItems[i].Name.ToLower().Contains("ammo"))
            {
                itemIndexes.Add(i);
            }
        }

        return itemIndexes;
    }

    public bool CheckKeysPresent(params string[] keys)
    {
        int keyAmount = 0;
        foreach (string key in keys)
        {
            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i] == null) continue;
                if (inventoryItems[i].ID.ToLower().Equals(key.ToLower()))
                {
                    keyAmount++;
                }
            }
        }
        if (keys.Length == keyAmount){
            return true;
        }
        return false;
    }

    private void VerifyItemsForDraw()
    {

        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                InventoryUI.Instance.DrawItem(null, i);
            }
        }
    }
}
