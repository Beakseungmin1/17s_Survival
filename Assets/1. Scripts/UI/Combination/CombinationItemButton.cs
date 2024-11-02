using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;
using JetBrains.Annotations;
using System;


public class CombinationItemButton : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _tartgetItemName;
    [SerializeField] private Image _targetItemImage;
    [SerializeField] private TextMeshProUGUI _needItems;
    [SerializeField] private Button makeItemButton;

    [Space(20)]
    [Header("Item")]
    [SerializeField] private DecorationSO _targetItem;
    private Inventory _inventory;


    private void Awake()
    {
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
        for (int i = 0; i < _targetItem.needItems.Length; i++)
        {
            _needItems.text = _targetItem.needItems[i].needItem + "" + _targetItem.needItems[i].needCount.ToString() + "\n";
        }
    }


    public void OnMakeItem()
    {
        CheckInventoryItem();
    }

    private void CheckInventoryItem()
    {

    }
}
