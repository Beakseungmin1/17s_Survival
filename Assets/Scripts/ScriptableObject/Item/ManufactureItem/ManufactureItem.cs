using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Manufacture_", menuName = "ItemBase/ManufactureItem", order = 6)]
public class ManufactureItem : ItemBase
{
    public RequireResourceItem[] requireItem;
}