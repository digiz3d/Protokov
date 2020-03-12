using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    float gap = (5f / 360f);

    int i = 0;

    foreach (HandActionId actionId in possibleActions)
    {
      GameObject menuItem = Instantiate(menuItemPrefab, canvas.gameObject.transform);
      menuItem.GetComponent<Image>().fillAmount = (1f / possibleActions.Length) - gap;
      menuItem.transform.Rotate(0, 0, (360 / possibleActions.Length) * i);
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
