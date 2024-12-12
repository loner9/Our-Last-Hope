using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BossEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool isChasing;
    private bool isAttacking = true;
    public Animator _animator;
    

    [Header("Player Detection")]
    public GameObject player;                  // Reference to the player's position
    public float detectionRadius = 10f;        // Detection
    public float attackRange = 2f;            // Distance at which the enemy can attack
    public float attackCooldown = 1.5f;       // Cooldown between attacks

    private float nextAttackTime;             // Time when the enemy can attack again

    [Header("Enemy Health")]
    public float maxHealth = 100f;            // Maximum health of the enemy
    public float currentHealth;               // Current health of the enemy
    private bool isDead = false; 
    public BossHealthBar _healthBar;
    
    [Header("Boss Phase System")]
    private bool isPhaseTwo = false;          // Determine if the boss is in phase two
    public float phaseTwoSpeedMultiplier = 1.5f; // Speed multiplier for phase two
    public float phaseTwoDamageMultiplier = 2f; // Damage multiplier for phase two
    private int currentPhaseOneCount = 0; // Counter serangan fase 1
    public int phaseOneAttacks = 3;   // Jumlah serangan fase 1 sebelum fase 2
    
    private void Awake()
    {
        _healthBar = GetComponent<BossHealthBar>();
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
        _animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        // Player detection condition
        if ((distanceToPlayer <= detectionRadius) && (isAttacking == false))
        {
            if(isDead) return;
            // Check if chasing
            if (!isDead && isAttacking==false)
            {
                isChasing = true;
                agent.SetDestination(player.transform.position);
            }
            
        }

        // Attack the player if within attack range and cooldown is complete
        if (distanceToPlayer <= attackRange)
        {
            isAttacking = true;
            isChasing = false;
            if (Time.time >= nextAttackTime)
            {
                DealDamage();
                
            }
        }
        if (distanceToPlayer >= attackRange+0.2f)
        {
            isAttacking = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletScript bullet = other.GetComponent<BulletScript>();
            if (bullet != null)
            {
                Debug.Log("Bullet damage: " + bullet.damage);
                TakeDamage(bullet.damage);
            }
            Destroy(other.gameObject);
        }

        if (other.CompareTag("melee"))
        {
            MeleeScript melee = other.GetComponent<MeleeScript>();
            if (melee != null)
            {
                TakeDamage(melee.damage);
            }
        }
    }

    public void DealDamage()
    {
        if (Time.time < nextAttackTime)
            return; // Tunggu hingga cooldown selesai

        // Arahkan musuh ke pemain
        transform.LookAt(player.transform.position);
        agent.SetDestination(transform.position);

        if (!isPhaseTwo)
        {
            // Attack Phase 1
            AttackPhase1();
        }
        else
        {
            // Phase 2 Logic
            if (currentPhaseOneCount < phaseOneAttacks)
            {
                // Serangan fase 1
                AttackPhase1();
                currentPhaseOneCount++;
            }
            else
            {
                // Serangan fase 2 setelah beberapa serangan fase 1
                AttackPhase2();
                currentPhaseOneCount = 0; // Reset counter ke 0
            }
        }
    }
    private void AttackPhase1()
    {
        agent.SetDestination(transform.position);
        _animator.SetTrigger("AttackPhase1");
        Debug.Log("Enemy performs Attack Phase 1!");
        nextAttackTime = Time.time + attackCooldown;
    }

    private void AttackPhase2()
    {
        agent.SetDestination(transform.position);
        _animator.SetTrigger("AttackPhase2");
        Debug.Log("Enemy performs Attack Phase 2!");
        nextAttackTime = Time.time + attackCooldown;
    }
    

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Damage Taken: " + damage + ". Health tersisa: " + currentHealth);
        _healthBar.updateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            if (!isPhaseTwo)
            {
                EnterPhaseTwo();
            }
            else
            {
                Die();
            }
        }
    }

    private void EnterPhaseTwo()
    {
        Debug.Log("Boss enters Phase Two!");
        isPhaseTwo = true;
        currentHealth = maxHealth; // Restore health
        _healthBar.updateHealthBar(currentHealth, maxHealth);

        // Increase speed and damage
        agent.speed *= phaseTwoSpeedMultiplier;
        attackCooldown /= phaseTwoDamageMultiplier; // Faster attacks
    }

    private void Die()
    {
        isChasing = false;
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
        transform.LookAt(gameObject.transform.rotation * Vector3.forward);
        isDead = true;
        _animator.SetTrigger("Dead");
        Debug.Log("Enemy is dead.");
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
