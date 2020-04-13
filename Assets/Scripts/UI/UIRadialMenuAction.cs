using UnityEngine;
using TMPro;

public class UIRadialMenuAction : MonoBehaviour
{
    [SerializeField]
    private float cancelUntilDistance = 50f;

    [SerializeField]
    private float mouseSensitivity = 1f;

    private Canvas canvas = default;

    [SerializeField]
    private GameObject menuItemPrefab = default;

    [SerializeField]
    private GameObject cursorMenuPrefab = default;

    private GameObject cursorMenu;

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

    public HandActionId GetSelectedAction()
    {
        if (cursorMenu.transform.localPosition.magnitude < cancelUntilDistance)
        {
            return HandActionId.Cancel;
        }

        int itemsCount = possibleActions.Length;
        float cursorAngle = GetClockwiseAngle();
        int selectedItemIndex = Mathf.FloorToInt(cursorAngle / 360f * (possibleActions.Length));
        Debug.Log("selectedItemIndex=" + selectedItemIndex);
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
    }

    public void Hide()
    {
        cursorMenu.SetActive(false);
        canvas.gameObject.SetActive(false);
        foreach (Transform child in canvas.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
