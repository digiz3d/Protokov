using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InventoryItemType
{
    any = 0,
    weapon = 1,
    pistol = 2,
    bag = 3,
    helmet = 4,
    melee = 5,
}

public class InventoryItem : MonoBehaviour
{
    public string itemName = "Item";
    public int height = 1;
    public int width = 1;
    public bool isRotated = false;

    public bool isTakable = true;

    public bool isContainer = false;
    public int containerHeight = 5;
    public int containerWidth = 3;
    public bool isOpenable = false;

    public InventoryItemType type;

    Dictionary<(int, int), InventoryItemSlot> containerCells;

    private void Start()
    {
        if (isContainer)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    containerCells.Add((i, j), new InventoryItemSlot());
                }
            }
        }
    }

    public bool TryInsert(InventoryItem item)
    {
        if (!isContainer) return false;

        int neededSlots = item.width * item.height;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                containerCells.TryGetValue((i, j), out InventoryItemSlot slot);
                if (slot != null)
                {
                    int availableSlots = 0;
                    List<InventoryItemSlot> slots = new List<InventoryItemSlot>();
                    for (int w = 0; w < item.height; w++)
                    {
                        for (int x = 0; x < item.width; x++)
                        {
                            containerCells.TryGetValue((w, x), out InventoryItemSlot currentSlot);
                            slots.Add(currentSlot);
                            if (currentSlot != null) availableSlots++;
                        }
                    }
                    if (neededSlots == availableSlots)
                    {
                        foreach (InventoryItemSlot availableSlot in slots)
                        {
                            availableSlot.item = item;
                        }
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public override string ToString()
    {
        if (!isContainer) return itemName;

        string str = $"{itemName} (";
        string.Join(",", containerCells.Values);
        return $"{str} )";
    }
}
