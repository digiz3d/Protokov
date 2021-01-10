using UnityEngine;

public class ThumbnailsRenderer : MonoBehaviour
{
    static Transform spawn;

    static Camera cam;

    public const int CELL_SIZE = 40;

    void Start()
    {
        DontDestroyOnLoad(this);
        spawn = transform;
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
    }

    public static void RenderItemTexture(InventoryItem item)
    {
        Debug.Log($"Rendering item : {item.name} from {item.gameObject.name}");
        GameObject go = Instantiate(item.gameObject, spawn.position, spawn.rotation, spawn);
        go.SetActive(true);
        go.GetComponent<Rigidbody>().isKinematic = true;
        go.GetComponent<Rigidbody>().detectCollisions = false;

        Rigidbody[] rbs = go.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        item.Thumbnail = new RenderTexture(CELL_SIZE * item.width * 10, 10 * CELL_SIZE * item.height, 0);
        cam.targetTexture = item.Thumbnail;
        cam.Render();
        cam.targetTexture = null;
        DestroyImmediate(go);
    }
}
