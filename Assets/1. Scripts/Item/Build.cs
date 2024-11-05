using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Build : MonoBehaviour
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

    public bool CheckNeedItem(ItemSO item)
    {
        if (needItem == item)
        {
            return true;
        }
        Debug.Log("Current : " + item + "Need" + needItem);
        return false;
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
        Destroy(gameObject);
    }
}