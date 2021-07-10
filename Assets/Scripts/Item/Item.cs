using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescript]
    [SerializeField]
    private int _itemCode;
    private SpriteRenderer spriteRenderer;

    public int ItemCode
    {
        get
        {
            return _itemCode;
        }
        set
        {
            _itemCode = value;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        if(_itemCode != 0)
        {
            Init(ItemCode);
        }
    }

    public void Init(int itemCodeParam)
    {
        if (itemCodeParam != 0)
        {
            ItemCode = itemCodeParam;

            ItemDetails itemDetails = InventoryManager.Instance.GetItemDeatails(ItemCode);

            spriteRenderer.sprite = itemDetails.itemSprite;

            // If item type is reapable then add nudgeable component
            if (itemDetails.itemType == ItemType.Reapable_Scenary)
            {
                gameObject.AddComponent<ItemNudge>();
            }
        }
    }
    
}
