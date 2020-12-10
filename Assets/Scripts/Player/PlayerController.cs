using UnityEngine;

[RequireComponent(typeof(PlayerInteraction))]
public class PlayerController : MonoBehaviour
{
    public bool ControlsEnabled { get; set; } = true;

    private PlayerInteraction playerInteraction;

    private float timePressedUseKey = 0f;

    void Awake()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        bool pressedUseKey = false;
        bool stillPressingUseKey = false;
        bool releasedUseKey = Input.GetKeyUp(KeyCode.F);

        if (ControlsEnabled)
        {
            pressedUseKey = Input.GetKeyDown(KeyCode.F);
            stillPressingUseKey = Input.GetKey(KeyCode.F);
        }

        if (pressedUseKey)
        {
            timePressedUseKey = Time.unscaledTime;
        }
        else if (releasedUseKey && Time.unscaledTime - timePressedUseKey < 1f && playerInteraction.CanInteract())
        {
            playerInteraction.FastInteract();
        }
        else if (stillPressingUseKey && Time.unscaledTime - timePressedUseKey >= 1f && playerInteraction.CanInteract())
        {
            ControlsEnabled = false;
            playerInteraction.ShowMenu();
        }
        else if (releasedUseKey && Time.unscaledTime - timePressedUseKey >= 1f)
        {
            ControlsEnabled = true;
            playerInteraction.SlowInteract();
            playerInteraction.HideMenu();
        }
    }
}
