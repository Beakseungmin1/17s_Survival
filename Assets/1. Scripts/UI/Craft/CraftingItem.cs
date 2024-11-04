using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CraftingItem : MonoBehaviour
{
    [SerializeField] private BuildingItemSO[] _buildingItems;
    [SerializeField] private GameObject craftingPanel;
    private GameObject _previewBuilding;
    private bool _isPreviewActive;


    private RaycastHit _hitInfo;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _range;
    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        // _playerTransform = Camera.main.transform;
    }


    void Update()
    {
        if (_isPreviewActive)
        {
            PreviewPositionUpdate();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Build();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    private void Build()
    {
        ItemInfomation itemInfomation = _previewBuilding.GetComponent<ItemInfomation>();

        GameObject tempPrefab = itemInfomation.item.itemPrefabs;

        Instantiate(_previewBuilding, _hitInfo.point + new Vector3(0, tempPrefab.transform.position.y, 0), quaternion.identity);
        Destroy(_previewBuilding);
        _isPreviewActive = false;
        _previewBuilding = null;
    }

    private void PreviewPositionUpdate()
    {
        if (Physics.Raycast(_playerTransform.position, _playerTransform.forward, out _hitInfo, _range, _layerMask))
        {
            if (_hitInfo.transform != null)
            {
                Vector3 location = _hitInfo.point;
                _previewBuilding.transform.position = location;
            }
        }
    }

    private void Cancel()
    {
        if (_isPreviewActive)
        {
            Destroy(_previewBuilding);
            _isPreviewActive = false;
            _previewBuilding = null;
            craftingPanel.gameObject.SetActive(false);
        }
    }

    public void OnSlotClick(int _slotNumber)
    {
        if (_isPreviewActive == true)
        {
            _previewBuilding = null;
            _isPreviewActive = false;
        }

        _previewBuilding = Instantiate(_buildingItems[_slotNumber].previewItemPrefabs, _playerTransform.position + _playerTransform.forward, quaternion.identity);
        _isPreviewActive = true;
        craftingPanel.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_playerTransform.position, _playerTransform.forward, Color.red, _range);
    }
}