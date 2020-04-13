using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovements : MonoBehaviour
{
    [SerializeField]
    private Transform orientationTransform;

    [SerializeField]
    private Transform cameraTransform;

    private float xRotation = 0f;

    [SerializeField, Range(0f, 20f)]
    private float mouseSensitivity = 10f;

    [SerializeField]
    private float moveForce = 4500f;

    [SerializeField]
    private float maxSpeed = 20f;

    [SerializeField]
    private bool isGrounded = false;

    private float sideInput = 0f;
    private float forwardInput = 0f;

    private bool jumpInput = false;
    private bool crouchInput = false;
    private bool sprintInput = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    private void MoveCharacter() { }

    private void HandleInputs()
    {
        forwardInput = Input.GetAxisRaw("Vertical");
        sideInput = Input.GetAxisRaw("Horizontal");

        jumpInput = Input.GetKey(KeyCode.Space);
        crouchInput = Input.GetKey(KeyCode.LeftControl);
        sprintInput = Input.GetKey(KeyCode.LeftShift);
    }

    private void RotateCharacter()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        orientationTransform.localRotation = Quaternion.Euler(0, orientationTransform.localRotation.eulerAngles.y + mouseX, 0);
    }
}
