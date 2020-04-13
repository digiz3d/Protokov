using UnityEngine;
using TMPro;

public class UIRadialMenuAction : MonoBehaviour
{
    [SerializeField]
    private float centerRadius = 50f;

    [SerializeField]
    private float mouseSensitivity = 1f;

    [SerializeField]
    private GameObject menuItemPrefab = default;

    [SerializeField]
    private GameObject cursorMenuPrefab = default;

    private Canvas canvas = default;
    private GameObject cursorMenu;
    private int previouslyHighlightedItemIndex = -1;
    private HandActionId[] possibleActions;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        cursorMenu = Instantiate(cursorMenuPrefab, transform.parent.transform);
        canvas.gameObject.SetActive(false);
        cursorMenu.SetActive(false);
    }

    void Update()
    {
        if (!canvas.isActiveAndEnabled) return;

        cursorMenu.transform.Translate(new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0) * mouseSensitivity, Space.Self);
        cursorMenu.transform.localPosition = Vector3.ClampMagnitude(cursorMenu.transform.localPosition, 100);

        HighlightHoveredItem();
    }

    private float GetClockwiseAngle()
    {
        float angle;

        if (cursorMenu.transform.localPosition.x < 0)
            angle = 180 + Vector3.Angle(Vector3.down, cursorMenu.transform.localPosition);
        else
            angle = Vector3.Angle(Vector3.up, cursorMenu.transform.localPosition);

        return angle;
    }

    public void HighlightHoveredItem()
    {
        int selectedItemIndex = GetCurrentSelectedIndex();
        if (selectedItemIndex == previouslyHighlightedItemIndex) return;

        if (previouslyHighlightedItemIndex != -1)
            canvas.transform.GetChild(previouslyHighlightedItemIndex).GetComponentInChildren<TextMeshProUGUI>().color =
                menuItemPrefab.GetComponentInChildren<TextMeshProUGUI>().color;

        if (selectedItemIndex != -1)
            canvas.transform.GetChild(selectedItemIndex).GetComponentInChildren<TextMeshProUGUI>().color = Color.red;

        previouslyHighlightedItemIndex = selectedItemIndex;
    }

    public int GetCurrentSelectedIndex()
    {
        if (cursorMenu.transform.localPosition.magnitude < centerRadius)
        {
            return -1;
        }
        float cursorAngle = GetClockwiseAngle();
        return Mathf.FloorToInt(cursorAngle / 360f * (possibleActions.Length)); ;
    }

    public HandActionId GetSelectedAction()
    {

        int selectedItemIndex = GetCurrentSelectedIndex();
        if (selectedItemIndex == -1) return HandActionId.Cancel;
        return possibleActions[selectedItemIndex];
    }

    public void ShowActions(HandActionId[] possibleActions)
    {
        int i = 0;

        foreach (HandActionId actionId in possibleActions)
        {
            GameObject menuItem = Instantiate(menuItemPrefab, canvas.transform);
            float itemAngle = (360 / possibleActions.Length);
            float clockwiseAngle = (itemAngle * -i) - (itemAngle / 2);
            menuItem.transform.Rotate(0, 0, clockwiseAngle);
            Transform childTextTransform = menuItem.transform.GetChild(0);
            childTextTransform.Rotate(0, 0, -clockwiseAngle);
            childTextTransform.GetComponent<TextMeshProUGUI>().text = i.ToString();
            i++;
        }

        this.possibleActions = possibleActions;
        cursorMenu.transform.localPosition = Vector3.zero;
        cursorMenu.SetActive(true);
        canvas.gameObject.SetActive(true);
        canvas.gameObject.transform.localScale = Vector3.zero;
        LeanTween.scale(canvas.gameObject, new Vector3(1, 1, 1), 0.1f).setEaseLinear();
    }

    public void Hide()
    {
        LeanTween.scale(canvas.gameObject, Vector3.zero, 0.1f).setEaseLinear().setOnComplete(DestroyMe);
    }

    private void DestroyMe()
    {
        cursorMenu.SetActive(false);
        canvas.gameObject.SetActive(false);
        foreach (Transform child in canvas.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
