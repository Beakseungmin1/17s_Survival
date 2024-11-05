using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingSlotItem : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    private PlayerController _playerController;
    private CraftingItem _craftingItem;

    [SerializeField] private float _maxDistance = 10;
    [SerializeField] private LayerMask _previewLayer;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _playerController = GetComponent<PlayerController>();

        _inventory = FindAnyObjectByType<Inventory>();
        _craftingItem = FindAnyObjectByType<CraftingItem>();
    }


    private void Start()
    {
        _playerController.mouseLeftClick += OnClickLeftMouseButton;
    }


    public void OnClickLeftMouseButton()
    {
        if (_inventory.isInventoryOpend) return;
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
            case ItemType.ReSource:
                InteractionPreviewBuilding(soltNumber);
                break;
            case ItemType.Weapon:
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

                _inventory.slots[soltNumber].SetSlotCount(-1);
                break;
            case ConsumableType.Hungry:

                _inventory.slots[soltNumber].SetSlotCount(-1);
                break;
            case ConsumableType.Thirst:

                _inventory.slots[soltNumber].SetSlotCount(-1);
                break;
            case ConsumableType.Stamina:

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
                    build.SetIngredient(_inventory.slots[soltNumber].item);
                    _inventory.slots[soltNumber].SetSlotCount(-1);
                    return;
                }
            }
        }
    }


    private void Attack(int soltNumber)
    {
        //ItemSO item = _inventory.slots[soltNumber].item;
        //Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //RaycastHit hit;

        //// Damage and range are given to each weapon
        //// Damage and resource damage are different values
        //if (Physics.Raycast(ray, out hit, attackDistance))
        //{
        //    // If you want to use it, create a Resource script for the lecture.
        //    if (item.itemType == ItemType.Tool && hit.collider.TryGetComponent(out Resource resource))
        //    {
        //        resource.Gather(hit.point, hit.normal);
        //    }

        //    if (hit.collider.TryGetComponent(out NPC npc))
        //    {
        //        npc.TakePhysicalDamage(damage);
        //    }
        //}
    }


}
