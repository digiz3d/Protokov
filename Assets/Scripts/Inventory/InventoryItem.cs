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
    stanag
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

        InventoryCellGroup[] cellGroups = GetComponents<InventoryCellGroup>();

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
}
