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
        if (!CanAttachItem(_item)) return;

        if (_item.transform.parent != null)
        {
            InventorySlot oldSlot = _item.transform.parent.GetComponent<InventorySlot>();
            if (oldSlot != null)
            {
                oldSlot.item = null;
            }
        }

        _item.transform.SetParent(attachPoint, true);
        item = _item;
        _item.GenerateThumbnail();

        GetComponentInParent<PlayerInventory>().InvalidateUI();
    }

    public void UnregisterItem(InventoryItem inventoryItem)
    {
        if (item == inventoryItem) item = null;
        GetComponentInParent<PlayerInventory>().InvalidateUI();
    }

    bool CanAttachItem(InventoryItem _item)
    {
        if (_item == null) return false;
        if (item) return false;

        foreach (InventoryItemTag tag in requiredTags)
        {
            if (!_item.tags.Contains(tag)) return false;
        }

        return true;
    }
}