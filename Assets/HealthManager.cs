using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100; // Maximum health
    private float currentHealth; // Current health
    public Slider healthBar; // Reference to UI Text for displaying health

    private PlayerMovement playerMovement;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        playerMovement = GetComponent<PlayerMovement>();
        UpdateHealthBar(); // Update the UI at the start
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.E) && playerMovement.isPlayer1){
            TakeDamage(20);
        }
        if(Input.GetKeyDown(KeyCode.R) && !playerMovement.isPlayer1){
            TakeDamage(20);
        }
    }



    // Method to take damage
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Decrease current health
        if (currentHealth <= 0) // Clamp to zero
        {
            currentHealth = 0;
            playerMovement.isDead = true;
            PlayerLose();
        }
        UpdateHealthBar(); // Update the health display
    }

    // Method to heal
    public void Heal(int healAmount)
    {
        currentHealth += healAmount; // Increase current health
        if (currentHealth > maxHealth) // Clamp to max health
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar(); // Update the health display
    }

    // Update the health text UI
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth/maxHealth;
        }
    }

    private void PlayerLose(){
        if (playerMovement.isPlayer1){
            print("Player 2 WINS!");
        }
        else{
            print("Player 1 WINS!");
        }
    }
}
