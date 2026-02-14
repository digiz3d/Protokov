using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInteraction))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerController : MonoBehaviour
{
    InputAction interactionInputAction;
    InputAction inventoryInputAction;

    public bool ControlsEnabled { get; set; } = true;

    private PlayerInteraction playerInteraction;
    private PlayerInventory playerInventory;

    private float timePressedUseKey = 0f;
    private bool isInventoryOpen = false;

    void Awake()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
        playerInventory = GetComponent<PlayerInventory>();
        playerInventory.HideInventory();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        interactionInputAction = InputSystem.actions.FindAction("Interact");
        inventoryInputAction = InputSystem.actions.FindAction("Inventory");
    }

    void Update()
    {
        bool pressedUseKey = false;
        bool isPressingUseKey = false;
        bool releasedUseKey = interactionInputAction.WasReleasedThisFrame();
        bool pressedInventoryKey = inventoryInputAction.WasPressedThisFrame();

        if (ControlsEnabled)
        {
            pressedUseKey = interactionInputAction.WasPressedThisFrame();
            isPressingUseKey = interactionInputAction.IsPressed();
        }

        // interact
        if (pressedUseKey)
        {
            timePressedUseKey = Time.unscaledTime;
        }
        else if (releasedUseKey && Time.unscaledTime - timePressedUseKey < 1f && playerInteraction.CanInteract())
        {
            playerInteraction.FastInteract();
        }
        else if (isPressingUseKey && Time.unscaledTime - timePressedUseKey >= 1f && playerInteraction.CanInteract())
        {
            ControlsEnabled = false;
            playerInteraction.ShowRadialMenu();
        }
        else if (releasedUseKey && Time.unscaledTime - timePressedUseKey >= 1f)
        {
            Debug.Log("Slow interact over");

            ControlsEnabled = true;
            playerInteraction.SlowInteract();
            playerInteraction.HideRadialMenu();
        }

        // inventory
        if (pressedInventoryKey)
        {
            if (isInventoryOpen)
            {
                playerInventory.HideInventory();
                isInventoryOpen = false;
                ControlsEnabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                playerInventory.ShowInventory();
                isInventoryOpen = true;
                ControlsEnabled = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

    }
}
