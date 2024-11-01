using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecorationSO_", menuName = "ItemBase/DecorationSO", order = 1)]
public class DecorationSO : ItemSO
{
	public GameObject previewItemPrefabs;
	public NeedItem[] needItems;
}