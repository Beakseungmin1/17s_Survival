using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;


[System.Serializable]
public class Slots
{
    public ItemSO item = null;
    public int itemCount = 0;
}


public class SlotModel : MonoBehaviour
{
    [Header("Constant Inventory")]
    public Slots[] constantSlots = null;

    [Header("Extend Top Inventory")]
    public Slots[] extendTopSlots = null;

    [Header("Extend Bottom Inventory")]
    [Tooltip("BottomSlots is Copy Constant Inventory")]
    public Slots[] extendBottomSlots = null;

    [Space(10)]
    [Header("Slot Default Image")]
    public Sprite slotDefaultImage;
}