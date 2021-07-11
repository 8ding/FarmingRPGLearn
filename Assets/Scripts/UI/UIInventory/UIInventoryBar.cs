using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryBar : MonoBehaviour
{
    private RectTransform rectTransform;

    private bool isInventoryBarPositionBottom = true;
    public List<UiInventorySlot> inventorySlots;
    public bool IsInventoryBarPositionBottom
    {
        get
        {
            return isInventoryBarPositionBottom;
        }
        set
        {
            isInventoryBarPositionBottom = value;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        EventHandler.InventoryUpdateEvent += UpDateInventorySlots;
        ClearInventory();
    }

    private void OnDisable()
    {
        EventHandler.InventoryUpdateEvent -= UpDateInventorySlots;
    }

    private void Update()
    {
        SwitchInventoryBarPosition();
    }

    private void ClearInventory()
    {
        if(inventorySlots.Count > 0)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                inventorySlots[i].itemDetails = null;
                inventorySlots[i].itemQuantity = 0;
                inventorySlots[i].UpdateContent();
            }
        }
    }
    private void UpDateInventorySlots(InventoryLocation _inventoryLocation, List<InventoryItem> inventoryList)
    {
        if(_inventoryLocation == InventoryLocation.player)
        {
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if(i < inventorySlots.Count)
                {
                    inventorySlots[i].itemDetails = InventoryManager.Instance.GetItemDeatails(inventoryList[i].itemCode);
                    inventorySlots[i].itemQuantity = inventoryList[i].itemQuantity;
                    inventorySlots[i].UpdateContent();
                }
                else
                {
                    break;
                }
            }
        }
    }
    private void SwitchInventoryBarPosition()
    {
        Vector3 playerViewportPosition = Player.Instance.GetPlayerViewportPosition();
        
        if (playerViewportPosition.y > 0.3f && IsInventoryBarPositionBottom == false)
        {
            // transform.position = new Vector3(transform.position.x, 7.5f, 0f); // this was changed to control the recttransform see below
            rectTransform.pivot = new Vector2(0.5f, 0f);
            rectTransform.anchorMin = new Vector2(0.5f, 0f);
            rectTransform.anchorMax = new Vector2(0.5f, 0f);
            rectTransform.anchoredPosition = new Vector2(0f, 2.5f);

            IsInventoryBarPositionBottom = true;
        }
        else if (playerViewportPosition.y <= 0.3f && IsInventoryBarPositionBottom == true)
        {
            //transform.position = new Vector3(transform.position.x, mainCamera.pixelHeight - 120f, 0f);// this was changed to control the recttransform see below
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = new Vector2(0f, -2.5f);

            IsInventoryBarPositionBottom = false;
        }
    }
}
