
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    private Dictionary<int, ItemDetails> itemDeatailsDictionary;
    [SerializeField] private SO_ItemList itemList = null;
    public List<InventoryItem>[] inventoryLists;
    public int[] inventroyListCapacityIntArray;
    

    protected override void Awake()
    {
        base.Awake();
        CreateInventoryLists();
        CreateItemDeatailsDictionary();
    }

    private void CreateInventoryLists()
    {
        inventoryLists = new List<InventoryItem>[(int) InventoryLocation.count];
        for (int i = 0; i < (int) InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }
        inventroyListCapacityIntArray = new int[(int) InventoryLocation.count];
        inventroyListCapacityIntArray[(int) InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
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
