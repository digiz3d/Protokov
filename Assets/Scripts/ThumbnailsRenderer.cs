using UnityEngine;

public class ThumbnailsRenderer : MonoBehaviour
{
    public Light thumbnailLight;
    public static Light staticLight;

    static Transform spawn;

    static Camera cam;

    public const int CELL_SIZE = 40;

    void Start()
    {
        DontDestroyOnLoad(this);
        spawn = transform;
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
        staticLight = thumbnailLight;
    }

    public static void RenderItemTexture(InventoryItem item)
    {
        Debug.Log($"Rendering item : {item.name} from {item.gameObject.name}");
        GameObject go = Instantiate(item.gameObject, spawn.position, spawn.rotation, spawn);
        go.SetActive(true);
        go.GetComponent<Rigidbody>().isKinematic = true;
        go.GetComponent<Rigidbody>().detectCollisions = false;
        staticLight.gameObject.SetActive(true);

        float initialSize = cam.orthographicSize;
        Rect initialRect = cam.rect;
        float initialFar = cam.farClipPlane;
        float initialNear = cam.nearClipPlane;

        ThumbnailGenerationSettings generationSettings = go.GetComponent<ThumbnailGenerationSettings>();
        if (generationSettings != null)
        {
            cam.orthographicSize = generationSettings.size;
            cam.rect = new Rect(0f, 0f, generationSettings.width, generationSettings.height);
            cam.nearClipPlane = generationSettings.near;
            cam.farClipPlane = generationSettings.far;
        }

        Rigidbody[] rbs = go.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        item.Thumbnail = new RenderTexture(CELL_SIZE * item.width * 8, 8 * CELL_SIZE * item.height, 0);
        cam.targetTexture = item.Thumbnail;
        cam.Render();
        cam.targetTexture = null;
        if (generationSettings != null)
        {
            cam.orthographicSize = initialSize;
            cam.rect = initialRect;
            cam.nearClipPlane = initialNear;
            cam.farClipPlane = initialFar;
        }
        staticLight.gameObject.SetActive(false);
        DestroyImmediate(go);
    }
}
