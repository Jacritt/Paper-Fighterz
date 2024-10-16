using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the bullet prefab in the inspector
    public Transform firePoint;     // Where the bullet should be instantiated
    
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) // Change to your preferred input
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.right = firePoint.right; // Make the bullet face the same direction as the player
        }
    }
}
