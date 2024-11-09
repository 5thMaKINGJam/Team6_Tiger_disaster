using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            fadeController.RegisterCallback(OnFadeOutComplete); // 페이드아웃 후 진행할 액션 등록
            fadeController.FadeOut(); // FadeOut 호출
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
