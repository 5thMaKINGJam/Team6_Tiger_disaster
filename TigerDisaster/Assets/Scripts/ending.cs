using UnityEngine;

public class Ending : MonoBehaviour
{
    public FadeController fadeController;

    void Start()
    {
        if (fadeController == null)
        {
            Debug.LogError("FadeController component not assigned.");
            return;
        }

        // Register the OnFadeOutComplete method as the callback for fade out completion
        fadeController.RegisterCallback(OnFadeOutComplete);
        
        // Start the fade out process
        fadeController.FadeOut();
    }

    private void OnFadeOutComplete()
    {
        Debug.Log("Fade-out is complete.");
        
        // Start fade in after fade out is complete
        fadeController.FadeIn();
    }
}
