using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryDraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    Canvas baseCanvas;
    InventoryItem representedItem;
    GameObject copy;

    public void Setup(Canvas _baseCanvas, InventoryItem _representedItem)
    {
        baseCanvas = _baseCanvas;
        representedItem = _representedItem;
        RawImage rawImage = GetComponent<RawImage>();
        rawImage.texture = representedItem.Thumbnail;
    }

    public void OnPointerDown(PointerEventData e)
    {
        //Debug.Log($"OnPointerDown {e}");
    }

    public void OnBeginDrag(PointerEventData e)
    {
        //Debug.Log($"OnBeginDrag {e}");

        CanvasGroup group = GetComponent<CanvasGroup>();
        group.blocksRaycasts = false;

        copy = Instantiate(gameObject, transform.parent);
        copy.transform.SetParent(baseCanvas.transform);
        Canvas copyCanvas = copy.GetComponent<Canvas>();
        copyCanvas.overrideSorting = true;
        copyCanvas.sortingOrder = 1;
        group.alpha = 0.8f;

        UIInventory.currentlyDraggingItem = representedItem;
    }

    public void OnEndDrag(PointerEventData e)
    {
        UIInventory.currentlyDraggingItem = null;

        Debug.Log($"OnEndDrag {e}");
        CanvasGroup group = GetComponent<CanvasGroup>();
        group.blocksRaycasts = true;
        group.alpha = 1f;
        Destroy(copy);
    }

    public void OnDrag(PointerEventData e)
    {
        copy.GetComponent<RectTransform>().anchoredPosition += e.delta / baseCanvas.scaleFactor;
    }

    public void OnDrop(PointerEventData e)
    {
        Debug.Log($"Dropped on item {e}");
    }
}
