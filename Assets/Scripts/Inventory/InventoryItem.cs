using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItemTag : byte
{
    weapon,
    primary,
    secondary,
    helmet,
    melee,
    backpack,
    magazine,
    stanag,
    armor
}

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public List<InventoryItemTag> tags;
    public int height = 1;
    public int width = 1;
    public float weight = 0f;
    public bool stackable = false;
    public int quantity = 1;

    public const int CELL_SIZE = 40;

    public RenderTexture thumbnail;

    public override string ToString()
    {
        InventorySlot[] slots = GetComponents<InventorySlot>();
        string slotStr = "";
        if (slots.Length > 0)
        {
            slotStr = ", slots: (";
            foreach (InventorySlot slot in slots)
            {
                if (slot.item != null)
                {
                    slotStr += $"slot: {slot.item},";
                }
            }
            slotStr += ")";
        }

        InventoryCellGroup[] cellGroups = GetComponentsInChildren<InventoryCellGroup>(true);

        string cellGroupStr = "";
        if (cellGroups.Length > 0)
        {
            cellGroupStr = ", cellGroups: (";
            foreach (InventoryCellGroup cellGroup in cellGroups)
            {
                foreach (InventoryCellGroup.InventoryItemCoords itemCoords in cellGroup.items)
                {
                    cellGroupStr += $"at {itemCoords.coord.x},{itemCoords.coord.y} : {itemCoords.item}";
                }
            }
            cellGroupStr += ")";
        }


        return $"{{ itemName: {itemName}, quantity: {quantity} {slotStr} {cellGroupStr} }}";
    }

    public void UnregisterFromParent()
    {
        if (transform.parent != null)
        {
            InventoryCellGroup cellGroup = transform.parent.GetComponent<InventoryCellGroup>();
            if (cellGroup != null) cellGroup.UnregisterItem(this);

            InventorySlot slot = transform.parent.GetComponent<InventorySlot>();
            if (slot != null) slot.UnregisterItem(this);
        }

    }

    public void GenerateThumbnail()
    {
        thumbnail = new RenderTexture(CELL_SIZE * width * 10, 10 * CELL_SIZE * height, 0);
        ThumbnailsRenderer.RenderItemToTexture(this, thumbnail);
    }
}
