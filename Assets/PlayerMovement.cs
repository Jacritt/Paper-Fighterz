using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Speed of the player
    public float jumpForce = 5f;   // Jump force
    public float gravityScale = 5f; // Gravity scalar
    public int jumps = 2;           // Number of jumps allowed
    public float dashDistance = 5f; // Distance to dash
    public float dashDuration = 0.2f; // Duration of the dash
    private float tapDelay = 0.3f;  // Time window for double tap

    private Rigidbody2D rb;         // Reference to Rigidbody2D
    private bool isGrounded;        // Check if the player is on the ground
    private bool isDashing = false; // Flag to check if the player is dashing
    private float lastTapTime;      // Time of the last key press
    private Vector2 dashDirection;  // Direction of the dash

    
     

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing)
        {
            Move();
            Jump();
        }

        // Set the gravity scale
        rb.gravityScale = gravityScale;

        // Handle dashing
        HandleDash();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal"); // Get horizontal input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y); // Set new velocity
    }

    void Jump()
    {
        if (jumps > 0 && Input.GetKeyDown(KeyCode.W)) // Check if grounded and jump button pressed
        {
            jumps--;
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Apply jump force
        }
    }


    void HandleDash()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Check for double tap to the right
        if (Input.GetKeyDown(KeyCode.D) && Time.time - lastTapTime < tapDelay)
        {
            Dash(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastTapTime = Time.time;
        }

        // Check for double tap to the left
        if (Input.GetKeyDown(KeyCode.A) && Time.time - lastTapTime < tapDelay)
        {
            Dash(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastTapTime = Time.time;
        }
    }

    void Dash(Vector2 direction)
    {
        isDashing = true;
        dashDirection = direction;
        StartCoroutine(DashCoroutine());
    }

    private System.Collections.IEnumerator DashCoroutine()
    {
        float dashStartTime = Time.time;
        Vector2 originalPosition = rb.position; // Store the original position for accurate movement

        // Calculate how much to move each frame based on the dash distance and duration
        float dashEndTime = dashStartTime + dashDuration;

        while (Time.time < dashEndTime)
        {
            // Calculate the dash progress based on time
            float t = (Time.time - dashStartTime) / dashDuration;
            
            // Use Lerp to smoothly interpolate between original position and the target position
            Vector2 targetPosition = originalPosition + dashDirection * dashDistance;
            rb.MovePosition(Vector2.Lerp(originalPosition, targetPosition, t));

            yield return null; // Wait until the next frame
        }

        isDashing = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Check for ground collision
        {
            isGrounded = true; // Set isGrounded to true
            jumps = 2; // Reset jumps when grounded
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Check for leaving ground
        {
            isGrounded = false; // Set isGrounded to false
        }
    }
}
