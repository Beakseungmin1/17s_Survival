using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("Item")]
    public ItemSO item;
    public int itemCount;

    [Space(10)]
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _itemCountText;
    [SerializeField] Image _itemImage = null;

    [Space(10)]
    [Header("Each Slot Type")]
    [SerializeField] ItemType _slotAllowedItemType;
    [SerializeField] EquipmentType _slotAllowedEquipmentType;


    #region  Item Control In Slot
    public void AddItem(ItemSO item, int quntity = 1)
    {
        this.item = item;
        itemCount = quntity;

        _itemImage.gameObject.SetActive(true);
        _itemImage.sprite = item.itemIcon;

        if (item.itemType != ItemType.Equipment || item.itemType != ItemType.Weapon)
        {
            _itemCountText.text = itemCount.ToString();
        }

        SetColor(1);
    }


    public void SetSlotCount(int quntity = 1)
    {
        itemCount += quntity;
        _itemCountText.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }


    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        _itemImage.sprite = null;
        _itemCountText.text = string.Empty;
        SetColor(0);
        _itemImage.gameObject.SetActive(false);
    }


    private void SetColor(float alpha) // if slot itemSO != null, change item sprite alpha
    {
        Color color = _itemImage.color;
        color.a = alpha;
        _itemImage.color = color;
    }
    #endregion


    #region Item Darg Interface
    public void OnBeginDrag(PointerEventData eventData) // when drag start on a slot that has this script
    {
        AudioManager.Instance.PlaySFX(UISFX.Click);

        if (item != null)
        {
            DragSlot.Instance.dargSlot = this;
            DragSlot.Instance.itemCount = itemCount;
            DragSlot.Instance.DragSlotSetImage(item.itemIcon);
            DragSlot.Instance.transform.position = eventData.position;
        }
    }


    public void OnDrag(PointerEventData eventData) // when draging
    {
        if (item != null)
        {
            DragSlot.Instance.transform.position = eventData.position;
        }
    }


    public void OnEndDrag(PointerEventData eventData) // when drag ended on not slot
    {
        ClearDragSlot();
    }


    public void OnDrop(PointerEventData eventData) // when drag ended on slot
    {
        AudioManager.Instance.PlaySFX(UISFX.OpenUI);

        if (DragSlot.Instance.dargSlot != null)
        {
            if (_slotAllowedItemType == ItemType.All)
            {
                ChangeSlot();
                return;
            }

            if (_slotAllowedItemType == ItemType.Equipment)
            {
                ChangeSlotEquipmentType();
                return;
            }
        }
    }
    #endregion


    private void ChangeSlot() // change the item location if drop location is a slot but slot type is all
    {
        ItemSO tempItem = item;
        int tempCount = itemCount;

        AddItem(DragSlot.Instance.dargSlot.item, DragSlot.Instance.itemCount);

        if (tempItem != null)
        {
            DragSlot.Instance.dargSlot.AddItem(tempItem, tempCount);
        }
        else
        {
            DragSlot.Instance.dargSlot.ClearSlot();
        }
    }


    private void ChangeSlotEquipmentType() // change the item location if drop location is a slot but slot type is equipment
    {
        ItemSO tempItem = item;
        int tempCount = itemCount;

        if (DragSlot.Instance.dargSlot.item is EquipmentSO)
        {
            EquipmentSO equipmentSO = (EquipmentSO)DragSlot.Instance.dargSlot.item;

            if (_slotAllowedEquipmentType == equipmentSO.equipmentType)
            {
                AddItem(DragSlot.Instance.dargSlot.item, DragSlot.Instance.itemCount);

                int hpValue = equipmentSO.IncreaseHpStat;
                int staminaValue = equipmentSO.IncreaseStaminaStat;

                if (tempItem != null)
                {
                    DragSlot.Instance.dargSlot.AddItem(tempItem, tempCount);
                }
                else
                {
                    DragSlot.Instance.dargSlot.ClearSlot();
                }

                // MethodName(hpValue, staminaValue);
                // increase hp or stamina maxValue
            }
            else
            {
                ClearDragSlot();
            }
        }
    }


    private void ClearDragSlot()
    {
        DragSlot.Instance.DragSlotSetColor(0);
        DragSlot.Instance.dargSlot = null;
    }
}