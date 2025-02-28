using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody player;
    private Vector2 moveInput;
    private bool isDashing = false;
    private bool canDash = true;
    private int jumpCount = 0;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Transform cameraTransform;
    private Vector3 dashDirection;

    void Awake()
    {
        player = GetComponent<Rigidbody>();
        player.freezeRotation = true;
        cameraTransform = Camera.main.transform; 
    }

    void Start()
    {
        InputManager inputManager = FindAnyObjectByType<InputManager>();
        if (inputManager)
        {
            inputManager.OnMove.AddListener(HandleMove);
            inputManager.OnJump.AddListener(Jump);
            inputManager.OnDash.AddListener(Dash);
        }
    }

    void Update()
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0)
            {
                canDash = true;
            }
        }
        if (IsGrounded() && jumpCount > 0)
        {
            jumpCount = 0;
        }
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
        }
    }

    void HandleMove(Vector2 input)
    {
        moveInput = input;
    }

    void Move()
    {
        if (moveInput.magnitude > 0.1f)
        {
            Vector3 moveDirection = GetCameraRelativeDirection(moveInput);
            player.linearVelocity = new Vector3(moveDirection.x * moveSpeed, player.linearVelocity.y, moveDirection.z * moveSpeed);
            transform.forward = moveDirection;
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            jumpCount = 0;
        }

        if (jumpCount < 2)
        {
            player.linearVelocity = new Vector3(player.linearVelocity.x, jumpForce, player.linearVelocity.z);
            jumpCount++;
        }
    }

    void Dash()
    {
        if (!isDashing && canDash)
        {
            isDashing = true;
            canDash = false;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
            dashDirection = GetCameraRelativeDirection(moveInput);
            player.linearVelocity = dashDirection * dashSpeed;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f) && player.linearVelocity.y <= 0;
    }

    Vector3 GetCameraRelativeDirection(Vector2 input)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        return (right * input.x + forward * input.y).normalized;
    }
}
