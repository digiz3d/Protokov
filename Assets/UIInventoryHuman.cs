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
    GameObject backpackContent;

    public void Feed(PlayerInventory inv)
    {
        RefreshRepresentation(weapon1, inv.weapon1);
        RefreshRepresentation(weapon2, inv.weapon2);
        RefreshRepresentation(secondary, inv.secondary);
        RefreshRepresentation(melee, inv.melee);
        RefreshRepresentation(helmet, inv.helmet);
        RefreshRepresentation(armor, inv.armor);
        RefreshRepresentation(backpack, inv.backpack);
        Erase(backpackContent);
    }

    void RefreshRepresentation(GameObject slotRepresentation, InventorySlot slot)
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

    void Erase(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}