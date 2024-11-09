using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public bool isFadeIn; // true=FadeIn, false=FadeOut
    public GameObject panel; 
    private Action onCompleteCallback;

    void Start()
    {
        if (isFadeIn)
        {
            panel.SetActive(true);
            StartCoroutine(CoFadeIn());
        }
    }

    public void FadeOut(string nextSceneName)
    {
        panel.SetActive(true);
        RegisterCallback(() => LoadNextScene(nextSceneName));
        StartCoroutine(CoFadeOut());
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f;
        float fadedTime = 3f;

        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        panel.SetActive(false);
        onCompleteCallback?.Invoke();
    }

    IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f;
        float fadedTime = 5f;

        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        onCompleteCallback?.Invoke();
    }

    public void RegisterCallback(Action callback)
    {
        onCompleteCallback = callback;
    }

    private void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
