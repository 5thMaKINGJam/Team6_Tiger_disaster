using System;
using System.Collections;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public bool isFadeIn; // true=FadeIn, false=FadeOut
    public GameObject panel; // Panel 오브젝트 Inspector에서 할당
    private Action onCompleteCallback; // FadeIn 또는 FadeOut 다음에 진행할 함수
    private bool isFadingIn = false; // 페이드인이 한 번만 실행되도록 제어하는 변수

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않게 설정
    }

    void Start()
    {
        if (!panel)
        {
            Debug.LogError("Panel 오브젝트를 찾을 수 없습니다.");
            throw new MissingComponentException();
        }

        if (isFadeIn) // Fade In Mode -> 바로 코루틴 시작
        {
            panel.SetActive(true); // Panel 활성화
            StartCoroutine(CoFadeIn());
        }
        else
        {
            panel.SetActive(false); // Panel 비활성화
        }
    }

    public void FadeOut()
    {
        Debug.Log("FadeCanvasController_ Fade Out 시작");
        panel.SetActive(true); // Panel 활성화
        StartCoroutine(CoFadeOut());
    }

    public void FadeIn()
    {
        // 이미 페이드인이 실행 중이라면 중복 실행을 막음
        if (isFadingIn) return;
        
        Debug.Log("FadeCanvasController_ Fade In 시작");
        isFadingIn = true; // 페이드인이 실행 중임을 표시
        panel.SetActive(true); // Panel 활성화
        panel.GetComponent<CanvasRenderer>().SetAlpha(0f); // 알파 값을 0으로 초기화하여 투명하게 시작
        StartCoroutine(CoFadeIn());
    }

    IEnumerator CoFadeIn()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadedTime = 5f; // 총 소요 시간

        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, elapsedTime / fadedTime));
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        Debug.Log("Fade In 끝");
        panel.SetActive(false); // Panel을 비활성화
        isFadingIn = false; // 페이드인이 완료되었음을 표시
        onCompleteCallback?.Invoke(); // 이후에 해야 하는 다른 액션이 있는 경우(null이 아님) 진행한다
        yield break;
    }

    IEnumerator CoFadeOut()
    {
        float elapsedTime = 0f; // 누적 경과 시간
        float fadedTime = 5f; // 총 소요 시간

        while (elapsedTime <= fadedTime)
        {
            panel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime / fadedTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Fade Out 끝");
        onCompleteCallback?.Invoke(); // 이후에 해야 하는 다른 액션이 있는 경우(null이 아님) 진행한다
        yield break;
    }

    public void RegisterCallback(Action callback) // 다른 스크립트에서 콜백 액션 등록하기 위해 사용
    {
        onCompleteCallback = callback;
    }
}
