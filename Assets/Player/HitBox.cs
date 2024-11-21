using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        print("Hit" + other.name);
        if (other.GetComponent<HealthManager>() != null){
            float damage = gameObject.GetComponentInParent<BaseCharacter>().attackDamage;
            other.GetComponent<HealthManager>().TakeDamage(damage);
        }
    }
}
