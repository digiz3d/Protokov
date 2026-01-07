using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)]
    private float InteractionMaxDistance = 1.3f;

    [SerializeField]
    public Image actionCursor;

    [SerializeField]
    private Transform pointer;

    [SerializeField]
    private UIRadialMenuAction radialMenuAction;

    private LayerMask layerMask;
    private Interactible currentInteractibleObject = null;

    void Start()
    {
        layerMask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    void Update()
    {
        if (Physics.Raycast(pointer.position, pointer.forward, out RaycastHit hit, InteractionMaxDistance, layerMask))
        {
            GameObject target = hit.collider.gameObject;
            Interactible interactibleObject = target.GetComponent<Interactible>();

            if (interactibleObject == null)
            {
                interactibleObject = target.GetComponentInParent<Interactible>();
            }

            if (interactibleObject != null)
            {
                actionCursor.gameObject.SetActive(true);
                currentInteractibleObject = interactibleObject;
                return;
            }
        }
        currentInteractibleObject = null;
        actionCursor.gameObject.SetActive(false);
    }

    public bool CanInteract() => currentInteractibleObject != null;

    public void FastInteract()
    {
        if (currentInteractibleObject != null)
        {
            currentInteractibleObject.TriggeredBy(gameObject);
        }
    }

    public void SlowInteract()
    {
        if (currentInteractibleObject != null)
        {
            currentInteractibleObject.TriggeredBy(radialMenuAction.GetSelectedAction(), gameObject);
        }
    }

    public void ShowRadialMenu()
    {
        if (currentInteractibleObject != null)
        {
            radialMenuAction.ShowActions(currentInteractibleObject.GetPossibleActions(gameObject));
        }
    }

    public void HideRadialMenu()
    {
        radialMenuAction.Hide();
    }
}
