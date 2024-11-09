using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ending : MonoBehaviour
{
    public Text dialogueText;               // 대화창 텍스트

    private string[] dialogues = {
        "엔딩 대사"
    };                                      // 대화 내용 저장 배열

    private int dialogueIndex = 0;          // 현재 대화 인덱스
    private bool isTyping = false;          // 텍스트 타이핑 중인지 여부

    private void Start()
    {
        DisplayNextDialogue();              // 첫 번째 대화 출력
    }

    public void DisplayNextDialogue()
    {
        // 타이핑 중이라면 중복 호출 방지
        if (isTyping) return;

        // 대화 배열 내에 출력할 대화가 남아있는 경우
        if (dialogueIndex < dialogues.Length)
        {
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // 타이핑 애니메이션 시작
            dialogueIndex++;                                        // 다음 대화로 이동
        }
        else
        {
            EndDialogue();               // 대화가 끝난 경우 처리
        }
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;                  // 타이핑 상태 시작
        dialogueText.text = "";           // 대화창 초기화

        // 한 글자씩 출력
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;  // 텍스트에 글자 추가
            yield return new WaitForSeconds(0.05f); // 글자 타이핑 속도 조절
        }

        isTyping = false;                 // 타이핑 상태 종료
    }

    private void EndDialogue()
    {
        dialogueText.text = "";           // 대화창 내용 초기화
    }
}
