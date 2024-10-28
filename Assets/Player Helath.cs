using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelath : MonoBehaviour
{
    public static event Action<PlayerHelath> OnEnemyKilled; 
    [SerializeField] float health, maxHealth = 3f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float damageamount)
    {
        health -= damageamount;

        if (health <= 0) {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }
}
