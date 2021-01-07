using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryCell : MonoBehaviour, IDropHandler
{
    InventoryCellGroup representedCellGroup;
    int x;
    int y;

    public void Setup(InventoryCellGroup cellGroup, int x, int y)
    {
        representedCellGroup = cellGroup;
        this.x = x;
        this.y = y;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"on drop on cell {x} {y}");
    }
}
