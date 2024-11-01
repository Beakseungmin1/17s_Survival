using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private SlotPresenter _slotPresenter;
    private CraftingTablePresenter _craftingTablePresenter;
    private SlotModel _slotModel;

    public Action updateUI;


    private void Awake()
    {
        _slotPresenter = GetComponentInChildren<SlotPresenter>();
        _craftingTablePresenter = GetComponentInChildren<CraftingTablePresenter>();
        _slotModel = GetComponent<SlotModel>();
    }


    public void AddItem(ItemBase item) // Used when the player interacts
    {
        for (int i = 0; i < _slotModel.extendTopSlots.Length; i++)
        {
            if (_slotModel.extendTopSlots[i].item != null && _slotModel.extendTopSlots[i].item.itemName == item.itemName)
            {
                _slotModel.extendTopSlots[i].itemCount++;
                Debug.Log("같은 이름 있음");
                break;
            }

            if (_slotModel.extendTopSlots[i].item == null)
            {
                _slotModel.extendTopSlots[i].item = item;
                _slotModel.extendTopSlots[i].itemCount = 1;
                Debug.Log("같은 이름 없음");

                return;
            }
        }

        ThrowItem(item);
        // ui refresh
    }


    public void RemoveItem(int slotIndex)
    {
        _slotModel.extendTopSlots[slotIndex].item = null;
        _slotModel.extendTopSlots[slotIndex].itemCount = 0;
        // ui refresh
    }

    public void SubstractItemCount(int slotIndex, int consumeCount) // for Crafting
    {
        _slotModel.extendTopSlots[slotIndex].itemCount -= consumeCount;
        // ui refresh
    }

    public void SubstractItemCount(int slotIndex) // for Constrant Inventory
    {
        _slotModel.extendTopSlots[slotIndex].itemCount--;
        // ui refresh
    }

    private void ThrowItem(ItemBase Item)
    {
        Debug.Log("자리가 없음");
        // Instantiate(Item.itemPrefabs, transform.position, Quaternion.identity);
    }
}