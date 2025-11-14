using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    private bool grounded;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        float speed = Mathf.Abs(rb.linearVelocity.x);
        float yVel = rb.linearVelocity.y;

        anim.SetFloat("Speed", speed);
        anim.SetBool("isGrounded", grounded);
        anim.SetFloat("yVelocity", yVel);
    }
}