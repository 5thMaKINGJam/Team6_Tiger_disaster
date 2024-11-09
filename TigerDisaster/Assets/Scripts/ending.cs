using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ending : MonoBehaviour
{
    private FadeController fadeController;
    // Start is called before the first frame update
    void Start()
    {
        // 페이드인&아웃
        fadeController = FindObjectOfType<FadeController>();
        if (fadeController != null)
        {
            // fadeController.RegisterCallback(OnFadeOutComplete); // 페이드아웃 후 진행할 액션 등록
            fadeController.FadeOut("ending"); // FadeOut 호출
        }
    }

    // Update is called once per frame
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
