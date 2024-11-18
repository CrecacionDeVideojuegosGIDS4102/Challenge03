using UnityEngine;

public class EnemigoMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontalInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        horizontalInput = Input.GetAxis("Horizontal");

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void UpdateAnimations()
    {
        if (isGrounded)
        {
            if (horizontalInput != 0)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isIdle", false);
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isIdle", true);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
            animator.SetTrigger("isJumping");
        }
    }
}
