using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NPC", menuName = "NPCBase/", order = 0)]
public class NPCBase : ScriptableObject
{
    [Header("Info")]
    public NPCType npcType;
    public string npcName;
    public float health;
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
}
