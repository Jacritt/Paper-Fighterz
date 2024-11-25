using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCharacter : MonoBehaviour
{
    // Movement Variables
    [Header("Movement")]
    public float moveSpeed = 5f;       // Speed of movement
    public float horizontalInput;      // Input for horizontal movement
    public bool isWalking = false;
    public float dashDuration = 0.5f;   // How long the dash lasts
    public float dashDistance = 2f;
    private bool isDashing = false;     // Whether the player is dashing 
    private Vector2 dashDirection;  // Direction of the dash
    private float lastTapTime; 
    private float tapDelay = 0.3f; 


    // Jumping Variables
    [Header("Jumping")]
    public float jumpForce = 15f;       // Force of the jump
    private bool isJumping = false;    // Whether the player is currently jumping
    public int jumps = 2;
    public bool isGrounded = false;    // Whether the player is on the ground
    public LayerMask groundLayer;      // Layer to check for the ground
    public float groundCheckRadius = 1f; // Radius of ground check


    // Walking & Attacking
    [Header("Attacking")]
    public float attackDamage = 10;
    public bool isAttacking = false;   // Whether the player is attacking
    public bool canAttack = true;      // Whether the player can attack
    public float attackTimer = 0f;     // Timer to track attack cooldown
    public CircleCollider2D hitbox;


    // Player Info
    [Header("Player Info")]
    public string characterName = "";  // Name of the character
    public bool isPlayer1;
    public string currentState = "";   // Current animation state
    public Transform otherPlayerTransform;
    public bool isDead = false;
    public bool isStunned = false;
    public Collider2D playerCollider;
    public float stunTime = 0.3f;

    // Components
    private Rigidbody2D rb;            // Rigidbody component for physics
    private Animator animator;         // Animator for character animations



    protected virtual void Start()
    {
        // Get the Rigidbody2D component for physics-based movement
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        groundLayer = LayerMask.GetMask("groundLayer");
        hitbox = GetComponentInChildren<CircleCollider2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isDead){ChangeAnimationState("DEAD");return;}
        if (isStunned){ChangeAnimationState("STUN");return;}

        if(gameObject.transform.position.x > 7){gameObject.transform.position = new Vector2(7,gameObject.transform.position.y);}
        if(gameObject.transform.position.x < -7){gameObject.transform.position = new Vector2(-7,gameObject.transform.position.y);}

        if(!isAttacking){HandleMovement();};
        HandleJumping();
        HandleAttacks();
        PlayerFaceEnemy();
        
        if (isGrounded){HandleDash();}
        

        HandleAnimationStates();

        // Update the attack timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime; // Decrease the timer each frame
        }
        else
        {
            // Stop attacking when the cooldown ends and there is no animation transition
            StopAttacking();
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }

    void HandleAnimationStates()
    {
        if (isAttacking)
        {
            canAttack = false; // Prevent further attacks during this animation
            return;
        }

        if (!isGrounded)
        {
            ChangeAnimationState("JUMP");
            return;
        }

        if (isWalking)
        {
            ChangeAnimationState("WALK");
            return;
        }

        ChangeAnimationState("IDLE");
    }

    private void HandleMovement()
    {
        if (isPlayer1){
            if (Input.GetKey(KeyCode.A)){
                horizontalInput = -1;
            }
            else if (Input.GetKey(KeyCode.D)){
                horizontalInput = 1;
            }
            else{
                horizontalInput = 0;
            }
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            isWalking = Mathf.Abs(horizontalInput) > 0 && isGrounded;
        }
        else{
            if (Input.GetKey(KeyCode.LeftArrow)){
                horizontalInput = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow)){
                horizontalInput = 1;
            }
            else{
                horizontalInput = 0;
            }
        }
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        isWalking = Mathf.Abs(horizontalInput) > 0 && isGrounded;
    
        
    }

    private void HandleJumping()
    {
        // Check if grounded using raycasting (check if the player is on the ground)
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, groundLayer);
        if (isGrounded && jumps < 2){
            jumps = 2;
        }

        // Jump if the W key is pressed and the character is grounded
        if (isPlayer1){
            if (Input.GetKeyDown(KeyCode.W) && jumps > 0)
            {
                jumps--;
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }else{
            if (Input.GetKeyDown(KeyCode.UpArrow) && jumps > 0)
            {
                jumps--;
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void HandleDash()
    {
        if(isPlayer1){

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
        playerCollider.gameObject.layer = LayerMask.NameToLayer("PlayerInvulnerable");
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
        playerCollider.gameObject.layer = LayerMask.NameToLayer("Default");
    }


    private void HandleAttacks()
    {
        // Don't allow attacks if the cooldown hasn't elapsed
        if (attackTimer > 0) return; // Wait for the cooldown

        if (isPlayer1){
            if (Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    // Up attack (W + Space)
                    UpAttack();
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    // Down attack (S + Space)
                    DownAttack();
                }
                else
                {
                    // Normal attack (Space)
                    NormalAttack();
                }
            }
        }
        else{
            if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.RightShift))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    // Up attack (W + Space)
                    UpAttack();
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    // Down attack (S + Space)
                    DownAttack();
                }
                else
                {
                    // Normal attack (Space)
                    NormalAttack();
                }
            }
        }
    }

    private void NormalAttack()
    {
        Debug.Log("Performing Normal Attack");
        ChangeAnimationState("N_ATTACK");
        isAttacking = true;
        // Set the attack cooldown based on the attack animation duration
        attackTimer = GetAttackAnimationDuration();
    }

    private void UpAttack()
    {
        Debug.Log("Performing Up Attack");
        ChangeAnimationState("UP_ATTACK");
        isAttacking = true;
        // Set the attack cooldown based on the attack animation duration
        attackTimer = GetAttackAnimationDuration();
    }

    private void DownAttack()
    {
        Debug.Log("Performing Down Attack");
        ChangeAnimationState("DOWN_ATTACK");
        isAttacking = true;
        // Set the attack cooldown based on the attack animation duration
        attackTimer = GetAttackAnimationDuration();
    }

    // Method to get the duration of the current attack animation
    private float GetAttackAnimationDuration()
    {
        
        return 0.5f; // Fallback cooldown if animation is not found
    }

    public void StopAttacking()
    {
        // Only stop attacking if we are not transitioning between animations
        if (!animator.IsInTransition(0)) 
        {
            isAttacking = false;
            canAttack = true;  // Allow attacks again after the cooldown
        }
    }


    private void PlayerFaceEnemy(){
            
        if (gameObject.tag == "Player1")
        {
            otherPlayerTransform = GameObject.FindWithTag("Player2").transform;
        }
        else
        {
            otherPlayerTransform = GameObject.FindWithTag("Player1").transform;
        }  


        // Check the x positions to determine if the player should face left or right
        if (otherPlayerTransform.position.x < transform.position.x)
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
        

     public void StunPlayer(){
        isStunned = true;
        rb.AddForce(new Vector2(200 * -transform.localScale.x, 0));
        Invoke("StopStunningPlayer", stunTime);
     }

     public void StopStunningPlayer(){
        isStunned = false;
     }

        
    
}