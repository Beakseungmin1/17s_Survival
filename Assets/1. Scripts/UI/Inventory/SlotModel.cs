using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Slot
{
    public ItemBase item = null;
    public int itemCount = 0;
}


public class SlotModel : MonoBehaviour
{
    private SlotPresenter _slotPresenter;

    private void Awake()
    {
        _slotPresenter = GetComponent<SlotPresenter>();
    }

    public Slot[] constantSlots = null;
    public Slot[] extendSlots = null;
}