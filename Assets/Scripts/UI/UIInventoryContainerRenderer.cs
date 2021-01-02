using System;
using UnityEngine;

public class UIInventoryContainerRenderer : MonoBehaviour
{
    public const int CELL_SIZE = 80;

    public GameObject prefabInventoryCell;

    GameObject currentlyRendererContainer;

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
                }
            }
            offsetY += (cellGroup.height+1) * CELL_SIZE;
        }

        Debug.Log($"It's alright {cellGroups.Length}");
    }

    public void SetCurrentlyRendedItem(GameObject container)
    {
        currentlyRendererContainer = container;
    }
}
