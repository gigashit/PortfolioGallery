using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10f;

    [Header("Gravity")]
    public float gravity = -9.81f;
    public float groundedOffset = -0.1f;

    [Header("References")]
    public Transform cameraTransform;
    [SerializeField] private Animator playerAnimator;

    private CharacterController controller;
    private InputSystem_Actions inputActions;

    private Vector2 moveInput;
    private bool isRunning;

    private float verticalVelocity;

    private Browser browserScript;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        browserScript = FindAnyObjectByType<Browser>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Sprint.performed += _ => isRunning = true;
        inputActions.Player.Sprint.canceled += _ => isRunning = false;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleAnimations();
    }

    private void HandleAnimations()
    {
        playerAnimator.SetFloat("Speed", controller.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    private void HandleMovement()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = groundedOffset;
        }

        if (!browserScript.isBrowserOn)
        {
            // Movement direction relative to camera
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;

            if (moveDirection.magnitude > 0.1f)
            {
                // Rotate character toward movement direction
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            float speed = isRunning ? runSpeed : walkSpeed;

            Vector3 horizontalVelocity = moveDirection * speed;

            // Gravity
            verticalVelocity += gravity * Time.deltaTime;

            Vector3 velocity = horizontalVelocity + Vector3.up * verticalVelocity;

            controller.Move(velocity * Time.deltaTime);
        }
    }
}