using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;

    private int jumpsLeft;    

    public InputAction MoveAction;
    public InputAction JumpAction;

    private bool isFacingRight = true;
    private bool isWalking;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canJump;

    public float movementSpeed = 10.0f;
    public float jumpforce = 16.0f;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlidingSpeed;

    public int jumpAmount = 2;

    public Transform groundCheck;
    public Transform wallCheck;

    public LayerMask whatIsGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        MoveAction.Enable();
        JumpAction.Enable();

        jumpsLeft = jumpAmount;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && player.linearVelocity.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && player.linearVelocity.x > 0)
        {
            Flip();
        }

        if (player.linearVelocity.x != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("yVelocity", player.linearVelocity.y);
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && player.linearVelocity.y <= 0)
        {
            jumpsLeft = jumpAmount;
        }

        if (jumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }

        if (JumpAction.WasPressedThisFrame())
        {
            Jump();
        }
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && player.linearVelocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void ApplyMovement()
    {
        Vector2 playerMovement = MoveAction.ReadValue<Vector2>();
        player.linearVelocity = new Vector2(movementSpeed * playerMovement.x, player.linearVelocity.y);

        if (isWallSliding)
        {
            if (player.linearVelocity.y < -wallSlidingSpeed)
            {
                player.linearVelocity = new Vector2(player.linearVelocity.x, wallSlidingSpeed);
            }
        }
    }

    private void CheckSurroundings()
    {
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void Jump()
    {
        if (canJump)
        {
            player.linearVelocity = new Vector2(player.linearVelocity.x, jumpforce);
            jumpsLeft -= 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }
}
