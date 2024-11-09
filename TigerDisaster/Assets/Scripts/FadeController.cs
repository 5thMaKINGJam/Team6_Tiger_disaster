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
        // RegisterCallback(() => StartCoroutine(LoadSceneWithFadeIn(nextSceneName)));
        StartCoroutine(CoFadeOut());
    }

    private IEnumerator LoadSceneWithFadeIn(string sceneName)
    {
        // 씬 로드 후 FadeIn을 진행합니다.
        SceneManager.LoadScene(sceneName);
        yield return null; // 한 프레임 대기하여 씬이 완전히 로드되도록 합니다.

        StartCoroutine(CoFadeIn());
    }

    public IEnumerator CoFadeIn()
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

    public IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f;
        float fadedTime = 3f;

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
}
