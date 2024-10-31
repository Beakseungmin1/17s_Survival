using UnityEngine;


[CreateAssetMenu(fileName = "ItemBase", menuName = "ItemBase/", order = 0)]
public class ItemBase : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public ItemType itemType;
    public GameObject itemPrefabs;
    public bool isCanStack;
    public int maxStack;
}