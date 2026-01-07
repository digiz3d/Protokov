using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryDroppableSlot : MonoBehaviour, IDropHandler
{
    public InventorySlot representedSlot;

    public void OnDrop(PointerEventData e)
    {
        Debug.Log($"Dropped on slot : name={UIInventory.currentlyDraggingItem} into {representedSlot.name}");
        representedSlot.AttachItem(UIInventory.currentlyDraggingItem);
    }
}
