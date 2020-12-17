using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryDraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas baseCanvas;
    RectTransform rec;
    CanvasGroup group;

    void Start()
    {
        rec = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log($"OnPointerDown {e}");
    }

    public void OnBeginDrag(PointerEventData e)
    {
        Debug.Log($"OnBeginDrag {e}");
        group.blocksRaycasts = false;
        group.alpha = 0.5f;
    }

    public void OnEndDrag(PointerEventData e)
    {
        Debug.Log($"OnEndDrag {e}");
        group.blocksRaycasts = true;
        group.alpha = 1f;
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
