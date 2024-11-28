using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    [SerializeField]
    private InventoryItem item;
    [SerializeField] 
    private int amount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player")
        {
            Inventory.Instance.AddItem(item, amount);
            Destroy(this.gameObject);
        }
    }
}
