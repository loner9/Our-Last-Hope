using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStatsUI : MonoBehaviour
{
    [SerializeField] private Slider damageSlider;
    [SerializeField] private Slider rangeSlider;
    [SerializeField] private Slider accuracySlider;
    [SerializeField] private Slider fireRateSlider;

    // Reference to the Inventory UI script
    [SerializeField] private InventoryUI inventoryUI;

    private void Start()
    {
        // Set slider min and max values
        SetSliderValues(damageSlider);
        SetSliderValues(rangeSlider);
        SetSliderValues(accuracySlider);
        SetSliderValues(fireRateSlider);

        // Subscribe to inventory slot selection event
        InventorySlot.OnSlotSelectedEvent += OnSlotSelected;
    }

    private void OnDestroy()
    {
        // Unsubscribe from inventory slot selection event
        InventorySlot.OnSlotSelectedEvent -= OnSlotSelected;
    }

    private void SetSliderValues(Slider slider)
    {
        slider.minValue = 0;
        slider.maxValue = 20;
    }

    private void OnSlotSelected(int index)
    {
        if (Inventory.Instance.InventoryItems[index] is ItemWeapon selectedWeapon)
        {
            UpdateSliders(selectedWeapon);
        }
    }

    private void UpdateSliders(ItemWeapon weapon)
    {
        damageSlider.value = weapon.damage;
        rangeSlider.value = weapon.range;
        accuracySlider.value = weapon.accuracy;
        fireRateSlider.value = weapon.fireRate;
    }
}
