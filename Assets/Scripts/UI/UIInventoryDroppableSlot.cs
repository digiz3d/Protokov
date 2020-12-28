using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryDroppableSlot : MonoBehaviour, IDropHandler
{
    public InventorySlot representedSlot;

    public void OnDrop(PointerEventData e)
    {
        Debug.Log($"GOOOOD OnDrop : name={UIPlayerInventoryRenderer.currentlyDraggingItem} into {representedSlot.name}");
        representedSlot.AttachItem(UIPlayerInventoryRenderer.currentlyDraggingItem);
    }
}
