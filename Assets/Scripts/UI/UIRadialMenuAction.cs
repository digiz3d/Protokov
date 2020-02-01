using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRadialMenuAction : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas = default;

    private HandActionId[] possibleActions;
    private HandActionId selectedActionId;

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
        this.possibleActions = possibleActions;
        canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        canvas.gameObject.SetActive(false);
    }
}
