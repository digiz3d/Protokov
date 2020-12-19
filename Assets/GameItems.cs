using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour
{
    [Serializable]
    public struct InventoryItem
    {
        public string key;
        public GameObject prefab;
    }
    public InventoryItem[] Items;

    private static InventoryItem[] _items;

    void Awake()
    {
        DontDestroyOnLoad(this);
        _items = Items;
    }

    public static GameObject GetPrefab(string key)
    {
        return Array.Find(_items, x => x.key == key).prefab;
    }
}
