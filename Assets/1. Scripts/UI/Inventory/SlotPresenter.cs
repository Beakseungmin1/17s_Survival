using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _constantSlotViewsParents;
    [SerializeField] private GameObject _extendTopSlotViewsParents;
    [SerializeField] private GameObject _extendBottomSlotnViewsParents;
    [SerializeField] private SlotView[] _constantSlotViews; // after delete attribute
    [SerializeField] private SlotView[] _extendTopSlotnViews;
    [SerializeField] private SlotView[] _extendBottomSlotViews;

    private InventoryManager _inventoryManager;
    private SlotModel _slotModel;


    private void Awake()
    {
        _constantSlotViews = _constantSlotViewsParents.GetComponentsInChildren<SlotView>();
        _extendTopSlotnViews = _extendTopSlotViewsParents.GetComponentsInChildren<SlotView>();
        _extendBottomSlotViews = _extendBottomSlotnViewsParents.GetComponentsInChildren<SlotView>();

        _inventoryManager = GetComponentInParent<InventoryManager>();

        _slotModel = GetComponent<SlotModel>();
    }


    private void Start()
    {
        InitalizedViewsInfomation();
        _inventoryManager.updateUI += UpdateUI;
    }


    private void InitalizedViewsInfomation()
    {
        for (int i = 0; i < _slotModel.constantSlots.Length; i++)
        {
            if (_slotModel.constantSlots[i].item != null)
            {
                _constantSlotViews[i].DisplayItem(_slotModel.constantSlots[i].item.itemIcon, _slotModel.constantSlots[i].itemCount, SlotType.Constant, i);
            }
        }

        for (int i = 0; i < _slotModel.extendTopSlots.Length; i++)
        {
            if (_slotModel.extendTopSlots[i].item != null)
            {
                _extendTopSlotnViews[i].DisplayItem(_slotModel.extendTopSlots[i].item.itemIcon, _slotModel.extendTopSlots[i].itemCount, SlotType.ExtendTop, i);
            }
        }

        for (int i = 0; i < _slotModel.extendBottomSlots.Length; i++)
        {
            if (_slotModel.extendBottomSlots[i].item != null)
            {
                _extendBottomSlotViews[i].DisplayItem(_slotModel.extendBottomSlots[i].item.itemIcon, _slotModel.extendBottomSlots[i].itemCount, SlotType.ExtendBottom, i);
            }
        }
    }


    public void UpdateUI()
    {

    }
}
