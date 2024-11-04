using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    public ItemSO testItem;
    public static bool isInventoryOpend = false;


    [SerializeField] private GameObject _slotsParent;
    [SerializeField] private GameObject _extendUI;
    [HideInInspector] public Slot[] slots = new Slot[26];

    int selectedItemIndex = 0;

    private void Awake()
    {
        slots = _slotsParent.GetComponentsInChildren<Slot>();
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


    private void OpenInventory()
    {
        _extendUI.gameObject.SetActive(true);
    }


    private void CloseInventory()
    {
        _extendUI.gameObject.SetActive(false);
    }

    public void OnHotKey(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // Output different values ​​for pressed keys
            if (context.action.name == "HotKeyNum1")
            {
                if (slots[0].item != null)
                {
                    CharacterManager.Instance.Player.equip.EquipNew(slots[0].item);
                }
                else
                {
                    CharacterManager.Instance.Player.equip.UnEquip();
                }
            }
            else if (context.action.name == "HotKeyNum2")
            {
                if (slots[1].item != null)
                {
                    CharacterManager.Instance.Player.equip.EquipNew(slots[1].item);
                }
                else
                {
                    CharacterManager.Instance.Player.equip.UnEquip();
                }
            }
            else if (context.action.name == "HotKeyNum3")
            {
                if (slots[2].item != null)
                {
                    CharacterManager.Instance.Player.equip.EquipNew(slots[2].item);
                }
                else
                {
                    CharacterManager.Instance.Player.equip.UnEquip();
                }
            }
            else if (context.action.name == "HotKeyNum4")
            {
                if (slots[3].item != null)
                {
                    CharacterManager.Instance.Player.equip.EquipNew(slots[3].item);
                }
                else
                {
                    CharacterManager.Instance.Player.equip.UnEquip();
                }
            }
            else if (context.action.name == "HotKeyNum5")
            {
                if (slots[4].item != null)
                {
                    CharacterManager.Instance.Player.equip.EquipNew(slots[4].item);
                }
                else
                {
                    CharacterManager.Instance.Player.equip.UnEquip();
                }
            }
        }
    }


}