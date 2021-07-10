
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_ItemList",menuName = "Scriptalbe Objects/Item/Item List")]
public class SO_ItemList : ScriptableObject
{
    [SerializeField]
    public List<ItemDetails> itemDetailsList;
}
