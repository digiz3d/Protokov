using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public List<InventoryItemTag> requiredTags;
    public Transform attachPoint;
    public InventoryItem item;
    
    public void AttachItem(InventoryItem _item)
    {
        _item.transform.SetParent(attachPoint, true);
        item = _item;
        _item.GenerateThumbnail();
    }
}