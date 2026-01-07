using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public UIInventory inventoryRenderer;
    public InventorySlot weapon1;
    public InventorySlot weapon2;
    public InventorySlot secondary;
    public InventorySlot melee;
    public InventorySlot backpack;
    public InventorySlot helmet;
    public InventorySlot armor;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(ToString());
        }
    }

    public bool TryTake(InventoryItem item)
    {
        bool res = true;

        if (item.tags.Contains(InventoryItemTag.primary) && item.tags.Contains(InventoryItemTag.weapon))
        {
            if (weapon1.item == null) weapon1.AttachItem(item);
            else if (weapon2.item == null) weapon2.AttachItem(item);
            else res = false;
        }
        else if (item.tags.Contains(InventoryItemTag.secondary) && item.tags.Contains(InventoryItemTag.weapon))
        {
            if (secondary.item == null) secondary.AttachItem(item);
            else res = false;
        }
        else if (item.tags.Contains(InventoryItemTag.backpack))
        {
            if (backpack.item == null) backpack.AttachItem(item);
            else res = false;
        }
        else
        {
            res = false;
        }


        if (res == false && backpack.item != null)
        {
            Debug.Log("It is trying to go into backpack");
            InventoryCellGroup[] cellGroups = backpack.item.GetComponentsInChildren<InventoryCellGroup>(true);
            foreach (var cellGroup in cellGroups)
            {
                res = cellGroup.TryAutoInsert(item);
                if (res) break;
            }
        }

        if (inventoryRenderer != null) InvalidateUI();

        return res;
    }

    public void InvalidateUI()
    {
        inventoryRenderer.Invalidate();
    }

    public override string ToString()
    {
        string str = "";
        str += $"{weapon1}\n";
        str += $"{weapon2}\n";
        str += $"{secondary}\n";
        str += $"{melee}\n";
        str += $"{backpack}\n";
        str += $"{helmet}\n";
        return str;
    }
}
