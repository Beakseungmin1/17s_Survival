using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsingSlotItem : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    private PlayerController _playerController;
    private PlayerCondition _playerCondition;
    private CraftingItem _craftingItem;
    private GameObject curHand;
    private Outline curSlot;
    [SerializeField] private Transform EquipPosition;

    [SerializeField] private float _maxDistance = 10;
    [SerializeField] private LayerMask _previewLayer;

    Animator animator;

    private Camera _camera;
    private Dictionary<WeaponType, ResourceType> matchingTypeDict = new Dictionary<WeaponType, ResourceType>
    {
        { WeaponType.Axe, ResourceType.Tree },
        { WeaponType.Pickax, ResourceType.Rock }
    };

    private void Awake()
    {
        _camera = Camera.main;
        _playerController = GetComponent<PlayerController>();
        _playerCondition = GetComponent<PlayerCondition>();

        _inventory = FindAnyObjectByType<Inventory>();
        _craftingItem = FindAnyObjectByType<CraftingItem>();
    }


    private void Start()
    {
        _playerController.mouseLeftClick += OnClickLeftMouseButton;
        _playerController.PressedKeyPad += OnkeypadPress;

    }

    public void OnkeypadPress()
    {
        if (_playerController.selectedNumber < 5)
        {
            if (curSlot == null)
            {
                curSlot = _inventory.slots[_playerController.selectedNumber].GetComponent<Outline>();
                curSlot.enabled = true;
            }
            else
            {
                curSlot.enabled = false;
                curSlot = _inventory.slots[_playerController.selectedNumber].GetComponent<Outline>();
                curSlot.enabled = true;
            }

            if (_inventory.slots[_playerController.selectedNumber].item is WeaponSO curWeapon)
            {
                if (curWeapon.EquipPrefab != null)
                {
                    if (curHand == null)
                    {
                        curHand = Instantiate(curWeapon.EquipPrefab, EquipPosition);
                    }
                    else
                    {
                        Destroy(curHand);
                        curHand = Instantiate(curWeapon.EquipPrefab, EquipPosition);
                    }
                }

            }
            else
            {
                Destroy(curHand);
                curHand = null;
            }


        }
    }

    public void OnClickLeftMouseButton()
    {
        if (UIToggle.isUIOpen) return;
        if (_craftingItem._isPreviewActive)
        {
            _craftingItem.Build();
            return;
        }

        LeftMouseClicked();
    }


    public void LeftMouseClicked()
    {
        if (CheckSlotItemCount(_playerController.selectedNumber))
        {
            CheckItemType(_playerController.selectedNumber);
        }
    }


    public bool CheckSlotItemCount(int soltNumber)
    {
        if (_inventory.slots[soltNumber].item != null)
        {
            return true;
        }

        return false;
    }


    private void CheckItemType(int soltNumber)
    {
        switch (_inventory.slots[soltNumber].item.itemType)
        {
            case ItemType.Consumable:
                UseConsumableItem(soltNumber);
                break;
            case ItemType.Resource:
                InteractionPreviewBuilding(soltNumber);
                break;
            case ItemType.Weapon:
                Attack(soltNumber);
                break;
            case ItemType.Tool:
                Attack(soltNumber);
                break;
            default:
                Debug.Log(_inventory.slots[soltNumber].item + "is can't interaction item");
                break;
        }
    }


    private void UseConsumableItem(int soltNumber)
    {
        ConsumableSO consumableSO = (ConsumableSO)_inventory.slots[soltNumber].item;

        switch (consumableSO.consumableType) // use Value in consumableSO
        {
            case ConsumableType.Health:
                _playerCondition.Heal(consumableSO.value);
                _inventory.slots[soltNumber].SetSlotCount(-1);
                break;
            case ConsumableType.Hungry:
                _playerCondition.EatFood(consumableSO.value);
                _inventory.slots[soltNumber].SetSlotCount(-1);
                break;
            case ConsumableType.Thirst:
                _playerCondition.DrinkWater(consumableSO.value);
                _inventory.slots[soltNumber].SetSlotCount(-1);
                break;
            case ConsumableType.Stamina:
                _playerCondition.HealStamina(consumableSO.value);
                _inventory.slots[soltNumber].SetSlotCount(-1);
                break;
        }
    }


    private void InteractionPreviewBuilding(int soltNumber)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out hit, _maxDistance, _previewLayer))
        {

            Build build = hit.collider.gameObject.GetComponent<Build>();

            if (build != null)
            {
                if (build.CheckNeedItem(_inventory.slots[soltNumber].item))
                {
                    build.AddIngredientCount(_inventory.slots[soltNumber].item);
                    _inventory.slots[soltNumber].SetSlotCount(-1);
                    return;
                }
            }
        }
    }


    private void Attack(int soltNumber)
    {

        animator = curHand.GetComponent<Animator>();

        animator.SetTrigger("Attack");

    }

}
