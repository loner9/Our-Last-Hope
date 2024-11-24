using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isChasing;
    private bool isAttacking = true;
    public Animator _animator;
    
    [Header("Player Detection")]
    public GameObject player;                  // Reference to the player's position
    public float detectionRadius = 10f;        // Detection
    public float stopChaseRange = 15f;        // Distance at which the enemy stops chasing
    public float attackRange = 2f;            // Distance at which the enemy can attack
    public float attackCooldown = 1.5f;       // Cooldown between attacks

    private float nextAttackTime;             // Time when the enemy can attack again

    [Header("Enemy Health")]
    public float maxHealth = 100f;            // Maximum health of the enemy
    public float currentHealth;               // Current health of the enemy

    [Header("Patrolling")]
    public float _range;
    public Transform centrePoint; // Centre of the area the agent wants to move around in
    [SerializeField] private EnemyHealthBar _healthBar;

    private void Awake()
    {
        _healthBar = GetComponentInChildren<EnemyHealthBar>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        currentHealth = maxHealth;            // Set initial health to maxHealth
        _healthBar.updateHealthBar(currentHealth, maxHealth);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        _animator.SetFloat("Speed",agent.velocity.magnitude / agent.speed);
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        
        // Player detection condition
        if ((distanceToPlayer <= detectionRadius) && (isAttacking==false))
        {
            // If the player is within detection range, start chasing
            isChasing = true;
        }
        else if (distanceToPlayer >= stopChaseRange)
        {
            // If the player is out of stopChaseRange, stop chasing
            isChasing = false;
        }
        
        // Attack the player if within attack range and cooldown is complete
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            isAttacking = true;
            isChasing = false;
            DealDamage();
        }
        if (distanceToPlayer >= attackRange)
        {
            isAttacking = false;
        }
        Chasing();
    }

    // Function to handle bullet collision
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            BulletScript bullet = other.GetComponent<BulletScript>(); // Get the Bullet script
            if (bullet != null)
            {
                Debug.Log("Bullet damage: " + bullet.damage);
                TakeDamage(bullet.damage); // Reduce health by bullet damage
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("melee")){
            MeleeScript melee = other.GetComponent<MeleeScript>();
            if (melee != null){
                TakeDamage(melee.damage);
            }
        }
    }

    // Function to chase the player
    public void Chasing()
    {
        // Check if chasing
        if ((isChasing==true) && (isAttacking==false))
        {
            agent.SetDestination(player.transform.position); // Enemy chases the player
        }
        if ((isChasing==false)&&(isAttacking==false))
        {
            // Random patrol when not chasing the player
            if (agent.remainingDistance <= agent.stoppingDistance) // Done with path
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, _range, out point)) // Pass in the centre point and radius of the area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); // Visualization with gizmos
                    agent.SetDestination(point);
                }
            }
        }//stop and attack
        if ((isChasing==false)&&(isAttacking == true))
        {
            // Face the player and attack
            transform.LookAt(player.transform.position);
            agent.SetDestination(gameObject.transform.position);
        }
    }

    // Function to deal damage to the player
    public void DealDamage()
    {
        // Simulate attack here 
        _animator.SetTrigger("Attack");
        Debug.Log("Enemy attacks the player!");
        
        // Set the next attack time to current time + cooldown
        nextAttackTime = Time.time + attackCooldown;
    }

    // Function to receive damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce enemy's health
        Debug.Log("Damage Taken: " + damage + ". Health tersisa: " + currentHealth);
        _healthBar.updateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to handle enemy death
    private void Die()
    {
        // Optionally, add death effects or sounds here
        Destroy(gameObject); // Destroy the enemy object
    }

    // Function to patrol randomly within a given range
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; // Random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) // Documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        // Mengatur warna Gizmo menjadi merah
        Gizmos.color = Color.red;
        // Menggambar lingkaran yang menunjukkan jangkauan deteksi
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Mengatur warna Gizmo menjadi biru untuk jarak berhenti mengejar
        Gizmos.color = Color.blue;
        // Menggambar lingkaran yang menunjukkan jarak berhenti mengejar
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);
    }
}
