
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    private Dictionary<int, ItemDetails> itemDeatailsDictionary;
    [SerializeField] private SO_ItemList itemList = null;

    protected override void Awake()
    {
        base.Awake();
        CreateItemDeatailsDictionary();
    }

    private void Start()
    {
    }
    /// <summary>
    /// 使用Scriptable objec 填充字典
    /// </summary>
    private void CreateItemDeatailsDictionary()
    {
        if(itemDeatailsDictionary == null)
        {
            itemDeatailsDictionary = new Dictionary<int, ItemDetails>();

            foreach (var itemDeatails in itemList.itemDetailsList)
            {
                itemDeatailsDictionary.Add(itemDeatails.itemCode, itemDeatails);
            }
        }
    }

    public ItemDetails GetItemDeatails(int itemCode)
    {
        ItemDetails itemDetails;
        if(itemDeatailsDictionary.TryGetValue(itemCode,out itemDetails))
        {
            return itemDetails;
        }
        return null;
    }
}
