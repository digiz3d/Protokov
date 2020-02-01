using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerActionsManager))]
public class PlayerInteraction : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)]
    private float InteractionMaxDistance = 1.3f;

    public Camera cam;
    public Image img;
    private Transform camTransform;

    private LayerMask layerMask;
    private Interactible currentInteractibleObject = null;

    private PlayerActionsManager playerActionsManager;

    void Start()
    {
        camTransform = cam.transform;
        layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        playerActionsManager = GetComponent<PlayerActionsManager>();
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
                img.gameObject.SetActive(true);
                currentInteractibleObject = interactibleObject;
                return;
            }
        }
        currentInteractibleObject = null;
        img.gameObject.SetActive(false);
    }

    public void TryInteract()
    {
        if (currentInteractibleObject != null)
        {
            currentInteractibleObject.TriggeredBy(gameObject);
        }
    }
}
