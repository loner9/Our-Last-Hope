using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemKey_", menuName ="Items/Key")]
public class ItemKey : InventoryItem
{
    public override bool UseItem()
    {
        return false;
    }

    public override bool Removable()
    {
        return false;
    }
}
