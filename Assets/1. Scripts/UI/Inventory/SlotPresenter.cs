using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _constantSlotViewsParents;
    [SerializeField] private GameObject _extendSlotViewsParents;
    [SerializeField] private SlotView[] _constantSlotViews; // after delete attribute
    [SerializeField] private SlotView[] _extendSlotnViews;

    private SlotModel _slotModel;


    private void Awake()
    {
        _constantSlotViews = _constantSlotViewsParents.GetComponentsInChildren<SlotView>();
        _extendSlotnViews = _extendSlotViewsParents.GetComponentsInChildren<SlotView>();

        _slotModel = GetComponent<SlotModel>();
    }

    private void Start()
    {
        InitalizedViewsInfomation();
    }

    private void InitalizedViewsInfomation()
    {
        for (int i = 0; i < _slotModel.constantSlots.Length; i++)
        {
            if (_slotModel.constantSlots[i].item != null)
            {
                _constantSlotViews[i].DisplayItem(_slotModel.constantSlots[i].item.itemIcon, _slotModel.constantSlots[i].itemCount);
            }
        }

        for (int i = 0; i < _slotModel.extendSlots.Length; i++)
        {
            if (_slotModel.extendSlots[i].item != null)
            {
                _extendSlotnViews[i].DisplayItem(_slotModel.extendSlots[i].item.itemIcon, _slotModel.extendSlots[i].itemCount);
            }
        }

    }
}