using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, IDamagalbe
{
    public NPCBase npcInfo;

    public float curHealth;
    private float lastAttackTime;
    private float distanceFromPlayer;

    private AIState aiState;

    private NavMeshAgent agent;
    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        curHealth = npcInfo.maxHealth;
    }

    void Start()
    {
        SetState(AIState.Wandering);
    }

    void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        animator.SetBool("Moving", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
            case AIState.Fleeing:
                FleeingUpdate();
                break;
        }
    }

    private void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = npcInfo.walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = npcInfo.walkSpeed;
                agent.isStopped = false;
                break;
            case AIState.Attacking:
                agent.speed = npcInfo.runSpeed;
                agent.isStopped = false;
                break;
            case AIState.Fleeing:
                agent.speed = npcInfo.runSpeed;
                agent.isStopped = false;
                break;
        }

        animator.speed = agent.speed / npcInfo.walkSpeed;
    }

    void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(npcInfo.minWanderWaitTime, npcInfo.maxWanderWaitTime));
        }

        if (distanceFromPlayer < npcInfo.detectDistance)
        {
            if (npcInfo.npcType == NPCType.Enemy)
            {
                SetState(AIState.Attacking);
            }
            else
            {
                SetState(AIState.Fleeing);
            }
        }
    }

    void AttackingUpdate()
    {
        if (distanceFromPlayer < npcInfo.attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > npcInfo.attackRate)
            {
                lastAttackTime = Time.time;
                CharacterManager.Instance.Player.controller.GetComponent<IDamagalbe>().TakePhysicalDamage(npcInfo.damage);
                animator.speed = 1;
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            if (distanceFromPlayer < npcInfo.detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
                {
                    agent.SetDestination(CharacterManager.Instance.Player.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Wandering);
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(AIState.Wandering);
            }
        }
    }

    void FleeingUpdate()
    {
        if (aiState == AIState.Fleeing && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("FleeingToNewLocation", npcInfo.minWanderWaitTime);
        }
    }

    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle)
        {
            return;
        }
        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(npcInfo.minWanderDistance, npcInfo.maxWanderDistance)), out hit, npcInfo.maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < npcInfo.detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(npcInfo.minWanderDistance, npcInfo.maxWanderDistance)), out hit, npcInfo.maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    void FleeingToNewLocation()
    {
        if (aiState != AIState.Idle)
        {
            return;
        }
        SetState(AIState.Fleeing);
        agent.SetDestination(GetFleeingLocation());
    }

    Vector3 GetFleeingLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * npcInfo.maxWanderDistance), out hit, npcInfo.maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < npcInfo.detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * npcInfo.maxWanderDistance), out hit, npcInfo.maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < npcInfo.fieldOfView * 0.5;
    }

    public void TakePhysicalDamage(float damage)
    {
        Debug.Log("hit!");
        curHealth -= damage;
        AudioManager.Instance.PlaySFX(EnemySFX.Hit);

        if (curHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        for (int i = 0; i < npcInfo.dropOnDeath.Length; i++)
        {
            Instantiate(npcInfo.dropOnDeath[i].dropPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
