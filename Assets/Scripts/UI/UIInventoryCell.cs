using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryCell : MonoBehaviour, IDropHandler
{
    InventoryCellGroup representedCellGroup;
    int x;
    int y;

    public void Setup(InventoryCellGroup cellGroup, int _x, int _y)
    {
        representedCellGroup = cellGroup;
        x = _x;
        y = _y;
    }

    public void OnDrop(PointerEventData eventData)
    {
        representedCellGroup.TryInsertAt(UIInventory.currentlyDraggingItem, x, y);
        Debug.Log($"on drop on cell {x} {y}");
    }
}
