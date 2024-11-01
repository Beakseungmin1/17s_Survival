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
    [SerializeField] private Image _itemImage;
    private TextMeshProUGUI _itemCount;

    private int _slotIndex;
    private SlotType _slotType;


    private void Awake()
    {
        _slotPresenter = GetComponentInParent<SlotPresenter>();

        _itemCount = GetComponentInChildren<TextMeshProUGUI>();

        _itemCount.text = string.Empty;
    }


    public void DisplayItem(Sprite itemImage, int itemCount, SlotType slotType, int slotIndex)
    {
        this._itemImage.sprite = itemImage;
        this._itemCount.text = itemCount <= 0 ? "" : itemCount.ToString();
        this._slotType = slotType;
        this._slotIndex = slotIndex;
    }


    public void SetItem(Sprite itemSprite, int itemCount = 1)
    {
        _itemCount.gameObject.SetActive(true);
        _itemImage.sprite = itemSprite;

        if (itemCount == 0) return;
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

    public void SetAlpha(bool isActive)
    {
        if (isActive)
        {
            _itemImage.color = new Color(_itemImage.color.r, _itemImage.color.g, _itemImage.color.b, 1f);
        }
        else
        {
            _itemImage.color = new Color(_itemImage.color.r, _itemImage.color.g, _itemImage.color.b, 0f);
        }
    }
}