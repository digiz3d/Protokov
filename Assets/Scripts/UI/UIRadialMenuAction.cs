using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRadialMenuAction : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas = default;

    [SerializeField]
    private GameObject menuItemPrefab = default;

    private HandActionId[] possibleActions;

    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    void Update()
    {

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
            GameObject menuItem = Instantiate(menuItemPrefab, canvas.gameObject.transform);
            float rotation = (360 / possibleActions.Length) * i;
            menuItem.transform.Rotate(0, 0, rotation);
            Transform childTextTransform = menuItem.transform.GetChild(0);
            childTextTransform.Rotate(0, 0, -rotation);
            childTextTransform.GetComponent<TextMeshProUGUI>().text = i.ToString();
            i++;
        }

        this.possibleActions = possibleActions;

        canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        canvas.gameObject.SetActive(false);
        foreach (Transform child in canvas.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
