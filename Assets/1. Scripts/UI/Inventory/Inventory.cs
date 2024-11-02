using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public ItemSO testItem;
    public static bool isInventoryOpend = false;

    [SerializeField] private GameObject _slotsParent;
    [SerializeField] private GameObject _extendUI;
    [HideInInspector] public Slot[] _slots = new Slot[26];


    private void Awake()
    {
        _slots = _slotsParent.GetComponentsInChildren<Slot>();
    }

    private void Start()
    {
        _extendUI.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AcquireItem(testItem, 1);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpend = !isInventoryOpend;

            if (isInventoryOpend)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }


    public void AcquireItem(ItemSO item, int count = 1)
    {
        if (item.itemType == ItemType.Equipment || item.itemType == ItemType.Weapon)
        {
            for (int i = 0; i < _slots.Length - 6; i++)
            {
                if (_slots[i].item == null)
                {
                    _slots[i].AddItem(item, count);
                    return;
                }
            }

            ThrowItem(item);
        }
        else
        {
            for (int i = 0; i < _slots.Length - 6; i++)
            {
                if (_slots[i].item != null)
                {
                    if (_slots[i].item.itemName == item.itemName && _slots[i].itemCount < item.maxStack)
                    {
                        _slots[i].SetSlotCount(count);
                        return;
                    }
                }
            }

            for (int i = 0; i < _slots.Length - 6; i++)
            {
                if (_slots[i].item == null)
                {
                    _slots[i].AddItem(item, count);
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


    private void OpenInventory()
    {
        _extendUI.gameObject.SetActive(true);
    }


    private void CloseInventory()
    {
        _extendUI.gameObject.SetActive(false);
    }
}