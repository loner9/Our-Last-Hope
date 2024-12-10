using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public float idleTime = 3f;
    public float patrolRadius = 10f;

    private bool isPatrolling = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        StartCoroutine(StartPatrol());
    }

    IEnumerator StartPatrol()
    {
        while (true)
        {
            if (!isPatrolling)
            {
                SetMoveSpeed(0f);
                yield return new WaitForSeconds(idleTime);

                Vector3 randomPoint = GetRandomPoint();
                    agent.SetDestination(randomPoint);

                isPatrolling = true;
                SetMoveSpeed(1f);
            }

            if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                isPatrolling = false;
            }

            yield return null;
        }
    }

    Vector3 GetRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas);

        return hit.position;
    }

    void SetMoveSpeed(float speed)
    {
        animator.SetFloat("moveSpeed", speed);
    }
}
