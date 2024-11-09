using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour // Make sure 'public' is only applied to the class and its members
{
    public string targetScene; // Valid 'public' usage on a field
    private FadeController fadeController;

    public void ChangeScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentSceneName);
        Debug.Log("이전 씬: " + PlayerPrefs.GetString("PreviousScene"));

        fadeController = FindObjectOfType<FadeController>();
        if (fadeController != null)
        {
            fadeController.RegisterCallback(OnFadeOutComplete); // Register callback for after FadeOut completes
            fadeController.FadeOut(); // Pass targetScene as the argument
        }
    }

    void OnFadeOutComplete()
    {
        Debug.Log("Fade Out이 완료되어 씬을 이동합니다.");
        SceneManager.LoadScene(targetScene); // Move to the target scene
    }
}
