using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotView : MonoBehaviour
{
    private SlotPresenter _slotPresenter;

    private Image _itemImage;
    private TextMeshProUGUI _itemCount;

    private void Awake()
    {
        _slotPresenter = GetComponentInParent<SlotPresenter>();

        _itemImage = GetComponentInChildren<Image>();
        _itemCount = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void DisplayItem(Sprite itemImage, int itemCount)
    {
        this._itemImage.sprite = itemImage;
        this._itemCount.text = itemCount.ToString();
    }
}