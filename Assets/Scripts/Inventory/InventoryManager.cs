
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

    public void AddItem(InventoryLocation _inventoryLocation, Item _item)
    {
        int itemCode = _item.ItemCode;
        List<InventoryItem> inventoryList = inventoryLists[(int)_inventoryLocation];
        int itemPosition = FindItemInventroy(_inventoryLocation, _item);
        if(itemPosition != -1)
        {
            AddItemAtPosition(inventoryList, itemCode, itemPosition);
        }
        else
        {
            AddItemAtPosition(inventoryList, itemCode);
        }
        EventHandler.CallInventoryUpdatedEvent(_inventoryLocation,inventoryLists[(int)_inventoryLocation]);
    }

    private void AddItemAtPosition(List<InventoryItem> _inventoryItems, int _itemCode, int _itemPosition)
    {
        if(_itemPosition < _inventoryItems.Count)
        {
            InventoryItem item = _inventoryItems[_itemPosition];
            _inventoryItems[_itemPosition] = new InventoryItem {itemCode = _itemCode, itemQuantity = item.itemQuantity + 1};
        }
        // DebugPrintInventoryList(_inventoryItems);
    }

    private void AddItemAtPosition(List<InventoryItem> _inventoryItems, int _itemCode)
    {
        _inventoryItems.Add(new InventoryItem {itemCode = _itemCode, itemQuantity = 1});
        // DebugPrintInventoryList(_inventoryItems);
    }

    // private void DebugPrintInventoryList(List<InventoryItem> _inventoryList)
    // {
    //     foreach (InventoryItem inventoryItem in _inventoryList)
    //     {
    //         Debug.Log("Item Description" + InventoryManager.Instance.GetItemDeatails(inventoryItem.itemCode).itemDescription + "    Item Quantity" +
    //                   inventoryItem.itemQuantity);
    //     }
    //     Debug.Log("***********************************************************************");
    // }
    public ItemDetails GetItemDeatails(int itemCode)
    {
        ItemDetails itemDetails;
        if(itemDeatailsDictionary.TryGetValue(itemCode,out itemDetails))
        {
            return itemDetails;
        }
        return null;
    }

    public int FindItemInventroy(InventoryLocation _inventoryLocation, Item _item)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int) _inventoryLocation];
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if(inventoryList[i].itemCode == _item.ItemCode)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddItem(InventoryLocation _inventoryLocation, Item _item, GameObject _gameObjectToDelete)
    {
        AddItem(_inventoryLocation, _item);
        Destroy(_gameObjectToDelete); 
    }
}
