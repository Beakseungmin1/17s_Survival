using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;


public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("Item")]
    public ItemSO item;
    public int itemCount;


    [Space(10)]
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _itemCountText;
    [SerializeField] Image _itemImage = null;
    [SerializeField] ItemType _slotAllowedItemType;
    [SerializeField] EquipmentType _slotAllowedEquipmentType;


    public void AddItem(ItemSO item, int quntity = 1)
    {
        this.item = item;
        itemCount = quntity;
        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = item.itemIcon;
        if (item.itemType != ItemType.Equipment || item.itemType != ItemType.Weapon)
        {
            _itemCountText.text = itemCount.ToString();
        }

        SetColor(1);
    }


    public void SetSlotCount(int quntity = 1)
    {
        itemCount += quntity;
        _itemCountText.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }


    private void SetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }


    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        _itemImage.sprite = null;
        _itemCountText.text = string.Empty;
        _itemImage.gameObject.SetActive(false);
        SetColor(0);
    }


    public void OnBeginDrag(PointerEventData eventData)// when Drag Start on a slot that has this script
    {
        if (item != null)
        {
            DragSlot.Instance.dargSlot = this;
            DragSlot.Instance.itemCount = itemCount;
            DragSlot.Instance.DragSetImage(item.itemIcon);
            DragSlot.Instance.transform.position = eventData.position;
        }
    }


    public void OnDrag(PointerEventData eventData) // when Draging
    {
        if (item != null)
        {
            DragSlot.Instance.transform.position = eventData.position;
        }
    }


    public void OnEndDrag(PointerEventData eventData) // when drag ended on not slot
    {
        ClearDragSlot();
    }


    public void OnDrop(PointerEventData eventData) // when Drag Ended on slot
    {
        if (DragSlot.Instance.dargSlot != null)
        {
            if (_slotAllowedItemType == ItemType.All)
            {
                ChangeSlot();
                return;
            }

            if (_slotAllowedItemType == DragSlot.Instance.dargSlot.item.itemType)
            {
                CheckEquipmentType(DragSlot.Instance.dargSlot.item);
                ClearDragSlot();
                return;
            }
            else
            {
                ClearDragSlot();
            }
        }
    }


    private void CheckEquipmentType(ItemSO item)
    {
        EquipmentSO equipmentSO = (EquipmentSO)item;

        if (equipmentSO == null) return;

        if (equipmentSO.equipmentType == _slotAllowedEquipmentType)
        {
            int hpValue = equipmentSO.IncreaseHpStat;
            int staminaValue = equipmentSO.IncreaseStaminaStat;

            // MethodName(hpValue, staminaValue);
            // increase hp or stamina maxValue
        }
    }


    private void ClearDragSlot()
    {
        DragSlot.Instance.SetColor(0);
        DragSlot.Instance.dargSlot = null;
    }


    private void ChangeSlot()
    {
        ItemSO tempItem = item;
        int tempCount = itemCount;

        AddItem(DragSlot.Instance.dargSlot.item, DragSlot.Instance.itemCount);

        if (tempItem != null)
        {
            DragSlot.Instance.dargSlot.AddItem(tempItem, tempCount);

            if (tempItem.itemType == ItemType.Equipment)
            {
                EquipmentSO equipmentSO = (EquipmentSO)tempItem;

                int hpValue = equipmentSO.IncreaseHpStat;
                int staminaValue = equipmentSO.IncreaseStaminaStat;

                // MethodName(hpValue, staminaValue);
                // increase hp or stamina maxValue
            }
        }
        else
        {
            DragSlot.Instance.dargSlot.ClearSlot();
        }
    }
}