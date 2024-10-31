using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CraftingTableModel : MonoBehaviour
{
    [Header("건축물")]
    public BuildingItem[] buildingItems = null;

    [Header("장식물")]
    public DecorationItem[] decorationItems = null;

    [Header("인벤토리")] // after delete
    public Slot[] slots = null;
}