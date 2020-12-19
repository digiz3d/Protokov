using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCellGroup : MonoBehaviour
{
    public int height = 2;
    public int width = 2;

    [Serializable]
    public struct InventoryItemCoords
    {
        public InventoryItem item;
        public Vector2 coord;
    }

    public List<InventoryItemCoords> items = new List<InventoryItemCoords>();

    public bool TryAutoInsert(InventoryItem item)
    {
        (bool foundFreeCells, (int x, int y) coords) = AutoFindFreeCoordinateFor(item);
        if (foundFreeCells)
        {
            // todo handle insert object here
            items.Add(new InventoryItemCoords()
            {
                coord = new Vector2(coords.x, coords.y),
                item = item,
            });
        }
        return foundFreeCells;
    }

    private (bool, (int, int)) AutoFindFreeCoordinateFor(InventoryItem item)
    {
        Dictionary<(int, int), bool> emptyCells = new Dictionary<(int, int), bool>();

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
                emptyCells.Add((i, j), true);

        foreach (InventoryItemCoords itemCoords in items)
        {
            for (int i = 0; i < itemCoords.item.height; i++)
                for (int j = 0; j < itemCoords.item.width; j++)
                    emptyCells.Add((i, j), false);
        }

        int neededSlots = item.width * item.height;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                emptyCells.TryGetValue((i, j), out bool isEmpty);
                if (isEmpty)
                {
                    int availableSlots = 0;
                    List<OldInventoryItemSlot> slots = new List<OldInventoryItemSlot>();
                    for (int w = 0; w < item.height; w++)
                    {
                        for (int x = 0; x < item.width; x++)
                        {
                            emptyCells.TryGetValue((w, x), out bool isEmptyNext);
                            if (isEmptyNext) availableSlots++;
                        }
                    }
                    if (neededSlots == availableSlots)
                    {
                        return (true, (i, j));
                    }
                }
            }
        }
        return (false, (0, 0));
    }
}

