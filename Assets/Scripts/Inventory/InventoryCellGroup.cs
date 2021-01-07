using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCellGroup : MonoBehaviour
{
    public int height = 2;
    public int width = 2;
    public Transform attachPoint;

    [Serializable]
    public struct InventoryItemCoords
    {
        public InventoryItem item;
        public Vector2Int coord;
    }

    public List<InventoryItemCoords> items = new List<InventoryItemCoords>();

    public bool HasItemRootAt(int x, int y)
    {
        int foundIndex = items.FindIndex(item => item.coord.x == x && item.coord.y == y);
        if (foundIndex >= 0) return false;
        return true;
    }

    bool HasItemAt(InventoryItem inventoryItem, int x, int y)
    {
        int foundIndex = items.FindIndex(item =>
        {
            bool isMatchingX = item.coord.x <= x && x <= (item.coord.x + item.item.width - 1);
            bool isMatchingY = item.coord.y <= y && y <= (item.coord.y + item.item.height - 1);
            bool isSameAsOriginal = inventoryItem == item.item;
            return isMatchingX && isMatchingY && !isSameAsOriginal;
        });

        if (foundIndex >= 0) return false;
        return true;
    }

    public (bool, InventoryItem) FindItemAt(int x, int y)
    {
        int foundIndex = items.FindIndex(item => item.coord.x == x && item.coord.y == y);
        if (foundIndex >= 0) return (true, items[foundIndex].item);
        return (false, null);
    }

    public bool TryInsertAt(InventoryItem item, int x, int y)
    {
        bool hasItem = HasItemAt(item, x, y);
        if (!hasItem) return false;

        throw new NotImplementedException("TODO");
    }

    public bool TryAutoInsert(InventoryItem item)
    {
        (bool foundFreeCells, (int x, int y) coords) = AutoFindFreeCoordinateFor(item);
        if (foundFreeCells)
        {
            // todo handle insert object here
            items.Add(new InventoryItemCoords()
            {
                coord = new Vector2Int(coords.x, coords.y),
                item = item,
            });
            item.transform.SetParent(attachPoint, false);
        }
        return foundFreeCells;
    }

    private (bool, (int, int)) AutoFindFreeCoordinateFor(InventoryItem item)
    {
        Dictionary<(int, int), bool> emptyCells = new Dictionary<(int, int), bool>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                emptyCells.Add((x, y), true);
            }
        }

        foreach (InventoryItemCoords itemCoords in items)
        {
            for (int x = 0; x < itemCoords.item.width; x++)
            {
                for (int y = 0; y < itemCoords.item.height; y++)
                {
                    int currentCellX = itemCoords.coord.x + x;
                    int currentCellY = itemCoords.coord.y + y;
                    emptyCells.Remove((currentCellX, currentCellY));
                    emptyCells.Add((currentCellX, currentCellY), false);
                }
            }
        }

        int neededSlots = item.width * item.height;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                emptyCells.TryGetValue((x, y), out bool isEmpty);
                if (isEmpty)
                {
                    int availableSlots = 0;
                    for (int itemX = 0; itemX < item.width; itemX++)
                    {
                        for (int itemY = 0; itemY < item.height; itemY++)
                        {
                            emptyCells.TryGetValue((x + itemX, y + itemY), out bool isEmptyNext);
                            if (isEmptyNext) availableSlots++;
                        }
                    }
                    if (neededSlots == availableSlots)
                    {
                        return (true, (x, y));
                    }
                }
            }
        }
        return (false, (0, 0));
    }
}

