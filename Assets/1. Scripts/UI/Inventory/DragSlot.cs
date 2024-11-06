using UnityEngine;
using UnityEngine.UI;


public class DragSlot : ConvertSingleton<DragSlot>
{
    public Slot dargSlot;
    public int itemCount;

    [SerializeField] private Image _itemImage;


    protected override void Awake()
    {
        base.Awake(); // ConvertSingleton
    }


    public void DragSlotSetImage(Sprite _itemImage)
    {
        this._itemImage.sprite = _itemImage;
        DragSlotSetColor(1);
    }


    public void DragSlotSetColor(float alpha)
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }
}