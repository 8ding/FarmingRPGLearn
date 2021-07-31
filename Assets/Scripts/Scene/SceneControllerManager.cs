using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerManager : SingletonMonoBehavior<SceneControllerManager>
{
    private bool isFading;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private CanvasGroup faderCanvasGroup = null;
    [SerializeField] private Image fadeImage = null;
    private Dictionary<string, Dictionary<string,Vector3>> SceneExitMatchPosition;
    public SceneName startingSceneName;
    public void FadeAndLoadScene(string sceneName, Vector3 spawnPosition)
    {
        if(!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName, spawnPosition));
        }
    }

    IEnumerator FadeAndSwitchScenes(string sceneName, Vector3 spawnPosition)
    {
        EventHandler.CallBeforeSceneUnloadFadeOutEvent();
        
        yield return StartCoroutine(Fade(1f));

        Player.Instance.gameObject.transform.position = spawnPosition;
        
        EventHandler.CallBeforeSceneUnloadEvent();
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        
        EventHandler.CallAfterSceneLoadEvent();

        yield return StartCoroutine(Fade(0f));
        
        EventHandler.CallAfterSceneLoadFadeInEvent();

    }

    private IEnumerator Start()
    {
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
        faderCanvasGroup.alpha = 1f;
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName.ToString()));
        
        EventHandler.CallAfterSceneLoadEvent();

        StartCoroutine(Fade(0f));
        
    }

    IEnumerator Fade(float finalAlpha)
    {
        isFading = true;

        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

            yield return null;
        }
        isFading = false;

        faderCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene NewScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(NewScene);
    }
}
