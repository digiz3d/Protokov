using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryDraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas baseCanvas;

    GameObject copy;
    RectTransform rec;

    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log($"OnPointerDown {e}");
    }

    public void OnBeginDrag(PointerEventData e)
    {
        Debug.Log($"OnBeginDrag {e}");

        CanvasGroup group = GetComponent<CanvasGroup>();
        group.blocksRaycasts = false;


        copy = Instantiate(gameObject, transform.parent);
        copy.transform.SetParent(baseCanvas.transform);
        //Destroy(copy.GetComponent<UIInventoryDraggableItem>());
        rec = copy.GetComponent<RectTransform>();
        Canvas canvas = copy.GetComponent<Canvas>();
        rec.position = GetComponent<RectTransform>().position;
        canvas.overrideSorting = true;
        canvas.sortingOrder = 1;

        group.alpha = 0.8f;
    }

    public void OnEndDrag(PointerEventData e)
    {
        Debug.Log($"OnEndDrag {e}");
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.blocksRaycasts = true;
        group.alpha = 1f;
        Destroy(copy);
    }

    public void OnDrag(PointerEventData e)
    {
        Debug.Log($"OnDrag {e} {baseCanvas.scaleFactor}");
        copy.GetComponent<RectTransform>().anchoredPosition += e.delta / baseCanvas.scaleFactor;
    }

    public void OnDrop(PointerEventData e)
    {
        Debug.Log($"BAD OnDrop {e}");
    }
}
