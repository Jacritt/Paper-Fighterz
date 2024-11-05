using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    
    public Animator animator;
    private PlayerMovement playerMovement;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public CapsuleCollider2D[] attackHitboxes;

    void Start() {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        
    }

    void Update(){
        if (!playerMovement.isDead){
            if (timeBtwAttack <= 0){
                HandleAttack();
                
            }else{
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    private void LaunchAttack(Collider2D col)
    {
        // Cast the collider to a CapsuleCollider2D if possible
        CapsuleCollider2D capsuleCol = col as CapsuleCollider2D;
        if (capsuleCol != null)
        {
            // Define capsule size and orientation
            Vector2 size = new Vector2(capsuleCol.size.x, capsuleCol.size.y);
            CapsuleDirection2D direction = capsuleCol.direction;

            // Use OverlapCapsule for 2D to find colliders overlapping the capsule
            Collider2D[] cols = Physics2D.OverlapCapsuleAll(capsuleCol.bounds.center, size, direction, 0, LayerMask.GetMask("Hitbox"));
            foreach (Collider2D c in cols)
            {
                if (c.transform.parent == transform)
                    continue;
                print(c);

                float damage = 5;
                c.SendMessageUpwards("TakeDamage", damage);
                print("damaged");
            }
        }
        else
        {
            Debug.LogWarning("Collider is not a CapsuleCollider2D.");
        }
    }

    public void NormalAttack()
    {
        print("Normal Attack!");
        animator.SetTrigger("NormalAttack");
    }

    public void UpAttack(){
        print("Up Attack!");
        animator.SetTrigger("UpAttack");
    }

    public void DownAttack(){
        print("Down Attack!");
        animator.SetTrigger("DownAttack");
    }

    void HandleAttack(){
        if (playerMovement.isPlayer1){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    UpAttack();
                    LaunchAttack(attackHitboxes[2]);
                    timeBtwAttack = startTimeBtwAttack;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    DownAttack();
                    LaunchAttack(attackHitboxes[1]);
                    timeBtwAttack = startTimeBtwAttack;
                }
                else
                {
                    NormalAttack();
                    LaunchAttack(attackHitboxes[0]);
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
        }
        else{
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    UpAttack();
                    LaunchAttack(attackHitboxes[2]);
                    timeBtwAttack = startTimeBtwAttack;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    DownAttack();
                    LaunchAttack(attackHitboxes[1]);
                    timeBtwAttack = startTimeBtwAttack;
                }
                else
                {
                    NormalAttack();
                    LaunchAttack(attackHitboxes[0]);
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
        }
        
    }
    

}
