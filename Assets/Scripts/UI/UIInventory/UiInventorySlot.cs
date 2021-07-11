using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UiInventorySlot : MonoBehaviour
{
    public Image inventorySlotHighlight;
    [SerializeField]
    private Image inventorySlotImage;
    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;
    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;
    [SerializeField] private Sprite blank16x16sprite;

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
}
