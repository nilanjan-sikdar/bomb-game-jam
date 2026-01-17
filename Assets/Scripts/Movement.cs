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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
