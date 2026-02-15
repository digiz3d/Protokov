using UnityEngine;

public class ThumbnailsRenderer : MonoBehaviour
{
    public Light thumbnailLight;
    public static GameObject staticLightGo;

    static Transform spawn;

    static Camera cam;

    public const int CELL_SIZE = 40;

    void Start()
    {
        DontDestroyOnLoad(this);
        spawn = transform;
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
        staticLightGo = thumbnailLight.gameObject;
    }

    public static void RenderItemTexture(InventoryItem item)
    {
#if UNITY_EDITOR
        // Ensure static fields are initialized

        if (spawn == null || cam == null || staticLightGo == null)
        {
            ThumbnailsRenderer renderer = FindFirstObjectByType<ThumbnailsRenderer>();
            if (renderer == null)
            {
                Debug.LogError("ThumbnailsRenderer not found in scene! Please add it to the scene.");
                return;
            }
            spawn = renderer.transform;
            cam = renderer.GetComponentInChildren<Camera>();
            staticLightGo = renderer.thumbnailLight.gameObject;
            if (cam == null)
            {
                Debug.LogError("Camera not found as child of ThumbnailsRenderer!");
                return;
            }
        }
#endif

        var go = Instantiate(item.gameObject, spawn.position, Quaternion.LookRotation(Vector3.back, Vector3.up), spawn);

        if (!go.TryGetComponent<ThumbnailGenerationSettings>(out var generationSettings))
        {
            DestroyImmediate(go);
            return;
        }

        go.SetActive(true);
        if (go.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            rigidbody.isKinematic = true;
            rigidbody.detectCollisions = false;
        }
        staticLightGo.SetActive(true);




        cam.orthographicSize = generationSettings.size;
        cam.rect = new Rect(0f, 0f, generationSettings.width, generationSettings.height);
        cam.nearClipPlane = generationSettings.near;
        cam.farClipPlane = generationSettings.far;


        var rbs = go.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        var texture = new RenderTexture(CELL_SIZE * item.width * 8, 8 * CELL_SIZE * item.height, 0);
        item.Thumbnail = texture;
        cam.targetTexture = texture;
        cam.Render();
        cam.targetTexture = null;

        staticLightGo.SetActive(false);
        DestroyImmediate(go);
    }
}
