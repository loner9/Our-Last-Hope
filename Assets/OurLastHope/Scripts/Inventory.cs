using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [Header("Config")]
    [SerializeField] private int inventorySize;
    [SerializeField] private InventoryItem[] inventoryItems;
    public int InventorySize => inventorySize;

    [Header("Testing")]
    public InventoryItem testItem;
    
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)){
            inventoryItems[0] = testItem.CopyItem();
            inventoryItems[0].Quantity = 1;
            InventoryUI.Instance.DrawItem(inventoryItems[0], 0);
        }
    }
}
