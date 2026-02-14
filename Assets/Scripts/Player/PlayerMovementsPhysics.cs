using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMovementsPhysics : MonoBehaviour
{

    InputAction jumpAction;
    InputAction crouchAction;
    InputAction sprintAction;
    InputAction lookAction;
    InputAction moveAction;




    [SerializeField]
    private float moveForce = 100000f;

    [SerializeField]
    private float maxSpeed = 5f;

    [SerializeField]
    private bool isGrounded = false;

    [SerializeField]
    private float groundDistanceCheck = 1.8f;

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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        jumpAction = InputSystem.actions.FindAction("Jump");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        lookAction = InputSystem.actions.FindAction("Look");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void FixedUpdate()
    {
        CheckGround();
        MoveCharacter();
    }

    private void Update()
    {
        HandleInputs();
        RotateCharacter();
    }

    private void CheckGround()
    {
        if (Physics.Raycast(gameObject.transform.position, -gameObject.transform.up, out RaycastHit hitInfo, groundDistanceCheck, ~LayerMask.GetMask("Player")))
            isGrounded = true;
        else
            isGrounded = false;
    }

    private void MoveCharacter()
    {
        if (!playerController.ControlsEnabled) return;

        if (rb.linearVelocity.magnitude > maxSpeed) return;

        if (isGrounded)
        {
            rb.AddForce(transform.forward * forwardInput * Time.deltaTime * moveForce);
            rb.AddForce(transform.right * sideInput * Time.deltaTime * (moveForce / 2));
            if (jumpInput)
                rb.AddForce(transform.up * gameObject.GetComponent<Rigidbody>().mass, ForceMode.Impulse);
        }
    }

    private void HandleInputs()
    {
        forwardInput = moveAction.ReadValue<Vector2>().y;
        sideInput = moveAction.ReadValue<Vector2>().x;

        jumpInput = jumpAction.IsPressed();
        crouchInput = crouchAction.IsPressed();
        sprintInput = sprintAction.IsPressed();

        Vector2 look = lookAction.ReadValue<Vector2>();
        mouseX = look.x;
        mouseY = look.y;
    }

    private void RotateCharacter()
    {
        if (!playerController.ControlsEnabled) return;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //playerController.cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + mouseX, 0);
    }
}
