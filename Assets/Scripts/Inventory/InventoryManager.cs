
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    private Dictionary<int, ItemDetails> itemDeatailsDictionary;
    [SerializeField] private SO_ItemList itemList = null;
    public List<InventoryItem>[] inventoryLists;
    public int[] inventroyListCapacityIntArray;
    private int[] selectedInventoryItem;

    protected override void Awake()
    {
        base.Awake();
        CreateInventoryLists();
        CreateItemDeatailsDictionary();
        selectedInventoryItem = new int[(int) InventoryLocation.count];
        for (int i = 0; i < selectedInventoryItem.Length; i++)
        {
            selectedInventoryItem[i] = -1;
        }
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

    public int FindItemInvetory(InventoryLocation _inventoryLocation, int _itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int) _inventoryLocation];
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if(inventoryList[i].itemCode == _itemCode)
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

    public void removeItem(InventoryLocation _inventoryLocation, int _itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)_inventoryLocation];
        int itemPosition = FindItemInvetory(_inventoryLocation, _itemCode);
        if(itemPosition != -1)
        {
            RemoveItemAtPosition(inventoryList, itemPosition);
        }
        EventHandler.CallInventoryUpdatedEvent(_inventoryLocation,inventoryLists[(int)_inventoryLocation]);
    }

    private void RemoveItemAtPosition(List<InventoryItem> _inventoryItems, int _itemPosition)
    {
        if(_itemPosition < _inventoryItems.Count)
        {
            InventoryItem item = _inventoryItems[_itemPosition];
            _inventoryItems[_itemPosition] = new InventoryItem {itemCode = item.itemCode, itemQuantity = item.itemQuantity - 1};
            if(_inventoryItems[_itemPosition].itemQuantity < 1)
            {
                _inventoryItems.RemoveAt(_itemPosition);
            }
        }
    }

    public void SwitchItem(InventoryLocation _inventoryLocation, int _itemCode1, int _itemCode2)
    {
        int position1 = FindItemInvetory(_inventoryLocation, _itemCode1);
        int position2 = FindItemInvetory(_inventoryLocation, _itemCode2);
        List<InventoryItem> inventoryList = inventoryLists[(int)_inventoryLocation];
        InventoryItem temp = inventoryList[position1];
        inventoryList[position1] = inventoryList[position2];
        inventoryList[position2] = temp;
        EventHandler.CallInventoryUpdatedEvent(_inventoryLocation, inventoryLists[(int)_inventoryLocation]);
    }

    public void SetSelectedInventoryItem(InventoryLocation _inventoryLocation, int _itemCode)
    {
        selectedInventoryItem[(int) _inventoryLocation] = _itemCode;
        
    }
    public void ClearSelectedInventoryItem(InventoryLocation _inventoryLocation)
    {
        selectedInventoryItem[(int) _inventoryLocation] = -1;
    }
}
