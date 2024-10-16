using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f; // Speed of the bullet
    public float lifetime = 2f; // How long the bullet lasts before being destroyed

    private void Start()
    {
        // Destroy the bullet after a certain lifetime
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Add collision logic here (e.g., damage the player, hit enemies, etc.)
        if (other.CompareTag("Enemy"))
        {
            // Handle enemy hit
            Destroy(other.gameObject); // Example: destroy enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}

