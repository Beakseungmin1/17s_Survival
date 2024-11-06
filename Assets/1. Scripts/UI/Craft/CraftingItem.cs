using System;
using Unity.Mathematics;
using UnityEngine;

public class CraftingItem : MonoBehaviour
{
    private PlayerController _playerController;
    private UIToggle uIToggle;

    [Header("preview Building Information")]
    [SerializeField] private BuildingItemSO[] _buildingItems;
    private GameObject _previewBuilding;
    public bool _isPreviewActive;

    [Header("Use preview Building Movement")]
    private RaycastHit _hitInfo;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _range;
    [SerializeField] private Transform _playerTransform;

    [Tooltip("if Build(), change preview building material to white")]
    [SerializeField] private Material _whiteMaterial;

    private void Awake()
    {
        // _playerTransform = Camera.main.transform;
        _playerController = FindAnyObjectByType<PlayerController>();
        uIToggle = GetComponentInParent<UIToggle>();
    }


    private void Start()
    {
        if (_playerTransform == null)
        {
            Debug.LogError("not assigned _playerTransform (need main camera transform) ", gameObject);
        }
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


    private void PreviewPositionUpdate() // change the prefab transform of the preview building to the center of the screen
    {
        if (Physics.Raycast(_playerTransform.position, _playerTransform.forward, out _hitInfo, _range, _layerMask))
        {
            Debug.DrawRay(_playerTransform.position, _playerTransform.forward * _range, Color.red);
            if (_hitInfo.transform != null)
            {
                Vector3 location = _hitInfo.point;
                _previewBuilding.transform.position = location;
            }
        }
    }


    public void Build() // instantiate preview prefabs last PreviewPositionUpdate()
    {
        AudioManager.Instance.PlaySFX(PlayerSFX.Walk4);

        if (_previewBuilding.GetComponent<CraftingPreview>().isBuildable())
        {
            ItemInfomation itemInfomation = _previewBuilding.GetComponent<ItemInfomation>();
            GameObject tempPrefab = itemInfomation.item.itemPrefabs;

            GameObject instantBuilding = Instantiate(_previewBuilding, _hitInfo.point, quaternion.identity);

            CraftingPreview craftingPreview = instantBuilding.GetComponent<CraftingPreview>();

            craftingPreview.isBuilded = true;
            SetMaterialToWhite(instantBuilding);

            Destroy(_previewBuilding);
            _isPreviewActive = false;
            _previewBuilding = null;
        }
    }


    private void SetMaterialToWhite(GameObject building) // if Build(), change preview building material to white
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


    public void OnSlotClick(int _slotNumber) // when the button is pressed, preview building instantiate according to _slotNumber.
    {
        if (_isPreviewActive == true)
        {
            _previewBuilding = null;
            _isPreviewActive = false;
        }

        _previewBuilding = Instantiate(_buildingItems[_slotNumber].previewItemPrefabs, _playerTransform.position + _playerTransform.forward, quaternion.identity);
        _isPreviewActive = true;
        uIToggle.OnOpenUI();

        _playerController.ToggleCursor();
    }
}