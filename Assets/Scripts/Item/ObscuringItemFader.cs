using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObscuringItemFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        float curretAlpha = spriteRenderer.color.a;
        float t = 0f;
        while (1-curretAlpha > 0.01f)
        {
            curretAlpha = Mathf.Lerp(curretAlpha, 1f, t += (Time.deltaTime / Settings.fadeInSeconds));
            spriteRenderer.color = new Color(1f, 1f, 1f, curretAlpha);
            yield return null;
        }
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        float curretAlpha = spriteRenderer.color.a;
        float t = 0;
        while (curretAlpha - Settings.targetAlpha > 0.01f)
        {
            curretAlpha = Mathf.Lerp(curretAlpha, Settings.targetAlpha, t += (Time.deltaTime / Settings.fadeOutSeconds));
            spriteRenderer.color = new Color(1f, 1f, 1f, curretAlpha);
            yield return null;
        }
        spriteRenderer.color = new Color(1f, 1f, 1f, Settings.targetAlpha);
    }
}
