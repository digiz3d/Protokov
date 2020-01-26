using UnityEngine;

[RequireComponent(typeof(PlayerInteraction))]
public class PlayerController : MonoBehaviour
{


    [SerializeField]
    private float acceleration = 6f;
    [SerializeField]
    private float deceleration = 6f;
    [SerializeField]
    private float maxSpeed = 3f;
    [SerializeField]
    private float sprintMultiplicator = 2f;
    [SerializeField]
    private float currentForwardSpeed = 0f;
    [SerializeField]
    private float currentRightSpeed = 0f;
    [SerializeField]
    public Camera cam;

    private float XClamp;
    private CharacterController characterController;
    private float yVelocity = 0f;

    public bool ControlsEnabled { get; set; } = true;

    private PlayerInteraction playerInteraction;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        bool pressingForward = false;
        bool pressingBackward = false;
        bool pressingLeft = false;
        bool pressingRight = false;
        bool pressingSprint = false;
        bool pressedUseKey = false;
        float mouseX = 0f;
        float mouseY = 0f;

        if (ControlsEnabled)
        {
            pressingForward = Input.GetKey(KeyCode.Z);
            pressingBackward = Input.GetKey(KeyCode.S);
            pressingLeft = Input.GetKey(KeyCode.Q);
            pressingRight = Input.GetKey(KeyCode.D);
            pressingSprint = Input.GetKey(KeyCode.LeftShift);
            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");
            pressedUseKey = Input.GetKeyDown(KeyCode.F);
        }

        if (pressedUseKey)
        {
            playerInteraction.TryInteract();
        }

        if (pressingForward && !pressingBackward)
        {
            if (currentForwardSpeed < 0f) currentForwardSpeed += deceleration * Time.deltaTime;
            currentForwardSpeed += acceleration * Time.deltaTime;
            currentForwardSpeed = Mathf.Min(currentForwardSpeed, pressingSprint ? maxSpeed * sprintMultiplicator : maxSpeed);
        }
        else if (!pressingForward && pressingBackward)
        {
            if (currentForwardSpeed > 0f) currentForwardSpeed -= deceleration * Time.deltaTime;
            currentForwardSpeed -= acceleration * Time.deltaTime;
            currentForwardSpeed = Mathf.Max(currentForwardSpeed, -maxSpeed);
        }
        else
        {
            if (currentForwardSpeed > 0f)
            {
                currentForwardSpeed -= deceleration * Time.deltaTime;
                currentForwardSpeed = Mathf.Max(currentForwardSpeed, 0f);
            }
            else if (currentForwardSpeed < 0f)
            {
                currentForwardSpeed += deceleration * Time.deltaTime;
                currentForwardSpeed = Mathf.Min(currentForwardSpeed, 0f);
            }
        }

        if (pressingRight && !pressingLeft)
        {
            if (currentRightSpeed < 0f) currentRightSpeed += deceleration * Time.deltaTime;
            currentRightSpeed += acceleration * Time.deltaTime;
            currentRightSpeed = Mathf.Min(currentRightSpeed, maxSpeed);
        }
        else if (!pressingRight && pressingLeft)
        {
            if (currentRightSpeed > 0f) currentRightSpeed -= deceleration * Time.deltaTime;
            currentRightSpeed -= acceleration * Time.deltaTime;
            currentRightSpeed = Mathf.Max(currentRightSpeed, -maxSpeed);
        }
        else
        {
            if (currentRightSpeed > 0f)
            {
                currentRightSpeed -= deceleration * Time.deltaTime;
                currentRightSpeed = Mathf.Max(currentRightSpeed, 0f);
            }
            else if (currentRightSpeed < 0f)
            {
                currentRightSpeed += deceleration * Time.deltaTime;
                currentRightSpeed = Mathf.Min(currentRightSpeed, 0f);
            }
        }

        if (!characterController.isGrounded)
        {
            yVelocity += Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            yVelocity = 0f;
        }


        transform.Rotate(0f, mouseX, 0f);

        characterController.Move(transform.TransformVector(new Vector3(currentRightSpeed * Time.deltaTime, yVelocity * Time.deltaTime, currentForwardSpeed * Time.deltaTime)));

        // transform camera
        XClamp -= mouseY;
        XClamp = Mathf.Clamp(XClamp, -80.0f, 80.0f);
        cam.transform.localRotation = Quaternion.Euler(XClamp, 0, 0);
    }
}
