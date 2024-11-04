using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class CombinationItemButton : MonoBehaviour
{
    [Header("TargetUI")]
    [SerializeField] private TextMeshProUGUI _tartgetItemName;
    [SerializeField] private Image _targetItemImage;
    [SerializeField] private TextMeshProUGUI _needItems;
    [SerializeField] private Button makeItemButton;

    [Header("NeedItemUI")]
    [SerializeField] private Image[] _needImages;
    [SerializeField] private TextMeshProUGUI[] _needCount;


    [Space(20)]
    [Header("Item")]
    [SerializeField] private DecorationSO _targetItem;
    private CombinationItem _combinationItem;


    private void Awake()
    {
        _combinationItem = GetComponentInParent<CombinationItem>();
        makeItemButton.onClick.AddListener(OnMakeItem);
    }


    private void Start()
    {
        _tartgetItemName.text = _targetItem.itemName;
        _targetItemImage.sprite = _targetItem.itemIcon;
        SetNeedItemText();
    }


    private void SetNeedItemText()
    {
        for (int i = 0; i < _needImages.Length; i++)
        {
            _needImages[i].sprite = _targetItem.needItems[i].needItem.itemIcon;
            _needCount[i].text = _targetItem.needItems[i].needCount.ToString();
        }
    }


    public void OnMakeItem()
    {
        _combinationItem.MakeItem(_targetItem);
    }
}
