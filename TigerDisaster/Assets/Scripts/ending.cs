using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ending : MonoBehaviour
{
    private FadeController fadeController;
    private dialogueManager dialogueManager;
    private SceneMove sceneMove;
    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        dialogueManager = FindObjectOfType<dialogueManager>();
        sceneMove = FindObjectOfType<SceneMove>();

        if (fadeController != null)
        {
            fadeController.StartCoroutine(fadeController.CoFadeIn());
            StartCoroutine(end());
        }
    }

    IEnumerator end(){
        yield return new WaitForSeconds(3f); //페이드아웃 지속 시간 3초
        dialogueManager.SelectDialogue(6);
        dialogueManager.DisplayCurrentDialogue();
        yield return new WaitForSeconds(5f); //대화 나오는 시간
        fadeController.StartCoroutine(fadeController.CoFadeOut());
        yield return new WaitForSeconds(5f); //페이드인 지속 시간 (3초) + 2초 대기 후 시작메뉴로 이동
        SaveManager.initDayAndTurn();
        sceneMove.ChangeScene();
    }
}
