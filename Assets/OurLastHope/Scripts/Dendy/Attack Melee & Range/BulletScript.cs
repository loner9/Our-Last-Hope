using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 10f; // Properti damage untuk peluru
    

    private void OnCollisionEnter(Collision collision)
    {
        // Logika untuk menghancurkan peluru dan memberikan damage
        if (collision.gameObject.TryGetComponent(out Health targetHealth))
        {
            targetHealth.TakeDamage(damage);
        }

        // Memunculkan prefab baru dan menambahkan debug
        
        
            
            
            
            
        
        
        
            
            
            Destroy(gameObject);
        

        
    }

    private void Start()
    {
        // Hancurkan peluru setelah 2 detik
        Destroy(gameObject, 2f);
    }
}
