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

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip deathSFX;


    private Rigidbody2D rb;
    private Animator anim;

    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true;
    private bool isDead = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PerformJump();
        }

        UpdateAnimations();
        FlipSprite();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    public bool IsPlayerMoving()
    {
        return Mathf.Abs(horizontalInput) > 0.01f || !isGrounded;
    }


    void PerformJump()
    {
        if (isDead) return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);

        if (jumpSFX != null)
            audioSource.PlayOneShot(jumpSFX);
    }

    void UpdateAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isJumping", !isGrounded);
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

    // 🔥 SPIKE DETECTION (NEW)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        // Force exit jump/fall states
        anim.SetBool("isJumping", false);

        // Trigger Death animation
        anim.SetTrigger("Death");

        if (deathSFX != null)
            audioSource.PlayOneShot(deathSFX);

        // 🔥 SHOW PAUSE MENU
        PauseMenu pauseMenu = Object.FindFirstObjectByType<PauseMenu>();
        if (pauseMenu != null)
        {
            pauseMenu.ShowDeathMenu();
        }


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
