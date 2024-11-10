using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ending : MonoBehaviour
{
    private FadeController fadeController;
    private dialogueManager dialogueManager;
    private SceneMove sceneMove;
    public GameObject[] titleImages;
    public GameObject backgroundShadow;

    private Sequence mySequence;
    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        dialogueManager = FindObjectOfType<dialogueManager>();
        sceneMove = FindObjectOfType<SceneMove>();

        if (fadeController != null)
        {
            StartCoroutine(endDialogue());
        }
    }
    IEnumerator endDialogue(){
        yield return new WaitForSeconds(3f);
        dialogueManager.SelectDialogue(6);
        dialogueManager.DisplayCurrentDialogue();
    }

    public void afterEpilogue(){
        backgroundShadow.SetActive(false);
        fadeController.panel.SetActive(false);
        titleImages[0].SetActive(true);
        titleImages[1].SetActive(true);
        typoEffect();
    }

    public void typoEffect()
    {
        float impactScale = 2.0f;   // 최대 크기 배율
        float duration = 0.3f;      // 애니메이션 시간
        float shakeDuration = 0.2f; // 진동 지속 시간
        float shakeStrength = 1.0f; // 진동 강도 (좌우 흔들림)

        // 이전 시퀀스를 정리하고 새 시퀀스 생성
        if (mySequence != null) mySequence.Kill();

        mySequence = DOTween.Sequence();
        mySequence.AppendCallback(() => 
        {
            titleImages[2].SetActive(true);
            titleImages[2].transform.localScale = Vector3.one;
            titleImages[2].transform.DOScale(impactScale, duration / 2)
                .SetEase(Ease.OutBack)
                .OnComplete(() => titleImages[2].transform.DOScale(1f, duration / 2));
            titleImages[2].transform.DOShakePosition(shakeDuration, new Vector3(shakeStrength, 0, 0), 10, 90, false, true);
            AudioManager.Instance.PlaySFX("TalGhostWalking");
        })
        .AppendInterval(1f) // 1초 동안 유지
        .AppendCallback(() => 
        {
            titleImages[3].SetActive(true);
            titleImages[3].transform.localScale = Vector3.one;
            titleImages[3].transform.DOScale(impactScale, duration / 2)
                .SetEase(Ease.OutBack)
                .OnComplete(() => titleImages[3].transform.DOScale(1f, duration / 2));
            titleImages[3].transform.DOShakePosition(shakeDuration, new Vector3(shakeStrength, 0, 0), 10, 90, false, true);
            AudioManager.Instance.PlaySFX("TalGhostWalking");
        })
        .AppendInterval(1f);

        mySequence.Restart(); // 시퀀스를 다시 시작
        StartCoroutine(afterTypoEffect());
}

    IEnumerator afterTypoEffect(){
        yield return new WaitForSeconds(3f);
        sceneMove.ChangeScene();
    }
    
}
