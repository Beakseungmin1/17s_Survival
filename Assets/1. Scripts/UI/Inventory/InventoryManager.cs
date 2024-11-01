using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private SlotPresenter _slotPresenter;
    private CraftingTablePresenter _craftingTablePresenter;
    private SlotModel _slotModel;

    public Action updateExtendUI;

    private bool isOpendExtendInventory = false;
    [SerializeField] private GameObject constantSlotPnael;
    [SerializeField] private GameObject extentSlotPanel;

    private void Awake()
    {
        _slotPresenter = GetComponentInChildren<SlotPresenter>();
        _craftingTablePresenter = GetComponentInChildren<CraftingTablePresenter>();
        _slotModel = GetComponent<SlotModel>();
    }

    private void Start()
    {
        extentSlotPanel.gameObject.SetActive(false);
        constantSlotPnael.gameObject.SetActive(true);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!isOpendExtendInventory)
            {
                isOpendExtendInventory = true;
                extentSlotPanel.gameObject.SetActive(true);
                constantSlotPnael.gameObject.SetActive(false);

                _slotPresenter.OpenExtedUI();
            }
            else
            {
                isOpendExtendInventory = false;
                extentSlotPanel.gameObject.SetActive(false);
                constantSlotPnael.gameObject.SetActive(true);

                _slotPresenter.CloseExtendUI();
            }
        }
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

        _slotPresenter.UpdateItem();
    }


    public void RemoveItem(int slotIndex)
    {
        _slotModel.extendTopSlots[slotIndex].item = null;
        _slotModel.extendTopSlots[slotIndex].itemCount = 0;
        _slotPresenter.UpdateItem();
    }

    public void SubstractItemCount(int slotIndex, int consumeCount) // for Crafting
    {
        _slotModel.extendTopSlots[slotIndex].itemCount -= consumeCount;
        _slotPresenter.UpdateItem();
    }

    public void SubstractItemCount(int slotIndex) // for Constrant Inventory
    {
        _slotModel.extendTopSlots[slotIndex].itemCount--;
        _slotPresenter.UpdateItem();
    }

    private void ThrowItem(ItemBase Item)
    {
        // Instantiate(Item.itemPrefabs, transform.position, Quaternion.identity);
    }
}