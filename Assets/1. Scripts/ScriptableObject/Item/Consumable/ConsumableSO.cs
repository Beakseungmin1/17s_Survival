using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ConsumableSO_", menuName = "ItemBase/ConsumableSO", order = 4)]
public class ConsumableSO : ItemSO
{
	public ConsumableType consumableType;
	public int value;
}
