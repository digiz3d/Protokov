using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public PlayerInventory inventory;
    public GameObject leftContentViewport;
    public GameObject rightContentViewport;

    public GameObject inventoryHumanPrefab;
    public GameObject inventoryNothingPrefab;

    public Canvas baseCanvas;

    public const int CELL_SIZE = 40;

    public bool isInvalidated = true;

    public static InventoryItem currentlyDraggingItem = null;

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
        UIInventoryHuman uiInventoryHuman = go.GetComponent<UIInventoryHuman>();
        uiInventoryHuman.Setup(baseCanvas);
        uiInventoryHuman.Render(inventory);
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
