using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prologue : MonoBehaviour
{
    public Text dialogueText;                // 대화창 텍스트
    public GameObject backgroundBox;         // 대사 배경 네모 상자
    public List<string> dialogues;           // Inspector에서 설정할 대사 리스트

    private int dialogueIndex = 0;           // 현재 대화 인덱스
    private bool isTyping = false;           // 텍스트 타이핑 중인지 여부

    private void Start()
    {
        backgroundBox.SetActive(false);      // 대사 배경 상자 비활성화
        DisplayNextDialogue();               // 첫 번째 대화 출력
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼 클릭 시 다음 대화로 넘어감
        if (Input.GetMouseButtonDown(0))
        {
            DisplayNextDialogue();
        }
    }

    public void DisplayNextDialogue()
    {
        if (isTyping) return;                // 타이핑 중이면 중복 호출 방지

        if (dialogueIndex < dialogues.Count)
        {
            backgroundBox.SetActive(true);   // 대사 배경 상자 활성화
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // 타이핑 애니메이션 시작
            dialogueIndex++;                 // 다음 대화로 이동
        }
        else
        {
            EndDialogue();                   // 대화가 끝난 경우 처리
        }
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;                     // 타이핑 상태 시작
        dialogueText.text = "";              // 대화창 초기화

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;     // 텍스트에 글자 추가
            yield return new WaitForSeconds(0.05f); // 글자 타이핑 속도 조절
        }

        isTyping = false;                    // 타이핑 상태 종료
    }

    private void EndDialogue()
    {
        dialogueText.text = "";              // 대화창 내용 초기화
        backgroundBox.SetActive(false);      // 대사 배경 상자 비활성화
    }
}
