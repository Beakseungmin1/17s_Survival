using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationItem : MonoBehaviour
{
    [SerializeField] Inventory _inventory;

    public void MakeItem(DecorationSO item)
    {
        Dictionary<int, int> itemsToConsume = new Dictionary<int, int>();
        int quantity = 0;

        for (int i = 0; i < _inventory.slots.Length; i++)
        {
            if (_inventory.slots[i].item == null)
            {
                // Debug.Log(i + "번째 슬롯은 비어있음");
                continue;
            }


            for (int j = 0; j < item.needItems.Length; j++)
            {
                if (_inventory.slots[i].item == item.needItems[j].needItem)
                {
                    if (_inventory.slots[i].itemCount >= item.needItems[j].needCount)
                    {
                        quantity++;
                        itemsToConsume[i] = item.needItems[j].needCount;
                    }
                    else
                    {
                        Debug.Log(_inventory.slots[i].item + "의 개수가 부족합니다");
                        return;
                    }
                }

            }
        }

        if (quantity == item.needItems.Length)
        {
            ApplyMakeItem(itemsToConsume, item);
        }
    }


    private void ApplyMakeItem(Dictionary<int, int> itemsToConsume, ItemSO item)
    {
        foreach (var Selecteditem in itemsToConsume)
        {
            int slotIndex = Selecteditem.Key;
            int consumeCount = Selecteditem.Value;

            _inventory.slots[slotIndex].SetSlotCount(-consumeCount);
        }

        _inventory.AcquireItem(item);
    }
}