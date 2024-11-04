using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, IDamagalbe
{
    public NPCBase npc;

    private AIState aiState;

    public float curHealth;
    private float lastAttackTime;
    private float playerDistance;

    private NavMeshAgent agent;
    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        curHealth = npc.maxHealth;
    }

    void Start()
    {
        SetState(AIState.Wandering);
    }

    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        animator.SetBool("Moving", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Idle:
                PassiveUpdate();
                break;
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
                agent.speed = npc.walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = npc.walkSpeed;
                agent.isStopped = false;
                break;
            case AIState.Attacking:
                agent.speed = npc.runSpeed;
                agent.isStopped = false;
                break;
            case AIState.Fleeing:
                agent.speed = npc.runSpeed;
                agent.isStopped = false;
                break;
        }

        animator.speed = agent.speed / npc.walkSpeed;
    }

    void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(npc.minWanderWaitTime, npc.maxWanderWaitTime));
        }

        if (playerDistance < npc.detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    void AttackingUpdate()
    {
        if(playerDistance < npc.safeDistance)
        {
            if (playerDistance > npc.attackDistance || !IsPlayerInFieldOfView())
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(CharacterManager.Instance.Player.transform.position, path))
                {
                    agent.SetDestination(CharacterManager.Instance.Player.transform.position);
                }
                else
                {
                    SetState(AIState.Fleeing);
                }
            }
            else
            {
                agent.isStopped = true;
                if (Time.time - lastAttackTime > npc.attackRate)
                {
                    lastAttackTime = Time.time;
                    CharacterManager.Instance.Player.controller.GetComponent<IDamagalbe>().TakePhysicalDamage(npc.damage);
                    animator.speed = 1;
                    animator.SetTrigger("Attack");
                }
            }
        }
        else
        {
            SetState(AIState.Wandering);
        }
    }

    void FleeingUpdate()
    {
        if (agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(GetFleeLocation());
        }
        else
        {
            SetState(AIState.Wandering);
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

    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < npc.fieldOfView * 0.5f;
    }

    Vector3 GetFleeLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * npc.safeDistance), out hit, npc.maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < npc.safeDistance)
        {

            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * npc.safeDistance), out hit, npc.maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(npc.minWanderDistance, npc.maxWanderDistance)), out hit, npc.maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < npc.detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(npc.minWanderDistance, npc.maxWanderDistance)), out hit, npc.maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - CharacterManager.Instance.Player.transform.position, transform.position + targetPos);
    }

    public void TakePhysicalDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
            Die();

        StartCoroutine(DamageFlash());
    }

    void Die()
    {
        //for (int x = 0; x < npcBase.dropOnDeath.Length; x++)
        //{
        //    Instantiate(npcBase.dropOnDeath[x].dropPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        //}

        Destroy(gameObject);
    }

    IEnumerator DamageFlash()
    {
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = new Color(1.0f, 0.6f, 0.6f);

        yield return new WaitForSeconds(0.1f);
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = Color.white;
    }
}
