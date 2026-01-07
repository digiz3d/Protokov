using UnityEngine;

[RequireComponent(typeof(PlayerInteraction))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerController : MonoBehaviour
{
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
    }

    void Update()
    {
        bool pressedUseKey = false;
        bool isPressingUseKey = false;
        bool releasedUseKey = Input.GetKeyUp(KeyCode.F);
        bool pressedInventoryKey = Input.GetKeyDown(KeyCode.I);

        if (ControlsEnabled)
        {
            pressedUseKey = Input.GetKeyDown(KeyCode.F);
            isPressingUseKey = Input.GetKey(KeyCode.F);
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
