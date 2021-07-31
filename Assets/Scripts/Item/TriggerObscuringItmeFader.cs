using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObscuringItmeFader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        ObscuringItemFader[] obscuringItemFaders = other.GetComponentsInChildren<ObscuringItemFader>();
        if(obscuringItemFaders != null && obscuringItemFaders.Length > 0)
        {
            for (int i = 0; i < obscuringItemFaders.Length; i++)
            {
                obscuringItemFaders[i].FadeOut();
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        ObscuringItemFader[] obscuringItemFaders = other.GetComponentsInChildren<ObscuringItemFader>();
        if(obscuringItemFaders != null && obscuringItemFaders.Length > 0)
        {
            for (int i = 0; i < obscuringItemFaders.Length; i++)
            {
                obscuringItemFaders[i].FadeIn();
            }
        }
    }
    
}
