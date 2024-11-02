using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;


public class DragSlot : ConvertSingleton<DragSlot>
{
    public Slot dargSlot;
    public int itemCount;
    [SerializeField] private Image _itemImage;


    protected override void Awake()
    {
        base.Awake();
    }


    public void DragSetImage(Sprite _itemImage)
    {
        this._itemImage.sprite = _itemImage;
        SetColor(1);
    }


    public void SetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }
}