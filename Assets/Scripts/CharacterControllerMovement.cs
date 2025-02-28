/*
A lot of this code was inspired by Rytech's YouTube video "SMOOTH FIRST PERSON MOVEMENT in Unity"
Other parts were inspired by previous labs guides, previous revisions of this code, and various other online videos/threads.
*/
using UnityEngine;
using Unity.Cinemachine;

public class CharacterControllerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 currentMoveVelocity;
    private Vector3 moveDampVelocity;
    private Vector3 currentForceVelocity;

    [SerializeField] private float gravityStrength = 9.81f;
    [SerializeField] private float jumpStrength = 5f;
    [SerializeField] private float walkSpeed = 7.5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 1f;

    private float moveSmoothTime = 0.05f;
    private int jumpCount = 0;

    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 dashDirection;

    private Vector2 movementInput;
    private bool jumpPressed = false;
    private bool dashPressed = false;

    private Transform cameraTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InputManager inputManager = FindFirstObjectByType<InputManager>();

        if (inputManager != null)
        {
            inputManager.OnMove.AddListener(HandleMove);
            inputManager.OnJump.AddListener(HandleJump);
            inputManager.OnDash.AddListener(HandleDash);
        }
        else
        {
            Debug.LogError("InputManager not found in the scene!");
        }

        CinemachineVirtualCameraBase cinemachineCamera = FindFirstObjectByType<CinemachineVirtualCameraBase>();

        if (cinemachineCamera != null)
        {
            cameraTransform = cinemachineCamera.transform;
        }
        else
        {
            Debug.LogError("CharacterControllerMovement: Cinemachine Camera not found!");
        }
    }

    void Update()
    {
        if (cameraTransform == null)
        {
            Debug.LogError("CharacterControllerMovement: Missing camera reference!");
            return;
        }

        /* ----- Move 1/2 ----------------------------------------------------------------------------------*/
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 moveVector = (cameraForward * movementInput.y + cameraRight * movementInput.x).normalized;
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        /* ----- Dash --------------------------------------------------------------------------------------*/
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
        if (dashPressed && dashCooldownTimer <= 0f && !isDashing)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            dashCooldownTimer = dashCooldown;
            dashDirection = moveVector;
            dashPressed = false;
        }
        if (isDashing)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            dashTimeLeft -= Time.deltaTime;

            if (dashTimeLeft <= 0f)
            {
                isDashing = false;
            }
        }
        /* ----- Move 2/2 ----------------------------------------------------------------------------------*/
        else
        {
            currentMoveVelocity = Vector3.SmoothDamp(
                currentMoveVelocity,
                moveVector * currentSpeed,
                ref moveDampVelocity,
                moveSmoothTime
            );

            controller.Move(currentMoveVelocity * Time.deltaTime);
        }

        /* ----- Jump --------------------------------------------------------------------------------------*/
        if (controller.isGrounded)
        {
            if (currentForceVelocity.y < 0)
            {
                currentForceVelocity.y = -2f;
                jumpCount = 0;
            }
        }
        if (jumpPressed && jumpCount < 2)
        {
            currentForceVelocity.y = jumpStrength;
            jumpCount++;
            jumpPressed = false;
        }

        currentForceVelocity.y -= gravityStrength * Time.deltaTime;
        controller.Move(currentForceVelocity * Time.deltaTime);
    }

    /* ----- InputManager Event Listeners -------------------------------------------------------------------*/
    private void HandleMove(Vector2 input)
    {
        movementInput = input;
    }

    private void HandleJump()
    {
        jumpPressed = true;
    }

    private void HandleDash()
    {
        dashPressed = true;
    }
}
