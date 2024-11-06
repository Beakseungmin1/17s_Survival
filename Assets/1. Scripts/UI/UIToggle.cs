using System;
using UnityEngine;
using UnityEngine.UI;


public class UIToggle : MonoBehaviour
{
    [Tooltip("if true, attck/build/interaction is impossible")]
    public static bool isUIOpen = false;

    private PlayerController _playerController;

    [Header("UI Group")]
    [SerializeField] Image[] _uiGroup;
    [SerializeField] Button[] _uiToggleButtons;
    [SerializeField] GameObject _buttonParent;


    private void Awake() // inventoryUI and craftingUI must be "Activated in Hierarchy" before the game starts.
    {
        _playerController = FindAnyObjectByType<PlayerController>();
    }


    private void Start()
    {
        _playerController.inventory += OnPressedTabKey;

        _uiToggleButtons[(int)UIEnums.Inventory].onClick.AddListener(OnOpenInventoryUI);
        _uiToggleButtons[(int)UIEnums.Combination].onClick.AddListener(OnOpenCombinationUI);
        _uiToggleButtons[(int)UIEnums.Crafting].onClick.AddListener(OnOpenCraftingUI);
        _uiToggleButtons[(int)UIEnums.Setting].onClick.AddListener(OnOpenSettingUI);

        CheckUIState();
    }


    private void OnPressedTabKey()
    {
        OnOpenUI();
    }


    #region UI Open & Close 
    public void OnOpenUI()
    {
        AudioManager.Instance.PlaySFX(UISFX.CloseUI);

        if (!isUIOpen)
        {
            isUIOpen = true;

            _uiGroup[(int)UIEnums.Inventory].gameObject.SetActive(true);
            _buttonParent.gameObject.SetActive(true);
        }
        else
        {
            isUIOpen = false;

            for (int i = 0; i < _uiGroup.Length; i++)
            {
                _uiGroup[i].gameObject.SetActive(false);
            }

            _buttonParent.gameObject.SetActive(false);
        }
    }


    public void OnOpenInventoryUI()
    {
        AudioManager.Instance.PlaySFX(UISFX.CloseUI);

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
        AudioManager.Instance.PlaySFX(UISFX.CloseUI);

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
        AudioManager.Instance.PlaySFX(UISFX.CloseUI);

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
        AudioManager.Instance.PlaySFX(UISFX.CloseUI);

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




    private void CheckUIState()
    {
        if (_uiGroup[(int)UIEnums.Inventory].gameObject.activeSelf == false || _uiGroup[(int)UIEnums.Crafting].gameObject.activeSelf == false)
        {
            Debug.LogError("inventoryUI and craftingUI must be \"activated in hierarchy\" before the game starts.");
        }

        for (int i = 0; i < _uiGroup.Length; i++)
        {
            _uiGroup[i].gameObject.SetActive(false);
        }

        _buttonParent.gameObject.SetActive(false);
    }
}