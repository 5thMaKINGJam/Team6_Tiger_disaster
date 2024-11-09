using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ending : MonoBehaviour
{
    private FadeController fadeController;
    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        if (fadeController != null)
        {
            fadeController.StartCoroutine(fadeController.CoFadeIn());
        }
    }

    void Update()
    {
        
    }

    // void OnFadeOutComplete()
    // {
    //     Debug.Log("Fade Out이 완료되어 씬을 이동합니다.");
    //     SceneManager.LoadScene(targetScene); //해당 씬으로 이동
    // }

    // public void ChangeSceneAfterPause()
    // {
    //     Time.timeScale = 1f;

    //     ChangeScene();
    // }
}
