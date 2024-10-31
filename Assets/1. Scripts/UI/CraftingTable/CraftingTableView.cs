using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingTableView : MonoBehaviour
{
    private CraftingTablePresenter _craftingTablePresenter;

    [SerializeField] private Button _CreateItembutton;

    [SerializeField] private Sprite _itemSprite;
    private ItemType _itemType;
    private int _viewNumber;

    public void DisplayItem(Sprite itemIamge, int index, ItemType itemType)
    {
        this._viewNumber = index;
        this._itemSprite = itemIamge;
        this._itemType = itemType;

        _CreateItembutton.image.sprite = _itemSprite;
    }

    private void Awake()
    {
        _craftingTablePresenter = GetComponentInParent<CraftingTablePresenter>();
        _CreateItembutton = GetComponentInParent<Button>();
    }

    private void Start()
    {
        _CreateItembutton.onClick.AddListener(OnCraftButton);
    }

    private void OnCraftButton()
    {
        _craftingTablePresenter.CraftItem(_itemType, _viewNumber);
    }
}
