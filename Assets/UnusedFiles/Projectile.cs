using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;        // Rigidbody2D reference
    public float damage;

    private void Awake()
    {
        // Get the Rigidbody2D component on this object
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.GetComponent<HealthManager>()){
            other.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        Destroy(gameObject);
        }
    }

}

