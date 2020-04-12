using UnityEngine;
using TMPro;

public class UIRadialMenuAction : MonoBehaviour
{
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

        cursorMenu.transform.Translate(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0, Space.Self);
    }

    public HandActionId GetSelectedAction()
    {
        return HandActionId.Cancel;
    }

    public void ShowActions(HandActionId[] possibleActions)
    {
        int i = 0;

        foreach (HandActionId actionId in possibleActions)
        {
            GameObject menuItem = Instantiate(menuItemPrefab, canvas.transform);
            float rotation = (360 / possibleActions.Length) * i;
            menuItem.transform.Rotate(0, 0, rotation);
            Transform childTextTransform = menuItem.transform.GetChild(0);
            childTextTransform.Rotate(0, 0, -rotation);
            childTextTransform.GetComponent<TextMeshProUGUI>().text = i.ToString();
            i++;
        }

        this.possibleActions = possibleActions;
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
