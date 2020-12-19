using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum OldInventoryItemType
{
    any = 0,
    weapon = 1,
    pistol = 2,
    bag = 3,
    helmet = 4,
    melee = 5,
}

public class OldInventoryItemSlot
{
    public OldIOnventoryItem item = null;
    public string itemSlotName = "";
    public OldInventoryItemType accepts = OldInventoryItemType.any;

    public override string ToString()
    {
        string str = $"{itemSlotName} : ";

        if (item != null)
        {
            str += item.ToString();
        }
        else
        {
            str += "-";
        }
        return str;
    }
}

public class OldIOnventoryItem : MonoBehaviour
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

    public OldInventoryItemType type;

    public RenderTexture thumbnail;

    Dictionary<(int, int), OldInventoryItemSlot> containerCells;

    private void Start()
    {
        thumbnail = new RenderTexture(256, 256, 1);
        if (isContainer)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    containerCells.Add((i, j), new OldInventoryItemSlot());
                }
            }
        }
    }

    public bool TryInsert(OldIOnventoryItem item)
    {
        if (!isContainer) return false;

        int neededSlots = item.width * item.height;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                containerCells.TryGetValue((i, j), out OldInventoryItemSlot slot);
                if (slot != null)
                {
                    int availableSlots = 0;
                    List<OldInventoryItemSlot> slots = new List<OldInventoryItemSlot>();
                    for (int w = 0; w < item.height; w++)
                    {
                        for (int x = 0; x < item.width; x++)
                        {
                            containerCells.TryGetValue((w, x), out OldInventoryItemSlot currentSlot);
                            slots.Add(currentSlot);
                            if (currentSlot != null) availableSlots++;
                        }
                    }
                    if (neededSlots == availableSlots)
                    {
                        foreach (OldInventoryItemSlot availableSlot in slots)
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

    public void CreateThumbnail()
    {
        ThumbnailsRenderer.RenderItemToTexture(this, thumbnail);
    }

    public override string ToString()
    {
        if (!isContainer) return itemName;

        string str = $"{itemName} (";
        string.Join(",", containerCells.Values);
        return $"{str} )";
    }
}
