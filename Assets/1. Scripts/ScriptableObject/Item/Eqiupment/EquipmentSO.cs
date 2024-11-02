using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EquipmentSO_", menuName = "ItemBase/EquipmentSO", order = 5)]
public class EquipmentSO : ItemSO
{
    public EquipmentType equipmentType;
    public int IncreaseHpStat;
    public int IncreaseStaminaStat;
}
