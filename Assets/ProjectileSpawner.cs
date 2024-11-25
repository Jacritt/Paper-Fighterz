using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{  // Reference to the projectile prefab
    public GameObject projectilePrefab;
    public Transform spawnPoint;
    public float moveSpeed = 1f;       // Speed at which the projectile moves
    public float lifeTime = 1f;
    public float bulletDamage = 10;

    // Call this method to spawn a projectile
    public void SpawnProjectile()
    {
        // Instantiate the projectile at the spawner's position and rotation
        GameObject newProjectile = Instantiate(projectilePrefab, spawnPoint.position, transform.rotation);
        float xDirection = gameObject.GetComponent<BaseCharacter>().transform.localScale.x;
        newProjectile.GetComponent<Rigidbody2D>().velocity = transform.right * moveSpeed * xDirection * Time.deltaTime ;
        newProjectile.GetComponent<Projectile>().damage = bulletDamage;
        Destroy(newProjectile, lifeTime);
        //transform.Translate(transform.up * moveSpeed * Time.deltaTime);
    }
    
}
