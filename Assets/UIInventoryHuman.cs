using UnityEngine;
using UnityEngine.UI;

public class UIInventoryHuman : MonoBehaviour
{
    public GameObject itemIconPrefab;

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
        Debug.Log("Feeding UIInventoryHuman with a player Inventory");
        RefreshRepresentation(weapon1, inv.weapon1.item);
        RefreshRepresentation(weapon2, inv.weapon2.item);
        RefreshRepresentation(secondary, inv.secondary.item);
        RefreshRepresentation(melee, inv.melee.item);
        RefreshRepresentation(helmet, inv.helmet.item);
        RefreshRepresentation(armor, inv.armor.item);
        RefreshRepresentation(backpack, inv.backpack.item);
        Erase(backpackContent);
    }

    void RefreshRepresentation(GameObject slot, InventoryItem item)
    {
        Erase(slot);
        if (item == null) return;
        GameObject newItem = Instantiate(itemIconPrefab, slot.transform);
        RawImage rawImage = newItem.GetComponent<RawImage>();
        rawImage.texture = item.thumbnail;
    }

    void Erase(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}