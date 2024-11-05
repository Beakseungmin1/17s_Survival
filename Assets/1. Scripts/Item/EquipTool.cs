using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    [Header("build")]
    public bool doesItBuilding;

    [Header("Consume")]
    public bool doesItFood;
    public float increasedFoodValue;
    public float increasedWaterValue;

    private Animator animator;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            attacking = true;
            Invoke("OnCanAttack", attackRate);
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            // If you want to use it, create a Resource script for the lecture.
            //if (doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
            //{
            //    resource.Gather(hit.point, hit.normal);
            //}

            if (doesDealDamage && hit.collider.TryGetComponent(out NPC npc))
            {
                npc.TakePhysicalDamage(damage);
            }
        }

    }
}
