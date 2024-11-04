using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NPC_", menuName = "NPC", order = 0)]
public class NPCBase : ScriptableObject
{
    [Header("Info")]
    public NPCType npcType;
    public string npcName;
    public float maxHealth;
    public float walkSpeed;
    public float runSpeed;
    public float fieldOfView;
    public GameObject npcPrefabs;
    //public ItemData[] dropOnDeath;

    [Header("AI")]
    public float detectDistance;
    public float safeDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")]
    public float damage;
    public float attackRate;
    public float attackDistance;
}
