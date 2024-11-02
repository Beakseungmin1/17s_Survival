using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class SlotPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _constantSlotViewsParents;
    [SerializeField] private GameObject _extendTopSlotViewsParents;
    [SerializeField] private GameObject _extendBottomSlotnViewsParents;
    [SerializeField] private SlotView[] _constantSlotViews; // after delete attribute
    [SerializeField] private SlotView[] _extendTopSlotViews;
    [SerializeField] private SlotView[] _extendBottomSlotViews;

    private InventoryManager _inventoryManager;
    private SlotModel _slotModel;


    private void Awake()
    {
        _constantSlotViews = _constantSlotViewsParents.GetComponentsInChildren<SlotView>();
        _extendTopSlotViews = _extendTopSlotViewsParents.GetComponentsInChildren<SlotView>();
        _extendBottomSlotViews = _extendBottomSlotnViewsParents.GetComponentsInChildren<SlotView>();

        _inventoryManager = GetComponentInParent<InventoryManager>();

        _slotModel = GetComponent<SlotModel>();

        InitalizedViewsInfomation();
    }


    private void InitalizedViewsInfomation()
    {
        for (int i = 0; i < _slotModel.constantSlots.Length; i++)
        {
            if (_slotModel.constantSlots[i].item != null)
            {
                _constantSlotViews[i].DisplayItem(_slotModel.constantSlots[i].item.itemIcon, _slotModel.constantSlots[i].itemCount, SlotType.Constant, i);

                _constantSlotViews[i].SetAlpha(true);
            }
        }

        for (int i = 0; i < _slotModel.extendTopSlots.Length; i++)
        {
            if (_slotModel.extendTopSlots[i].item != null)
            {
                _extendTopSlotViews[i].DisplayItem(_slotModel.extendTopSlots[i].item.itemIcon, _slotModel.extendTopSlots[i].itemCount, SlotType.ExtendTop, i);

                _extendTopSlotViews[i].SetAlpha(true);
            }
        }

        for (int i = 0; i < _slotModel.extendBottomSlots.Length; i++)
        {
            if (_slotModel.extendBottomSlots[i].item != null)
            {
                _extendBottomSlotViews[i].DisplayItem(_slotModel.extendBottomSlots[i].item.itemIcon, _slotModel.extendBottomSlots[i].itemCount, SlotType.ExtendBottom, i);

                _constantSlotViews[i].SetAlpha(true);
            }
        }
    }


    public void OpenExtedUI()
    {
        for (int i = 0; i < _slotModel.constantSlots.Length; i++)
        {
            if (_slotModel.constantSlots[i].item != null)
            {
                _slotModel.extendBottomSlots[i].item = _slotModel.constantSlots[i].item;
                _slotModel.extendBottomSlots[i].itemCount = _slotModel.constantSlots[i].itemCount;
                _slotModel.constantSlots[i].item = null;
                _slotModel.constantSlots[i].itemCount = 0;
            }
        }

        UpdateExtendUI();
    }


    public void CloseExtendUI()
    {
        for (int i = 0; i < _slotModel.extendBottomSlots.Length; i++)
        {
            if (_slotModel.extendBottomSlots[i].item != null)
            {
                _slotModel.constantSlots[i].item = _slotModel.extendBottomSlots[i].item;
                _slotModel.constantSlots[i].itemCount = _slotModel.extendBottomSlots[i].itemCount;
                _slotModel.extendBottomSlots[i].item = null;
                _slotModel.extendBottomSlots[i].itemCount = 0;
            }
        }

        UpdateConstantUI();
    }


    public void UpdateExtendUI()
    {
        for (int i = 0; i < _slotModel.extendTopSlots.Length; i++)
        {
            if (_slotModel.extendTopSlots[i].item != null)
            {
                _extendTopSlotViews[i].SetItem(_slotModel.extendTopSlots[i].item.itemIcon, _slotModel.extendTopSlots[i].itemCount);

                _extendTopSlotViews[i].SetAlpha(true);

            }
        }

        for (int i = 0; i < _slotModel.extendBottomSlots.Length; i++)
        {
            if (_slotModel.extendBottomSlots[i].item != null)
            {
                _extendBottomSlotViews[i].SetItem(_slotModel.extendBottomSlots[i].item.itemIcon, _slotModel.extendBottomSlots[i].itemCount);

                _extendBottomSlotViews[i].SetAlpha(true);
            }
        }
    }


    public void UpdateConstantUI()
    {
        for (int i = 0; i < _slotModel.constantSlots.Length; i++)
        {
            if (_slotModel.constantSlots[i].item != null)
            {
                _constantSlotViews[i].SetItem(_slotModel.constantSlots[i].item.itemIcon, _slotModel.constantSlots[i].itemCount);

                _constantSlotViews[i].SetAlpha(true);
            }
        }
    }


    public void UpdateItem()
    {
        for (int i = 0; i < _slotModel.extendTopSlots.Length; i++)
        {
            if (_slotModel.extendTopSlots[i].item != null)
            {
                _extendTopSlotViews[i].SetItem(_slotModel.extendTopSlots[i].item.itemIcon, _slotModel.extendTopSlots[i].itemCount);

                _extendTopSlotViews[i].SetAlpha(true);
            }
            else
            {
                _extendTopSlotViews[i].SetItem(_slotModel.slotDefaultImage, 0);
                _extendTopSlotViews[i].SetAlpha(false);
            }
        }

        for (int i = 0; i < _slotModel.extendBottomSlots.Length; i++)
        {
            if (_slotModel.extendBottomSlots[i].item != null)
            {
                _extendBottomSlotViews[i].SetItem(_slotModel.extendBottomSlots[i].item.itemIcon, _slotModel.extendBottomSlots[i].itemCount);

                _extendTopSlotViews[i].SetAlpha(true);
            }
            else
            {
                _extendBottomSlotViews[i].SetItem(_slotModel.slotDefaultImage, 0);
                _extendTopSlotViews[i].SetAlpha(false);

            }
        }
    }
}