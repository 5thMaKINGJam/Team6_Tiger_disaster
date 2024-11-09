using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public string targetScene;
    private FadeController fadeController;
    private bool hasFadedIn = false; // 페이드인이 한 번만 실행되도록 제어하는 변수

    public void ChangeScene()
    {
        // 현재 씬 이름을 PlayerPrefs에 저장
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentSceneName);
        Debug.Log("이전 씬: " + PlayerPrefs.GetString("PreviousScene"));

        // 페이드인&아웃
        fadeController = FindObjectOfType<FadeController>();
        if (fadeController != null)
        {
            fadeController.RegisterCallback(OnFadeOutComplete); // 페이드아웃 후 진행할 액션 등록
            fadeController.FadeOut(); // FadeOut 호출
        }
    }

    void OnFadeOutComplete()
    {
        Debug.Log("Fade Out이 완료되어 씬을 이동합니다.");
        SceneManager.LoadScene(targetScene); // 해당 씬으로 이동
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드된 후 페이드인이 한 번만 실행되도록 설정
        if (fadeController != null && !hasFadedIn)
        {
            fadeController.FadeIn();
            hasFadedIn = true; // 페이드인이 한 번만 실행되도록 설정
        }
    }

    void OnEnable()
    {
        hasFadedIn = false; // 초기화하여 새 씬에서 다시 한 번 실행 가능하도록 설정
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때 OnSceneLoaded 호출
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 언로드 시 이벤트 해제
    }
}
