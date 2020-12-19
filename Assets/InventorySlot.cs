using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InventorySlot : MonoBehaviour
{
    public List<string> requiredTags;
    public InventoryItem item;
}

[System.Serializable]
public class InventorySlotSerialized
{
    public List<string> requiredTags;
    public InventoryItem item;

    public override string ToString()
    {
        return $"{{item: {item}, requiredTags: {string.Join(",",requiredTags)},}}";
    }
}
