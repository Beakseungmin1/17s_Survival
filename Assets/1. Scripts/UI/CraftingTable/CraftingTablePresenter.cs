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

    private CraftingTableModel craftingTableModel;


    private void Awake()
    {
        _buildingViews = _buildingViewsParents.GetComponentsInChildren<CraftingTableView>();
        _decorationViews = _decorationViewsParents.GetComponentsInChildren<CraftingTableView>();

        craftingTableModel = GetComponent<CraftingTableModel>();
    }


    private void Start()
    {
        InitalizedViewsInfomation();
    }


    private void InitalizedViewsInfomation()
    {
        for (int i = 0; i < _buildingViews.Length; i++)
        {
            _buildingViews[i].DisplayItem(craftingTableModel.buildingItems[i].itemIcon, i, ItemType.Building);
            _decorationViews[i].DisplayItem(craftingTableModel.decorationItems[i].itemIcon, i, ItemType.Decoration);
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
        BuildingItem buildingItem = craftingTableModel.buildingItems[itemNumber];

        CheckQuantity(buildingItem);
    }


    private void CheckQuantity(BuildingItem buildingItem)
    {
        Dictionary<int, int> itemsToConsume = new Dictionary<int, int>();
        int quantity = 0;


        for (int i = 0; i < craftingTableModel.slots.Length; i++)
        {
            if (craftingTableModel.slots[i].item == null)
            {
                Debug.Log(i + "번째 슬롯은 비어있음");
                continue;
            }

            if (craftingTableModel.slots[i].item.itemName == buildingItem.itemName)
            {
                for (int j = 0; j < buildingItem.needItems.Length; j++)
                {
                    if (craftingTableModel.slots[i].item == buildingItem.needItems[j].needItem)
                    {
                        if (craftingTableModel.slots[i].itemCount >= buildingItem.needItems[j].needCount)
                        {
                            quantity++;
                            itemsToConsume[i] = buildingItem.needItems[j].needCount;
                        }
                        else
                        {
                            Debug.Log(craftingTableModel.slots[i].item + "의 개수가 부족합니다");
                            return;
                        }
                    }
                }
            }
        }

        if (quantity == buildingItem.needItems.Length)
        {
            ApplyMakeItem(itemsToConsume, buildingItem);
        }
    }


    private void ApplyMakeItem(Dictionary<int, int> itemsToConsume, ItemBase item)
    {
        foreach (var Selecteditem in itemsToConsume)
        {
            int slotIndex = Selecteditem.Key;
            int consumeCount = Selecteditem.Value;

            craftingTableModel.slots[slotIndex].itemCount -= consumeCount; // reduce quantity

            if (craftingTableModel.slots[slotIndex].itemCount <= 0)
            {
                // slot[i].itemCount <= clearSlot 
            }

            AddItemToInventory(item);
        }
    }

    private void AddItemToInventory(ItemBase item)
    {

        for (int i = 0; i < craftingTableModel.slots.Length; i++) // have empty slot
        {
            if (craftingTableModel.slots[i].item == null)
            {
                craftingTableModel.slots[i].item = item;
                craftingTableModel.slots[i].itemCount++;
                break;
            }

            // inventory ui refresh
            ThrowItem(item);
        }
    }

    private void ThrowItem(ItemBase Item)
    {
        Instantiate(Item.itemPrefabs, transform.position, Quaternion.identity);
    }
}