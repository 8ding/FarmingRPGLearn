using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNudge : MonoBehaviour
{
    private WaitForSeconds pause;
    private bool isAnimating = false;

    private void Awake()
    {
        pause = new WaitForSeconds(0.04f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isAnimating == false)
        {
            if(gameObject.transform.position.x < other.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(isAnimating == false)
        {
            if(gameObject.transform.position.x > other.gameObject.transform.position.x)
            {
                StartCoroutine(RotateAntiClock());
            }
            else
            {
                StartCoroutine(RotateClock());
            }
        }
    }

    private IEnumerator RotateAntiClock()
    {
        isAnimating = true;
        for (int i = 0; i < 8; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        for (int i = 0; i < 12; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        for (int i = 0; i <7; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        for (int i = 0; i <3; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        gameObject.transform.GetChild(0).Rotate(0, 0, -2);
        yield return pause;
        isAnimating = false;
    }

    private IEnumerator RotateClock()
    {
        isAnimating = true;
        for (int i = 0; i < 8; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        for (int i = 0; i < 12; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        for (int i = 0; i <7; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        for (int i = 0; i < 5; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, 2f);
            yield return pause;
        }
        for (int i = 0; i <3; i++)
        {
            gameObject.transform.GetChild(0).Rotate(0f, 0f, -2f);
            yield return pause;
        }
        gameObject.transform.GetChild(0).Rotate(0, 0, 2);
        yield return pause;
        isAnimating = false;
    }
    
}
