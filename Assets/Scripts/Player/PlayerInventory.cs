using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerInventory : MonoBehaviour
{
    public InventorySlotSerialized weapon1 = new InventorySlotSerialized();
    public InventorySlotSerialized weapon2 = new InventorySlotSerialized();
    public InventorySlotSerialized pistol = new InventorySlotSerialized();
    public InventorySlotSerialized melee = new InventorySlotSerialized();
    public InventorySlotSerialized backpack = new InventorySlotSerialized();
    public InventorySlotSerialized helmet = new InventorySlotSerialized();

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

        if (item.tags.Contains("primary") && item.tags.Contains("weapon"))
        {
            if (weapon1.item == null) weapon1.item = item;
            else if (weapon2.item == null) weapon2.item = item;
            else res = false;
        }
        else if (item.tags.Contains("secondary") && item.tags.Contains("weapon"))
        {
            if (pistol.item == null) pistol.item = item;
            else res = false;
        }
        else if (item.tags.Contains("backpack"))
        {
            if (backpack.item == null) backpack.item = item;
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

        //if (res) item.CreateThumbnail();

        return res;
    }

    public override string ToString()
    {
        string str = "";
        str += $"{weapon1}\n";
        str += $"{weapon2}\n";
        str += $"{pistol}\n";
        str += $"{melee}\n";
        str += $"{backpack}\n";
        str += $"{helmet}\n";
        return str;
    }
}
