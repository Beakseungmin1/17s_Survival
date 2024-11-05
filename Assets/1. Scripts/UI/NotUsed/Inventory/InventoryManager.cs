using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    private SlotPresenter _slotPresenter;
    private CraftingTablePresenter _craftingTablePresenter;
    private SlotModel _slotModel;
    private Transform _dropPosition;

    public Action updateExtendUI;

    // private bool isOpendExtendInventory = false;
    [SerializeField] private GameObject constantSlotPanel;
    [SerializeField] private GameObject extentSlotPanel;

    private PlayerController controller;
    private PlayerCondition condition;

    private void Awake()
    {
        _slotPresenter = GetComponentInChildren<SlotPresenter>();
        _craftingTablePresenter = GetComponentInChildren<CraftingTablePresenter>();
        _slotModel = GetComponent<SlotModel>();
    }

    private void Start()
    {
        _dropPosition = CharacterManager.Instance.Player.dropPosition;
        controller = CharacterManager.Instance.Player.GetComponent<PlayerController>();
        condition = CharacterManager.Instance.Player.GetComponent<PlayerCondition>();

        controller.inventory += Toggle;

        extentSlotPanel.gameObject.SetActive(false);
        constantSlotPanel.gameObject.SetActive(true);
    }

    public void AddItem(ItemSO item) // Used when the player interacts
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

    private void ThrowItem(ItemSO Item)
    {
        Instantiate(Item.itemPrefabs, _dropPosition.position, Quaternion.identity);
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            extentSlotPanel.SetActive(false);
            constantSlotPanel.SetActive(true);

        }
        else
        {
            extentSlotPanel.SetActive(true);
            constantSlotPanel.SetActive(false);

        }
    }

    public bool IsOpen()
    {
        return extentSlotPanel.activeInHierarchy;
    }
}