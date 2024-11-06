﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public ItemSO testItem;


    [SerializeField] private GameObject _slotsParent;
    [SerializeField] private GameObject _extendUI;
    [HideInInspector] public Slot[] slots = new Slot[26];


    private void Awake()
    {
        slots = _slotsParent.GetComponentsInChildren<Slot>();
    }


    public void AcquireItem(ItemSO item, int count = 1)
    {
        if (item.itemType == ItemType.Equipment || item.itemType == ItemType.Weapon)
        {
            for (int i = 0; i < slots.Length - 6; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(item, count);
                    return;
                }
            }

            ThrowItem(item);
        }
        else
        {
            for (int i = 0; i < slots.Length - 6; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == item.itemName && slots[i].itemCount < item.maxStack)
                    {
                        slots[i].SetSlotCount(count);
                        return;
                    }
                }
            }

            for (int i = 0; i < slots.Length - 6; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(item, count);
                    return;
                }
            }

            ThrowItem(item);
        }
    }


    private void ThrowItem(ItemSO item)
    {
        // Instantiate(item, transform.position, quaternion.identity); // tf.positon -> playerPositon
    }
}