using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;


[System.Serializable]
public class Slot
{
    public ItemSO item = null;
    public int itemCount = 0;
}


public class SlotModel : MonoBehaviour
{
    [Header("Constant Inventory")]
    public Slot[] constantSlots = null;

    [Header("Extend Top Inventory")]
    public Slot[] extendTopSlots = null;

    [Header("Extend Bottom Inventory")]
    [Tooltip("BottomSlots is Copy Constant Inventory")]
    public Slot[] extendBottomSlots = null;

    [Space(10)]
    [Header("Slot Default Image")]
    public Sprite slotDefaultImage;
}