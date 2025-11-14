using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 1f;
    public float jumpBufferTime = 0.15f;
    public float jumpCutMultiplier = 0.5f; // controls how quickly jump cuts off when you release early

    [Header("Checks")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    private Rigidbody2D rb;
    private float moveInput;
    private bool isFacingRight = true;
    private bool isGrounded;
    private float coyoteCounter;
    private float jumpBufferCounter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- INPUT ---
        moveInput = Input.GetAxisRaw("Horizontal");

        // --- JUMP BUFFER ---
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // --- GROUND CHECK ---
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

       

        // --- JUMP EXECUTION ---
        if (jumpBufferCounter > 0)
        {
            Jump();
            jumpBufferCounter = 0;
        }

        

        // --- FLIP SPRITE ---
        Flip();
    }

    void FixedUpdate()
    {
        // --- HORIZONTAL MOVEMENT ---
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void Flip()
    {
        if ((isFacingRight && moveInput < 0) || (!isFacingRight && moveInput > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}