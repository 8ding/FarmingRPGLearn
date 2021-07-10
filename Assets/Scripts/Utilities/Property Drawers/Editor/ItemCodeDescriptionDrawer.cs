using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomPropertyDrawer(typeof(ItemCodeDescriptAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        if(property.propertyType == SerializedPropertyType.Integer)
        {
            EditorGUI.BeginChangeCheck();
            var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height * .5f), label, property.intValue);
            
            EditorGUI.LabelField(new Rect(position.x,position.y + position.height/2f,position.width,position.height*.5f),"Item DesCription",GetItemDescription(property.intValue));
            if(EditorGUI.EndChangeCheck())
            {
                property.intValue = newValue;
            }
        }
        EditorGUI.EndProperty();
    }

    private string GetItemDescription(int itemCode)
    {
        SO_ItemList itemList;
        itemList = AssetDatabase.LoadAssetAtPath("Assets/SO/so_ItemList.asset", typeof(SO_ItemList)) as SO_ItemList;
        List<ItemDetails> itemDetailsList = itemList.itemDetailsList;
        ItemDetails itemDetails = itemDetailsList.Find(_details => _details.itemCode == itemCode);

        if(itemDetails != null)
        {
            return itemDetails.itemDescription;
        }
        else
        {
            return "";
        }
    }
}
