using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent;
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image qtyContainer;
    [SerializeField] private TextMeshProUGUI qntyText;

    public int index {get; set;}

    public void ClickSlot(){
        OnSlotSelectedEvent?.Invoke(index);
    }

    public void UpdateSlot(InventoryItem item){
        itemIcon.sprite = item.Icon;
        qntyText.text = item.Quantity.ToString();
    }

    public void ShowSlotInformation(bool value){
        itemIcon.gameObject.SetActive(value);
        qtyContainer.gameObject.SetActive(value);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
