using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerActionsManager))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)]
    private float InteractionMaxDistance = 1.3f;

    public Camera cam;
    public Image actionCursor;
    private Transform camTransform;

    private LayerMask layerMask;
    private Interactible currentInteractibleObject = null;

    private PlayerController playerController;
    private PlayerActionsManager playerActionsManager;
    private UIRadialMenuAction radialMenuAction;
    
    void Start()
    {
        camTransform = cam.transform;
        layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        playerActionsManager = GetComponent<PlayerActionsManager>();
        playerController = GetComponent<PlayerController>();
        radialMenuAction = GetComponent<UIRadialMenuAction>();
    }

    void Update()
    {
        Debug.DrawRay(camTransform.position, camTransform.forward * InteractionMaxDistance, Color.green, 0f);

        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, InteractionMaxDistance, layerMask))
        {
            GameObject target = hit.collider.gameObject;
            Interactible interactibleObject = target.GetComponent<Interactible>();

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

    public void ShowMenu()
    {
        if (currentInteractibleObject != null)
        {
            playerController.ControlsEnabled = false;
            radialMenuAction.ShowActions(currentInteractibleObject.GetPossibleActions(gameObject));
        }
    }

    public void HideMenu()
    {
        radialMenuAction.Hide();
    }
}
