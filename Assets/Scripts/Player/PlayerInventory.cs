using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerInventory : MonoBehaviour
{
    public UIPlayerInventoryRenderer uiRenderer;
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


        if (res == false && backpack.item != null)
        {
            InventoryCellGroup[] cellGroups = backpack.item.GetComponents<InventoryCellGroup>();
            foreach (var cellGroup in cellGroups)
            {
                res = cellGroup.TryAutoInsert(item);
                if (res) break;
            }
        }

        if (uiRenderer != null) InvalidateUI();

        return res;
    }

    public void InvalidateUI()
    {
        uiRenderer.Invalidate();
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
