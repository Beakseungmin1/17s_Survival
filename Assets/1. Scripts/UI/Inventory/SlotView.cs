using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Purchasing;

public class SlotView : MonoBehaviour
{
    private SlotPresenter _slotPresenter;

    Button _button;
    private Image _itemImage;
    private TextMeshProUGUI _itemCount;

    private int _slotIndex;
    private SlotType _slotType;


    private void Awake()
    {
        _slotPresenter = GetComponentInParent<SlotPresenter>();

        _itemImage = GetComponentInChildren<Image>();
        _itemCount = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void DisplayItem(Sprite itemImage, int itemCount, SlotType slotType, int slotIndex)
    {
        this._itemImage.sprite = itemImage;
        this._itemCount.text = itemCount.ToString();
        this._slotType = slotType;
        this._slotIndex = slotIndex;
    }


    public void SetItem(Sprite itemSprite, int itemCount = 1)
    {
        _itemCount.gameObject.SetActive(true);
        _itemImage.sprite = itemSprite;
        _itemCount.text = itemCount.ToString();
    }

    public void SetCount(int itemCount)
    {
        _itemCount.text = itemCount.ToString();
    }


    public void ClearItem(Sprite defaultImage)
    {
        _itemImage.sprite = defaultImage;
        _itemCount.text = string.Empty;
        _itemCount.gameObject.SetActive(false);
    }
}