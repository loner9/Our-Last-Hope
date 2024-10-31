using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;

    private List<InventorySlot> slots = new List<InventorySlot>();
    public InventorySlot SelectedSlot { get; set; }

    [SerializeField] private GameObject descPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private PlayerControls controls;
    private bool isInventoryOpen = false;
    public bool IsInventoryOpen => isInventoryOpen;

    [SerializeField]
    private Transform inventoryTransform;

    private void Awake()
    {
        Instance = this;
        InitInventory();
        controls = new PlayerControls();

        controls.UI.Inventory.performed += ctx => ToggleInventory();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ToggleInventory()
    {
        Debug.Log("Toggle Inventory");
        if (!isInventoryOpen)
        {
            inventoryTransform.gameObject.SetActive(true);
        }
        else
        {
            inventoryTransform.gameObject.SetActive(false);
            descPanel.SetActive(false);
            SelectedSlot = null;
        }

        isInventoryOpen = !isInventoryOpen;
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

    public void UseItem()
    {
        Inventory.Instance.UseItem(SelectedSlot.index);
    }

    public void RemoveItem()
    {
        if (SelectedSlot == null) return;
        Inventory.Instance.RemoveItem(SelectedSlot.index);
    }

    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slots[index];
        if (item == null)
        {
            slot.ShowSlotInformation(false);
            return;
        }
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }

    public void ShowItemDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] == null) return;
        {
            descPanel.SetActive(true);
            itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
            itemName.text = Inventory.Instance.InventoryItems[index].Name;
            itemDescription.text = Inventory.Instance.InventoryItems[index].Description;
        }
    }

    private void SlotSelectedCallBack(int index)
    {
        SelectedSlot = slots[index];
        ShowItemDescription(index);
    }

    void OnEnable()
    {
        InventorySlot.OnSlotSelectedEvent += SlotSelectedCallBack;
        controls.Enable();
    }

    void OnDisable()
    {
        InventorySlot.OnSlotSelectedEvent -= SlotSelectedCallBack;
        controls.Disable();
    }
}
