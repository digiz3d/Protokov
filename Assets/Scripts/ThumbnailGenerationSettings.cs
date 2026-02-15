using UnityEngine;

[RequireComponent(typeof(InventoryItem))]
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
            if (TryGetComponent<InventoryItem>(out var item))
            {
                item.UpdateThumbnail();
            }
        }
    }

    string makeHash()
    {
        return $"{size}-{width}-{height}-{near}-{far}";
    }
}
