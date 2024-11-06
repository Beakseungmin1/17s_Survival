using UnityEngine;


public class Build : MonoBehaviour
{
    [Header("Building Item Informaton")]
    private ItemInfomation _itemInfomation;
    private BuildingItemSO buildingItemSO;

    [Header("need ingredient")]
    [SerializeField] private ItemSO needItem;
    [SerializeField] private int needCount;

    private int currentItemCount;


    private void Awake()
    {
        _itemInfomation = GetComponent<ItemInfomation>();

        buildingItemSO = (BuildingItemSO)_itemInfomation.item; // ??
    }


    public bool CheckNeedItem(ItemSO item)
    {
        if (needItem == item)
        {
            return true;
        }
        Debug.Log("Current : " + item + "Need" + needItem);
        return false;
    }


    public void AddIngredientCount(ItemSO item, int quntity = 1)
    {
        if (item == needItem)
        {
            currentItemCount++;
        }

        CheckQuntity();
    }


    private void CheckQuntity()
    {
        if (needCount == currentItemCount)
        {
            AudioManager.Instance.PlaySFX(PlayerSFX.Run1);
            BuildArchitecture();
        }
    }


    private void BuildArchitecture()
    {
        Instantiate(buildingItemSO.itemPrefabs, transform.position, Quaternion.identity);
        Destroy(gameObject); // destroy preview building
    }
}