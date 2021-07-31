using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryTextBox : MonoBehaviour
{
    [SerializeField] private Text textMeshTop1 = null;
    [SerializeField] private Text textMeshTop2 = null;
    [SerializeField] private Text textMeshTop3 = null;
    [SerializeField] private Text textMeshBottom1 = null;
    [SerializeField] private Text textMeshBottom2 = null;
    [SerializeField] private Text textMeshBottom3 = null;



    // Set text values
    public void SetTextboxText(string textTop1, string textTop2, string textTop3, string textBottom1, string textBottom2, string textBottom3)
    {
        textMeshTop1.text = textTop1;
        textMeshTop2.text = textTop2;
        textMeshTop3.text = textTop3;
        textMeshBottom1.text = textBottom1;
        textMeshBottom2.text = textBottom2;
        textMeshBottom3.text = textBottom3;
    }

}