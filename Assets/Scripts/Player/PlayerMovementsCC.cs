using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerController))]
public class PlayerMovementsCC : MonoBehaviour
{
    const float walkingSpeed = 5f;

    Camera cam;
    PlayerController playerController;
    CharacterController cc;
    CollisionFlags characterCollisionFlag;

    Vector2 mouseInput;
    Vector2 movementInput;

    float xRotation = 0;
    Vector3 desiredMove;
    Vector3 moveDir;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        HandleInput();
        PlayerRotation();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void HandleInput()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");
        mouseInput.x = x;
        mouseInput.y = y;

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        movementInput = new Vector2(horizontal, vertical);
        if (movementInput.sqrMagnitude > 1)
        {
            movementInput.Normalize();
        }
    }

    void PlayerRotation()
    {
        if (!playerController.ControlsEnabled) return;

        xRotation -= mouseInput.y;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + mouseInput.x, 0);
    }

    void PlayerMovement()
    {
        desiredMove = playerController.ControlsEnabled ? transform.forward * movementInput.y + transform.right * movementInput.x : Vector3.zero;

        Physics.SphereCast(transform.position, cc.radius, Vector3.down, out RaycastHit hitInfo, cc.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        Debug.DrawLine(transform.position, transform.position + desiredMove, Color.yellow);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
        Debug.DrawLine(transform.position, transform.position + desiredMove * 0.8f, Color.cyan);

        moveDir.x = desiredMove.x * walkingSpeed;
        moveDir.z = desiredMove.z * walkingSpeed;

        if (cc.isGrounded)
        {
            //moveDir.y = 10f; // stick to ground here if needed
        }
        else
        {
            moveDir += Physics.gravity * Time.fixedDeltaTime;
        }

        cc.Move(moveDir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        //dont move the rigidbody if the character is on top of it
        if (characterCollisionFlag == CollisionFlags.Below)
        {
            return;
        }

        if (rb == null || rb.isKinematic)
        {
            return;
        }
        rb.AddForceAtPosition(cc.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }

}
