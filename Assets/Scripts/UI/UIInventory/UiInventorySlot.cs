using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Quaternion = System.Numerics.Quaternion;

public class UiInventorySlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    private Camera mainCamera;
    private Transform parentItem;
    private GameObject draggedItem;
    private Canvas parentCanvas;
    [SerializeField]private int slotNumber;
    [SerializeField] public bool IsSelected;
    [SerializeField] private UIInventoryBar inventoryBar = null;
    [SerializeField] private GameObject itemPrefab = null;
    [SerializeField] private GameObject inventoryTextBoxPrefab = null;
    public Image inventorySlotHighlight;
    [SerializeField]
    private Image inventorySlotImage;
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;
    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;
    [SerializeField] private Sprite blank16x16sprite;

    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadEvent += SceneLoaded;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneLoadEvent -= SceneLoaded;
    }

    private void SceneLoaded()
    {
        parentItem = GameObject.FindGameObjectWithTag(Tags.ItemParentTransform).transform;
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    
    public void UpdateContent()
    {
        if(itemQuantity == 0)
            textMeshProUGUI.text = "";
        else
        {
            textMeshProUGUI.text = itemQuantity.ToString();
        }
        if(itemDetails != null)
            inventorySlotImage.sprite = itemDetails.itemSprite;
        else
        {
            inventorySlotImage.sprite = blank16x16sprite;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemDetails != null)
        {
            Player.Instance.DisableInputAndResetMovement();
            draggedItem = Instantiate(inventoryBar.inventoryBarDraggedItem, inventoryBar.transform);
            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = itemDetails.itemSprite;
            if(IsSelected == false)
            {
                IsSelected = true;
                SetSelectItem();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(draggedItem != null)
        {
            Destroy(draggedItem);

            if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UiInventorySlot>() != null)
            {
                UiInventorySlot SwitchSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<UiInventorySlot>();
                if(SwitchSlot != this && SwitchSlot.itemDetails != null)
                {
                    InventoryManager.Instance.SwitchItem(InventoryLocation.player,itemDetails.itemCode,
                        eventData.pointerCurrentRaycast.gameObject.GetComponent<UiInventorySlot>().itemDetails.itemCode);
                } 
                DisableTextBox();
                ClearSelectItem();
            }
            else
            {
                if(itemDetails.canBeDropped)
                {
                    if(itemQuantity <= 1)
                    {
                        ClearSelectItem();
                    }
                    DropSelectItmeAtMousePosition();
                }
            }
            Player.Instance.EnablePlayerInput();
        }
    }

    private void DropSelectItmeAtMousePosition()
    {
        GameObject item = Instantiate(itemPrefab, mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            0 - mainCamera.transform.position.z)), UnityEngine.Quaternion.identity,parentItem);
        item.GetComponent<Item>().Init(itemDetails.itemCode);
        InventoryManager.Instance.removeItem(InventoryLocation.player,itemDetails.itemCode);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(itemQuantity != 0)
        {
            if(inventoryBar.inventoryTextBoxGameObject == null)
                inventoryBar.inventoryTextBoxGameObject = Instantiate(inventoryTextBoxPrefab, transform.position, quaternion.identity);
            else
            {
                inventoryBar.inventoryTextBoxGameObject.SetActive(true);
            }
            inventoryBar.inventoryTextBoxGameObject.transform.SetParent(parentCanvas.transform, false);

            String description = itemDetails.itemDescription;

            UIInventoryTextBox inventoryTextBox = inventoryBar.inventoryTextBoxGameObject.GetComponent<UIInventoryTextBox>();
            inventoryTextBox.SetTextboxText(description, description, "", itemDetails.itemLongDescription, "", "");
            if(inventoryBar.IsInventoryBarPositionBottom)
            {
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);
                inventoryBar.inventoryTextBoxGameObject.transform.position =
                    new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
            }
            else
            {
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);
                inventoryBar.inventoryTextBoxGameObject.transform.position =
                    new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisableTextBox();
    }

    private void DisableTextBox()
    {
        if(inventoryBar.inventoryTextBoxGameObject != null)
            inventoryBar.inventoryTextBoxGameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(IsSelected == true)
                ClearSelectItem();
            else
            {
                if(itemDetails != null && itemQuantity != 0)
                {
                    SetSelectItem();
                }
            }
        }
    }

    public void ClearSelectItem()
    {
        inventoryBar.ClearHighlightSlot();

        InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.player);
        Player.Instance.HidedCarriedItem();
    }

    public void SetSelectItem()
    {
        ClearSelectItem();
        IsSelected = true;
        inventoryBar.SetHighlightSlot();
        InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.player, itemDetails.itemCode);
        if(itemDetails.canBeCarried)
        {
            Player.Instance.ShowedCarriedItem(itemDetails.itemCode);
        }
        else
        {
            Player.Instance.HidedCarriedItem();
        }
    }
}
