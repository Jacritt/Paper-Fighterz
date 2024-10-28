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
                    timeBtwAttack = startTimeBtwAttack;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    DownAttack();
                    timeBtwAttack = startTimeBtwAttack;
                }
                else
                {
                    NormalAttack();
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
                    timeBtwAttack = startTimeBtwAttack;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    DownAttack();
                    timeBtwAttack = startTimeBtwAttack;
                }
                else
                {
                    NormalAttack();
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
        }
        
    }


}
