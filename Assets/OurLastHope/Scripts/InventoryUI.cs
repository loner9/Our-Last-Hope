using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    private List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitInventory();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.index = i;
            slots.Add(slot);
        }
    }

    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slots[index];
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }
}
