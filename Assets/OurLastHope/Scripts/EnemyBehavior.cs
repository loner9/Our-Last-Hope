using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Player Detection")]
    public Transform player;                  // Referensi ke posisi pemain
    public float detectionRange = 10f;        // Jarak deteksi musuh terhadap pemain
    public float stopChaseRange = 15f;        // Jarak di mana musuh akan berhenti mengejar

    [Header("Enemy Health")]
    public float maxHealth = 100f;            // Health maksimal musuh
    public float currentHealth;              // Health saat ini

    private NavMeshAgent agent;
    private bool isChasing;

    [SerializeField] private EnemyHealthBar _healthBar;

    private void Awake()
    {
        _healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Mengambil komponen NavMeshAgent
        currentHealth = maxHealth;            // Set health awal sesuai maxHealth
        _healthBar.updateHealthBar(currentHealth,maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0) 
        {
            Die();
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Jika pemain berada dalam jarak deteksi, musuh mulai mengejar
            isChasing = true;
        }
        else if (distanceToPlayer >= stopChaseRange)
        {
            // Jika pemain menjauh dari jarak berhenti mengejar, musuh berhenti mengejar
            isChasing = false;
            agent.SetDestination(transform.position);  // Musuh berhenti bergerak
        }

        if (isChasing)
        {
            agent.SetDestination(player.position);    // Musuh mengejar pemain
        }
    }
    // Fungsi untuk mendeteksi peluru yang masuk
    private void OnTriggerEnter(Collider other)
    {
        // Mengecek jika collider yang masuk memiliki tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            BulletScript bullet = other.GetComponent<BulletScript>(); // Mendapatkan skrip Bullet
            if (bullet != null)
            {
                TakeDamage(bullet.damage); // Mengurangi health dengan damage dari peluru
            }
        }
    }

    // Fungsi untuk menerima damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;                      // Mengurangi health musuh
        _healthBar.updateHealthBar(currentHealth,maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Fungsi untuk mematikan musuh
    private void Die()
    {
        // Misalnya menambahkan efek atau suara kematian di sini
        Destroy(gameObject);                           // Menghancurkan objek musuh
    }
}
