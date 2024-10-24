using System.Data.Common;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public bool isPlayer1 = true;
    public float moveSpeed = 5f;   // Speed of the player
    public float jumpForce = 5f;   // Jump force
    public float gravityScale = 5f; // Gravity scalar
    public int jumps = 2;           // Number of jumps allowed
    public float dashDistance = 5f; // Distance to dash
    public float dashDuration = 0.2f; // Duration of the dash
    private float tapDelay = 0.3f;  // Time window for double tap

    public Rigidbody2D rb;         // Reference to Rigidbody2D
    public bool isWalking = false;
    public bool isJumping = false;
    private bool isGrounded;        // Check if the player is on the ground
    public bool isDashing = false; // Flag to check if the player is dashing
    public bool isStunned = false;
    private float lastTapTime;      // Time of the last key press
    private Vector2 dashDirection;  // Direction of the dash

    public Animator animator;
    public Transform otherPlayer;

    
     

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = gravityScale;
    }

    void Update()
    {
        PlayerFaceEnemy();
        if (!isDashing && !isStunned){
            Move();
            Jump();
        }

        // Handle dashing
        if(!isJumping && !isStunned){
            HandleDash();
        }

        
        animator.SetBool("isStunned", isStunned);
        
    }

    void Move()
    {
        if (isPlayer1){
            if (Input.GetKey(KeyCode.D)){
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Set new velocity
                isWalking = true;
                
            } 
            else if (Input.GetKey(KeyCode.A)){
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // Set new velocity
                isWalking = true;
                
            }
            else{
                isWalking = false;
                
            }

        }
        else{
            if (Input.GetKey(KeyCode.RightArrow)){
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y); // Set new velocity
                isWalking = true;
            } 
            else if (Input.GetKey(KeyCode.LeftArrow)){
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y); // Set new velocity
                isWalking = true;
            }
            else{
                isWalking = false;
            }
        }

        animator.SetBool("isWalking", isWalking);
    
    }

    void Jump()
    {
        if (isPlayer1){
            if (jumps > 0 && Input.GetKeyDown(KeyCode.W)) // Check if grounded and jump button pressed
            {
                jumps--;
                rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Apply jump force
                //isJumping = true;
            }
        }
        else{
            if (jumps > 0 && Input.GetKeyDown(KeyCode.UpArrow)) // Check if grounded and jump button pressed
            {
                jumps--;
                rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Apply jump force
                //isJumping = true;
            }
        }
    }



    void HandleDash()
    {
        //float moveInput = Input.GetAxisRaw("Horizontal");
        
        // Check for double tap to the right
        if (isPlayer1){
            if (Input.GetKeyDown(KeyCode.D) && Time.time - lastTapTime < tapDelay && !isDashing)
            {
                Dash(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                lastTapTime = Time.time;
            }

            // Check for double tap to the left
            if (Input.GetKeyDown(KeyCode.A) && Time.time - lastTapTime < tapDelay && !isDashing)
            {
                Dash(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                lastTapTime = Time.time;
            }
        } 
        else{
            if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time - lastTapTime < tapDelay && !isDashing)
            {
                Dash(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                lastTapTime = Time.time;
            }

            // Check for double tap to the left
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time - lastTapTime < tapDelay && !isDashing)
            {
                Dash(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                lastTapTime = Time.time;
            }
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

    private void PlayerFaceEnemy(){
        
        if (otherPlayer != null)
        {
            // Check the x positions to determine if the player should face left or right
            if (otherPlayer.position.x < transform.position.x)
            {
                // Other player is to the left, flip the player to face left
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // Other player is to the right, face right
                if (transform.localScale != new Vector3(1, 1, 1)){
                    transform.localScale = new Vector3(1, 1, 1);
                }
                
            }
        }

        else{
            if (isPlayer1){
                otherPlayer = GameObject.FindWithTag("Player2").transform;
            }
            else{
                otherPlayer = GameObject.FindWithTag("Player1").transform;
            }
        }
    
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Check for ground collision
        {
            isGrounded = true; // Set isGrounded to true
            jumps = 2; // Reset jumps when grounded
            isJumping = false;
        }

        animator.SetBool("isInAir", isJumping);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Check for leaving ground
        {
            isGrounded = false; // Set isGrounded to false
            isJumping = true;
        }
        animator.SetBool("isInAir", isJumping);
    }
}
