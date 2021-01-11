using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbnailGenerationSettings : MonoBehaviour
{
    public float size = 1f;

    public float width = 1f;
    public float height = 1f;

    public float near = 1f;
    public float far = 1f;

    public string previousHash = "";

    void Start()
    {
        previousHash = makeHash();
    }

    void Update()
    {
        string newHash = makeHash();
        if (previousHash != newHash)
        {
            previousHash = newHash;
            InventoryItem item = GetComponent<InventoryItem>();
            if (item != null)
                item.UpdateThumbnail();
        }
    }

    string makeHash()
    {
        return $"{size}-{width}-{height}-{near}-{far}";
    }
}
