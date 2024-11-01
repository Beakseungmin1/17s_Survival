using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingTablePresenter : MonoBehaviour
{
    [SerializeField] private GameObject _buildingViewsParents;
    [SerializeField] private GameObject _decorationViewsParents;
    [SerializeField] private CraftingTableView[] _buildingViews;
    [SerializeField] private CraftingTableView[] _decorationViews;

    [SerializeField] private InventoryManager _inventoryManager;
    private CraftingTableModel _craftingTableModel;
    [SerializeField] private SlotModel _slotModel;


    private void Awake()
    {
        _buildingViews = _buildingViewsParents.GetComponentsInChildren<CraftingTableView>();
        _decorationViews = _decorationViewsParents.GetComponentsInChildren<CraftingTableView>();

        _craftingTableModel = GetComponent<CraftingTableModel>();
    }


    private void Start()
    {
        InitalizedViewsInfomation();
    }


    private void InitalizedViewsInfomation()
    {
        for (int i = 0; i < _buildingViews.Length; i++)
        {
            _buildingViews[i].DisplayItem(_craftingTableModel.buildingItems[i].itemIcon, i, ItemType.Building);
            _decorationViews[i].DisplayItem(_craftingTableModel.decorationItems[i].itemIcon, i, ItemType.Decoration);
        }
    }


    public void CraftItem(ItemType itemType, int itemNumber)
    {
        switch (itemType)
        {
            case ItemType.Building:
                CheckEnoughItems(itemNumber);
                break;
            case ItemType.Decoration:
                // CheckEnoughItems(itemNumber);
                break;
        }
    }


    private void CheckEnoughItems(int itemNumber)
    {
        BuildingItemSO buildingItem = _craftingTableModel.buildingItems[itemNumber];

        CheckQuantity(buildingItem);
    }


    private void CheckQuantity(BuildingItemSO buildingItem)
    {
        Dictionary<int, int> itemsToConsume = new Dictionary<int, int>();
        int quantity = 0;


        for (int i = 0; i < _slotModel.extendTopSlots.Length; i++)
        {
            if (_slotModel.extendTopSlots[i].item == null)
            {
                // Debug.Log(i + "번째 슬롯은 비어있음");
                continue;
            }


            for (int j = 0; j < buildingItem.needItems.Length; j++)
            {
                if (_slotModel.extendTopSlots[i].item == buildingItem.needItems[j].needItem)
                {
                    if (_slotModel.extendTopSlots[i].itemCount >= buildingItem.needItems[j].needCount)
                    {
                        quantity++;
                        itemsToConsume[i] = buildingItem.needItems[j].needCount;

                    }
                    else
                    {
                        Debug.Log(_slotModel.extendTopSlots[i].item + "의 개수가 부족합니다");
                        return;
                    }
                }

            }
        }

        if (quantity == buildingItem.needItems.Length)
        {
            ApplyMakeItem(itemsToConsume, buildingItem);
        }
    }


    private void ApplyMakeItem(Dictionary<int, int> itemsToConsume, ItemSO item)
    {
        foreach (var Selecteditem in itemsToConsume)
        {
            int slotIndex = Selecteditem.Key;
            int consumeCount = Selecteditem.Value;

            _inventoryManager.SubstractItemCount(slotIndex, consumeCount);

            if (_slotModel.extendTopSlots[slotIndex].itemCount <= 0)
            {
                _inventoryManager.RemoveItem(slotIndex);
            }
        }

        _inventoryManager.AddItem(item);
    }
}