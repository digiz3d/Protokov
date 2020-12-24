using UnityEngine;

public class ThumbnailsRenderer : MonoBehaviour
{
    static Transform spawn;

    static Camera cam;

    void Start()
    {
        DontDestroyOnLoad(this);
        spawn = transform;
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
    }

    public static void RenderItemToTexture(InventoryItem item, RenderTexture thumbnail)
    {
        Debug.Log($"[Thumbmnails] Generating {item.gameObject.name} picture.");
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

        cam.targetTexture = thumbnail;
        cam.Render();
        cam.targetTexture = null;
        DestroyImmediate(go);
    }
}
