using System;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryHuman : MonoBehaviour
{
    public GameObject itemIconPrefab;

    public Canvas baseCanvas;

    [SerializeField]
    GameObject weapon1;
    [SerializeField]
    GameObject weapon2;
    [SerializeField]
    GameObject secondary;
    [SerializeField]
    GameObject melee;
    [SerializeField]
    GameObject helmet;
    [SerializeField]
    GameObject armor;
    [SerializeField]
    GameObject backpack;
    [SerializeField]
    UIInventoryContainerRenderer backpackContent;

    public void Feed(PlayerInventory inv)
    {
        RefreshSlotRepresentation(weapon1, inv.weapon1);
        RefreshSlotRepresentation(weapon2, inv.weapon2);
        RefreshSlotRepresentation(secondary, inv.secondary);
        RefreshSlotRepresentation(melee, inv.melee);
        RefreshSlotRepresentation(helmet, inv.helmet);
        RefreshSlotRepresentation(armor, inv.armor);
        RefreshSlotRepresentation(backpack, inv.backpack);
        RefreshBackpackContentRepresentation(backpackContent, inv.backpack);
    }

    void RefreshSlotRepresentation(GameObject slotRepresentation, InventorySlot slot)
    {
        Erase(slotRepresentation);
        slotRepresentation.GetComponent<UIInventoryDroppableSlot>().representedSlot = slot;
        if (slot.item == null) return;

        GameObject newItem = Instantiate(itemIconPrefab, slotRepresentation.transform);
        RawImage rawImage = newItem.GetComponent<RawImage>();
        rawImage.texture = slot.item.thumbnail;
        UIInventoryDraggableItem draggableItem = newItem.GetComponent<UIInventoryDraggableItem>();
        draggableItem.representedItem = slot.item;
        draggableItem.baseCanvas = baseCanvas;
    }

    void RefreshBackpackContentRepresentation(UIInventoryContainerRenderer backpackContainer, InventorySlot backpackSlot)
    {
        Erase(backpackContainer.gameObject);
        if (backpackSlot.item == null) return;

        backpackContainer.SetCurrentlyRendedItem(backpackSlot.item.gameObject);
        backpackContainer.Render();
    }

    void Erase(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}