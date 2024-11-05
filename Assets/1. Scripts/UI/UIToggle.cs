using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIToggle : MonoBehaviour
{
    public static bool isUIOpen; // if true, attck/build/interaction is impossible
    [SerializeField] PlayerController _playerController;
    [SerializeField] Image[] _uiGroup;
    [SerializeField] Button[] _uiToggleButtons;
    [SerializeField] GameObject buttonParent;


    private void Awake() // inventoryUI and craftingUI must be "Activated in Hierarchy" before the game starts.
    {
        _playerController = FindAnyObjectByType<PlayerController>();
    }


    private void Start()
    {
        // _playerController. ActionName += OnOpenUI;

        _uiToggleButtons[(int)UIEnums.Inventory].onClick.AddListener(OnOpenInventoryUI);
        _uiToggleButtons[(int)UIEnums.Combination].onClick.AddListener(OnOpenCombinationUI);
        _uiToggleButtons[(int)UIEnums.Crafting].onClick.AddListener(OnOpenCraftingUI);
        _uiToggleButtons[(int)UIEnums.Setting].onClick.AddListener(OnOpenSettingUI);

        CheckUIState();
    }


    #region UI Open & Close 
    public void OnOpenUI()  // use this in _playerInputController
    {
        if (!isUIOpen)
        {
            isUIOpen = true;
            _uiGroup[(int)UIEnums.Inventory].gameObject.SetActive(true);
            buttonParent.gameObject.SetActive(true);

        }
        else
        {
            isUIOpen = false;

            for (int i = 0; i < _uiGroup.Length; i++)
            {
                _uiGroup[i].gameObject.SetActive(false);
            }

            buttonParent.gameObject.SetActive(false);
        }
    }


    public void OnOpenInventoryUI()
    {
        for (int i = 0; i < _uiGroup.Length; i++)
        {
            if (i == (int)UIEnums.Inventory)
            {
                _uiGroup[i].gameObject.SetActive(true);
                continue;
            }

            _uiGroup[i].gameObject.SetActive(false);
        }
    }


    public void OnOpenCombinationUI()
    {
        for (int i = 0; i < _uiGroup.Length; i++)
        {
            if (i == (int)UIEnums.Combination)
            {
                _uiGroup[i].gameObject.SetActive(true);
                continue;
            }

            _uiGroup[i].gameObject.SetActive(false);
        }
    }


    public void OnOpenCraftingUI()
    {
        for (int i = 0; i < _uiGroup.Length; i++)
        {
            if (i == (int)UIEnums.Crafting)
            {
                _uiGroup[i].gameObject.SetActive(true);
                continue;
            }

            _uiGroup[i].gameObject.SetActive(false);
        }
    }


    public void OnOpenSettingUI()
    {
        for (int i = 0; i < _uiGroup.Length; i++)
        {
            if (i == (int)UIEnums.Setting)
            {
                _uiGroup[i].gameObject.SetActive(true);
                continue;
            }

            _uiGroup[i].gameObject.SetActive(false);
        }
    }
    #endregion


    private void OnPressedTabKey()
    {
        OnOpenUI();
    }

    private void CheckUIState()
    {
        if (_uiGroup[(int)UIEnums.Inventory].gameObject.activeSelf == false || _uiGroup[(int)UIEnums.Crafting].gameObject.activeSelf == false)
        {
            Debug.LogError("inventoryUI and craftingUI must be \"Activated in Hierarchy\" before the game starts.");
        }

        for (int i = 0; i < _uiGroup.Length; i++)
        {
            _uiGroup[i].gameObject.SetActive(false);
        }

        buttonParent.gameObject.SetActive(false);
    }
}