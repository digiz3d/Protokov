using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryDraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas baseCanvas;

    RectTransform rec;
    CanvasGroup group;
    Canvas canvas;
    Vector2 previousPosition;

    void Start()
    {
        rec = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        canvas = GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log($"OnPointerDown {e}");
    }

    public void OnBeginDrag(PointerEventData e)
    {
        Debug.Log($"OnBeginDrag {e}");
        group.blocksRaycasts = false;
        group.alpha = 0.8f;
        previousPosition = rec.anchoredPosition;
        canvas.overrideSorting = true;
        canvas.sortingOrder = 1;
    }

    public void OnEndDrag(PointerEventData e)
    {
        Debug.Log($"OnEndDrag {e}");
        group.blocksRaycasts = true;
        group.alpha = 1f;
        rec.anchoredPosition = previousPosition;
        canvas.overrideSorting = false;
    }

    public void OnDrag(PointerEventData e)
    {
        Debug.Log($"OnDrag {e}");
        rec.anchoredPosition += e.delta / baseCanvas.scaleFactor;
    }

    public void OnDrop(PointerEventData e)
    {
        Debug.Log($"BAD OnDrop {e}");
    }
}
