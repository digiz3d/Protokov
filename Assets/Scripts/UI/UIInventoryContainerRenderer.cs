using System;
using UnityEngine;

public class UIInventoryContainerRenderer : MonoBehaviour
{
    public const int CELL_SIZE = 80;
    public const int CELL_GROUPS_MARGIN = 20;

    public GameObject prefabInventoryCell;
    public GameObject prefabInventoryDraggableItem;

    GameObject currentlyRendererContainer;
    Canvas baseCanvas;

    public void SetBaseCanvas(Canvas _baseCanvas)
    {
        baseCanvas = _baseCanvas;
    }

    public void Render()
    {
        if (currentlyRendererContainer == null) return;

        InventoryCellGroup[] cellGroups = currentlyRendererContainer.GetComponents<InventoryCellGroup>();

        if (cellGroups.Length == 0) throw new Exception("A backpack should always have cellgroups");

        int offsetY = 0;

        foreach (InventoryCellGroup cellGroup in cellGroups)
        {
            for (int x = 0; x < cellGroup.width; x++)
            {
                for (int y = 0; y < cellGroup.height; y++)
                {
                    GameObject go = Instantiate(prefabInventoryCell, transform);
                    go.name = $"Cell {x} {y}";
                    RectTransform rect = go.GetComponent<RectTransform>();
                    rect.sizeDelta = new Vector2(CELL_SIZE, CELL_SIZE);
                    rect.anchoredPosition = new Vector3(CELL_SIZE * x, -CELL_SIZE * y - offsetY, 0);
                    UIInventoryCell cell = go.GetComponent<UIInventoryCell>();
                    cell.Setup(cellGroup, x, y);
                }
            }
            offsetY += cellGroup.height * CELL_SIZE + CELL_GROUPS_MARGIN;
        }

        offsetY = 0;

        foreach (InventoryCellGroup cellGroup in cellGroups)
        {
            for (int x = 0; x < cellGroup.width; x++)
            {
                for (int y = 0; y < cellGroup.height; y++)
                {
                    (bool hasItem, InventoryItem item) = cellGroup.FindItemAt(x, y);
                    Debug.Log($"hasItem={hasItem}, x = {x}, y ={y}");
                    if (hasItem)
                    {
                        GameObject go = Instantiate(prefabInventoryDraggableItem, transform);
                        go.name = $"Item {item.itemName}";
                        RectTransform rect = go.GetComponent<RectTransform>();
                        rect.sizeDelta = new Vector2(CELL_SIZE * item.width, CELL_SIZE * item.height);
                        rect.anchoredPosition = new Vector3(CELL_SIZE * x, -CELL_SIZE * y - offsetY, 0);
                        UIInventoryDraggableItem draggableItem = go.GetComponent<UIInventoryDraggableItem>();
                        //draggableItem.representedItem = slot.item;
                        draggableItem.baseCanvas = baseCanvas;
                    }
                }
            }
            offsetY += cellGroup.height * CELL_SIZE + CELL_GROUPS_MARGIN;
        }

        int totalYSize = 0;
        foreach (InventoryCellGroup cellGroup in cellGroups)
        {
            if (cellGroup != cellGroups[0])
                totalYSize += CELL_GROUPS_MARGIN;

            for (int h = 0; h < cellGroup.height; h++)
            {
                totalYSize += CELL_SIZE;
            }
        }

        RectTransform currentTransform = GetComponent<RectTransform>();
        currentTransform.sizeDelta = new Vector2(currentTransform.sizeDelta.x, totalYSize);
    }

    public void SetCurrentlyRendedItem(GameObject container)
    {
        currentlyRendererContainer = container;
    }
}
