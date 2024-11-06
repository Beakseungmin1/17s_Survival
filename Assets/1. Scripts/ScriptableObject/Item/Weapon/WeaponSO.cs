using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponSO_", menuName = "ItemBase/WeaponSO", order = 3)]
public class WeaponSO : ItemSO
{
    public float Damage;
    public float AttackDistance;

    [Header("Tool")]
    public float ResourceDamage;
}
