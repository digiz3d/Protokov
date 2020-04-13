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

    private void MoveCharacter()
    {
        if (rb.velocity.magnitude > maxSpeed) return;

        // apply input forces
        Debug.Log("forward input = " + forwardInput + ", sideInput=" + sideInput);
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
