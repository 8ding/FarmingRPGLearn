using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SceneItem
{
    public int itemCode;
    public Vector3Serializable position;
    public string itemName;

    public SceneItem()
    {
        position = new Vector3Serializable();
    }

    public SceneItem(Item item)
    {
        itemCode = item.ItemCode;
        position = new Vector3Serializable(item.transform.position);
        itemName = InventoryManager.Instance.GetItemDeatails(itemCode).itemDescription;
    }

    public UnityEngine.Vector3 TurnVec()
    {
        return new Vector3(position.x, position.y, position.z);
    }
}
