using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public interface IBuildArchitecture
{
    public void SetIngredient(ItemSO item, int quntity = 1);
}


public class Build : MonoBehaviour, IBuildArchitecture
{
    private ItemInfomation _itemInfomation;
    private BuildingItemSO buildingItemSO;

    [SerializeField] private ItemSO needItem;
    [SerializeField] private int needCount;
    private int currentItemCount;


    private void Awake()
    {
        _itemInfomation = GetComponent<ItemInfomation>();

        buildingItemSO = (BuildingItemSO)_itemInfomation.item;
    }


    public void SetIngredient(ItemSO item, int quntity = 1)
    {
        if (item == needItem)
        {
            currentItemCount++;
        }

        CheckQuntity();
    }

    private void CheckQuntity()
    {
        if (needCount == currentItemCount)
        {
            BuildArchitecture();
        }
    }

    private void BuildArchitecture()
    {
        Instantiate(buildingItemSO.itemPrefabs, transform.position, quaternion.identity);
    }
}