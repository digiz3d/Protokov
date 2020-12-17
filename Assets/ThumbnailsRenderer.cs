using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbnailsRenderer : MonoBehaviour
{
    static Transform spawn;

    static Camera cam;

    void Start()
    {
        spawn = transform;
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
    }

    public static void RenderItemToTexture(InventoryItem item, RenderTexture thumbnail)
    {
        Debug.Log($"[Thumbmnails] Generating {item.gameObject.name} picture.");
        GameObject go = Instantiate(item.gameObject, spawn.position, spawn.rotation,spawn );
        go.GetComponent<Rigidbody>().isKinematic = true;
        go.GetComponent<Rigidbody>().detectCollisions = false;
        cam.targetTexture = thumbnail;
        cam.Render();
        cam.targetTexture = null;
        Destroy(go);
    }
}
