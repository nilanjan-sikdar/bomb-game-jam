using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpPower = 12f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;

    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool isDead = false; // New flag to stop movement
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return; // 2. IGNORE INPUT if dead
        // Input
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PerformJump();
        }

        // Animation + Flip
        UpdateAnimations();
        FlipSprite();
    }

    void FixedUpdate()
    {
        if (isDead) return; // 1. STOP MOVING if dead
        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Movement
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void PerformJump()
    {
        if (isDead) return; // 3. IGNORE JUMP if dead
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
    }

    void UpdateAnimations()
    {
        // Run / Idle
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Jumping state (TRUE when NOT grounded)
        anim.SetBool("isJumping", !isGrounded);

        // Vertical velocity (useful for jump/fall blend trees)
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f ||
            !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }

    public void Die()
    {
        if (isDead) return; // Prevent dying twice
        isDead = true;

        Debug.Log("Player Died!");

        // Stop all physics movement immediately
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0; // Optional: Stop falling

        // Play the Animation
        if (anim != null) anim.SetTrigger("Death");

        // Destroy player after 1 second (adjust time to match your animation length)
        Destroy(gameObject, 1.3f);
    }
        void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
}
