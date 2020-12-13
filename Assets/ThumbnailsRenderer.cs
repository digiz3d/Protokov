using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbnailsRenderer : MonoBehaviour
{
    public Transform ItemSpawn;

    public static Transform spawn;

    static Camera cam;

    void Start()
    {
        spawn = ItemSpawn;
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
    }

    public static void RenderItemToTexture(InventoryItem item, RenderTexture thumbnail)
    {
        GameObject go = item.gameObject;
        int previousLayer = go.layer;
        go.layer = LayerMask.NameToLayer("ThumbnailGenerator");
        Vector3 originalPos = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);

        go.transform.position = new Vector3(spawn.position.x, spawn.position.y, spawn.position.z);
        cam.targetTexture = thumbnail;
        cam.Render();

        go.transform.position = new Vector3(originalPos.x, originalPos.y, originalPos.z);
        go.layer = previousLayer;
    }
}
