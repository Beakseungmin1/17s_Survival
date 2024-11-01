using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyNPC_", menuName = "NPCBase/EnemyNPC", order = 0)]
public class EnemyNPC : NPCBase
{
    [Header("Combat")]
    public float damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;
}
