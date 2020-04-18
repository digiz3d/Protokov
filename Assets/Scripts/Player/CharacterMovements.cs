using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerController))]
public class CharacterMovements : MonoBehaviour
{
    [SerializeField]
    private Transform orientationTransform;

    [SerializeField, Range(0f, 20f)]
    private float mouseSensitivity = 5f;

    [SerializeField]
    private float moveForce = 100000f;

    [SerializeField]
    private float maxSpeed = 5f;

    [SerializeField]
    private bool isGrounded = false;

    private float forwardInput = 0f;
    private float sideInput = 0f;

    private bool jumpInput = false;
    private bool crouchInput = false;
    private bool sprintInput = false;

    private Rigidbody rb;
    private PlayerController playerController;
    private float xRotation = 0f;
    private float mouseX;
    private float mouseY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void Update()
    {
        HandleInputs();
        RotateCharacter();
    }

    private void MoveCharacter()
    {
        if (!playerController.ControlsEnabled) return;

        if (rb.velocity.magnitude > maxSpeed) return;

        rb.AddForce(orientationTransform.forward * forwardInput * Time.deltaTime * moveForce);
        rb.AddForce(orientationTransform.right * sideInput * Time.deltaTime * (moveForce / 2));
    }

    private void HandleInputs()
    {
        forwardInput = Input.GetAxisRaw("Vertical");
        sideInput = Input.GetAxisRaw("Horizontal");

        jumpInput = Input.GetKey(KeyCode.Space);
        crouchInput = Input.GetKey(KeyCode.LeftControl);
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
    }

    private void RotateCharacter()
    {
        if (!playerController.ControlsEnabled) return;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        playerController.cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        orientationTransform.localRotation = Quaternion.Euler(0, orientationTransform.localRotation.eulerAngles.y + mouseX, 0);
    }
}
