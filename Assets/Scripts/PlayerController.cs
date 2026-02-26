using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed = 8f;
    private float jumpHeight = 2f;

    private CharacterController controller;
    private Vector3 moveInput;
    private Vector3 velocity;
    private float gravity = -9.8f;
    private bool hasDoubleJump;

    [Header("Raycasting")]
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && (controller.isGrounded || hasDoubleJump))
        {
            if (hasDoubleJump) hasDoubleJump = false;
            if (controller.isGrounded) hasDoubleJump = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void Update()
    {
        // Move code
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(transform.rotation * move * moveSpeed * Time.deltaTime);

        // Gravity
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}