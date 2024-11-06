using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CraftingItem : MonoBehaviour
{
    private UIToggle uIToggle;
    [SerializeField] private BuildingItemSO[] _buildingItems;
    private GameObject _previewBuilding;
    [SerializeField] private Material _whiteMaterial;
    public bool _isPreviewActive;


    private RaycastHit _hitInfo;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _range;
    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = Camera.main.transform;
        uIToggle = GetComponentInParent<UIToggle>();
    }


    void Update()
    {
        if (_isPreviewActive)
        {
            PreviewPositionUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }

    public void Build()
    {
        if (_previewBuilding.GetComponent<CraftingPreview>().isBuildable())
        {
            ItemInfomation itemInfomation = _previewBuilding.GetComponent<ItemInfomation>();
            GameObject tempPrefab = itemInfomation.item.itemPrefabs;

            GameObject instantBuilding = Instantiate(_previewBuilding, _hitInfo.point + new Vector3(0, tempPrefab.transform.position.y, 0), quaternion.identity);

            CraftingPreview craftingPreview = instantBuilding.GetComponent<CraftingPreview>();

            craftingPreview.isBuilded = true;
            SetMaterialToWhite(instantBuilding);

            Destroy(_previewBuilding);
            _isPreviewActive = false;
            _previewBuilding = null;
        }
    }


    private void SetMaterialToWhite(GameObject building)
    {
        foreach (Transform child in building.transform)
        {
            var newMaterials = new Material[child.GetComponent<Renderer>().materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = _whiteMaterial;
            }
            child.GetComponent<Renderer>().materials = newMaterials;
        }
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
            uIToggle.OnOpenUI();
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
        uIToggle.OnOpenUI();
    }

    // private void OnDrawGizmos()
    // {
    //     Debug.DrawRay(_playerTransform.position, _playerTransform.forward, Color.red, _range);
    // }
}