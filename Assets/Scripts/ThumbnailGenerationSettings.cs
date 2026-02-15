using UnityEngine;

[RequireComponent(typeof(InventoryItem))]
public class ThumbnailGenerationSettings : MonoBehaviour
{
    public float size = 1f;

    public int width = 1;
    public int height = 1;

    public int near = 1;
    public int far = 1;

    public string previousHash = "";

    [SerializeField]
    private RenderTexture thumbnailPreview;

    void Start()
    {
        previousHash = makeHash();
    }

    void Update()
    {
        var newHash = makeHash();
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

    public void GenerateThumbnailPreview()
    {
        if (!TryGetComponent<InventoryItem>(out var item))
        {
            Debug.LogError("ThumbnailGenerationSettings requires an InventoryItem component on the same GameObject.");
            return;
        }
        ThumbnailsRenderer.RenderItemTexture(item);
        thumbnailPreview = item.Thumbnail;
    }
}
