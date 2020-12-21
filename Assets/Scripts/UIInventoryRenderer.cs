using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI = UnityEngine.UI;

public class UIInventoryRenderer : MonoBehaviour
{
    public PlayerInventory inventory;
    public GameObject leftContent;
    public GameObject middleContent;
    public GameObject rightContent;

    public const int CELL_SIZE = 50;

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
        RenderMiddleColumn();
        RenderRightColumn();
    }

    void RenderLeftColumn()
    {
        int y = -CELL_SIZE;
        Erase(leftContent);
        RenderSlot(0, y, CELL_SIZE * 2, CELL_SIZE * 2, leftContent.transform);
        y -= (CELL_SIZE * 3);
        RenderSlot(0, y, CELL_SIZE * 2, CELL_SIZE * 2, leftContent.transform);
    }

    void RenderMiddleColumn()
    {
        Erase(middleContent);
    }

    void RenderRightColumn()
    {
        Erase(rightContent);
    }

    void Erase(GameObject content)
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void RenderSlot(int x, int y, int width, int height, Transform parent)
    {
        GameObject go = new GameObject($"test ui y {y}");
        go.transform.SetParent(parent);
        RectTransform rec = go.AddComponent<RectTransform>();
        rec.anchoredPosition = new Vector2(0, 1);
        rec.localPosition = new Vector3(x, y, 0);
        rec.anchorMin = new Vector2(0, 1);
        rec.anchorMax = new Vector2(0, 1);
        rec.sizeDelta = new Vector2(width, height);
        rec.pivot = new Vector2(0, 1);
        rec.localScale = new Vector3(1, 1, 1);
        go.layer = LayerMask.NameToLayer("UI");
        go.AddComponent<Canvas>();
        go.AddComponent<GraphicRaycaster>();
    }
}
