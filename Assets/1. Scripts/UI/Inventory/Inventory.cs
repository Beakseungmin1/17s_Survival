using System;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _slotsParent;
    [SerializeField] private GameObject _extendUI;

    [Header("Slots Data")]
    [HideInInspector] public Slot[] slots = new Slot[26];

    [Tooltip("player object transform in hierarchy")]
    [SerializeField] private Transform _playerTransform;


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


    private void ThrowItem(ItemSO item) // when the inventory is full
    {
        Instantiate(item, _playerTransform.transform.position + new Vector3(0, 0, 1), Quaternion.identity);
    }
}