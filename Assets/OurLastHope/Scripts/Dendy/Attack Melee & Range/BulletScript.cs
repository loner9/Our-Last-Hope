using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 10f; // Properti damage untuk peluru
    [SerializeField] private GameObject explosionPrefab; // Prefab yang akan dimunculkan saat peluru hancur

    private void OnCollisionEnter(Collision collision)
    {
        // Logika untuk menghancurkan peluru dan memberikan damage
        if (collision.gameObject.TryGetComponent(out Health targetHealth))
        {
            targetHealth.TakeDamage(damage);
        }

        // Memunculkan prefab baru dan menambahkan debug
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Debug.Log("Explosion prefab instantiated at: " + transform.position + " with rotation: " + transform.rotation);
            // Hancurkan peluru ketika menabrak objek dengan collider
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Explosion prefab is not assigned in the Inspector!");
            // Hancurkan peluru ketika menabrak objek dengan collider
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
        // Hancurkan peluru setelah 2 detik
        Destroy(gameObject, 2f);
    }
}
