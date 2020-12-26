using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInventoryRenderer : MonoBehaviour
{
    public PlayerInventory inventory;
    public GameObject leftContentViewport;
    public GameObject rightContentViewport;

    public GameObject inventoryHumanPrefab;
    public GameObject inventoryNothingPrefab;

    public const int CELL_SIZE = 40;

    public bool isInvalidated = true;

    void Update()
    {
        if (isInvalidated)
        {
            isInvalidated = false;
            RenderInventory();
        }
    }

    public void Invalidate()
    {
        isInvalidated = true;
    }

    void RenderInventory()
    {
        RenderLeftColumn();
        RenderRightColumn();
    }

    void RenderLeftColumn()
    {
        ScrollRect rec = leftContentViewport.GetComponentInParent<ScrollRect>();
        Erase(leftContentViewport);
        GameObject go = Instantiate(inventoryHumanPrefab, leftContentViewport.transform);
        rec.content = go.GetComponent<RectTransform>();
        go.GetComponent<UIInventoryHuman>().Feed(inventory);
    }

    void RenderRightColumn()
    {
        ScrollRect rec = rightContentViewport.GetComponentInParent<ScrollRect>();
        Erase(rightContentViewport);
        GameObject go = Instantiate(inventoryNothingPrefab, rightContentViewport.transform);
        rec.content = go.GetComponent<RectTransform>();
    }

    void Erase(GameObject content)
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
