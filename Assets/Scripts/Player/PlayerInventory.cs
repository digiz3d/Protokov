using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot
{
    public InventoryItem item = null;
    public string itemSlotName = "";
    public InventoryItemType accepts = InventoryItemType.any;

    public override string ToString()
    {
        string str = $"{itemSlotName} : ";

        if (item != null)
        {
            str += item.ToString();
        }
        else
        {
            str += "-";
        }
        return str;
    }
}

public class PlayerInventory : MonoBehaviour
{
    public InventoryItemSlot weapon1;
    public InventoryItemSlot weapon2;
    public InventoryItemSlot pistol;
    public InventoryItemSlot melee;
    public InventoryItemSlot bag;
    public InventoryItemSlot helmet;

    public LayerMask renderLayer;

    void Start()
    {
        weapon1 = new InventoryItemSlot() { accepts = InventoryItemType.weapon, itemSlotName = "primary weapon" };
        weapon2 = new InventoryItemSlot() { accepts = InventoryItemType.weapon, itemSlotName = "secondary weapon" };
        pistol = new InventoryItemSlot() { accepts = InventoryItemType.pistol, itemSlotName = "pistol" };
        melee = new InventoryItemSlot() { accepts = InventoryItemType.melee, itemSlotName = "melee weapon" };
        bag = new InventoryItemSlot() { accepts = InventoryItemType.bag, itemSlotName = "bag" };
        helmet = new InventoryItemSlot() { accepts = InventoryItemType.helmet, itemSlotName = "helmet" };
    }

    public bool TryTake(InventoryItem item)
    {
        if (!item.isTakable) return false;

        bool res = true;

        if (item.type == InventoryItemType.weapon)
        {
            if (weapon1.item == null) weapon1.item = item;
            else if (weapon2.item == null) weapon2.item = item;
            else res = false;
        }
        else if (item.type == InventoryItemType.pistol)
        {
            if (pistol.item == null) pistol.item = item;
            else res = false;
        }
        else if (item.type == InventoryItemType.bag)
        {
            if (bag.item == null) bag.item = item;
            else res = false;
        }


        if (res == false && bag.item != null && bag.item.isContainer)
            res = bag.item.TryInsert(item);

        if (res) item.CreateThumbnail();

        return res;
    }

    public override string ToString()
    {
        string str = "";
        str += $"{weapon1}\n";
        str += $"{weapon2}\n";
        str += $"{pistol}\n";
        str += $"{melee}\n";
        str += $"{bag}\n";
        str += $"{helmet}\n";
        return str;
    }
}
