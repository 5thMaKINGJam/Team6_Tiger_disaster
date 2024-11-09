using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public Text dialogueText;                // 대화창 텍스트
    public GameObject backgroundBox;         // 대사 배경 네모 상자
    public Button nextDialogueButton;        // 다음 대화로 넘어가는 버튼

    private List<string> tuto = new List<string> { "1", "2" };
    private List<string> dialogues1 = new List<string> { "2", "3" };
    private List<string> dialogues2 = new List<string> { "4", "5" };
    private List<string> dialogues3 = new List<string>(); // 빈 리스트 예시
    private List<string> dialogues4 = new List<string>(); // 빈 리스트 예시
    private List<string> ending = new List<string>();

    private List<string> currentDialogue;    // 현재 선택된 대화 리스트
    private int dialogueIndex = 0;           // 현재 대화 인덱스
    private bool isTyping = false;           // 텍스트 타이핑 중인지 여부

    private void Start()
    {
        // 초기 시작 시 대화 셋 2를 표시
        SelectDialogue(2);
        DisplayCurrentDialogue();

        // 버튼 클릭 시 다음 대화로 넘어가도록 이벤트 추가
        nextDialogueButton.onClick.AddListener(OnNextDialogueButtonClicked);
    }

    // 현재 대화 출력 메서드 - 인덱스를 증가시키지 않고 현재 대화만 표시
    private void DisplayCurrentDialogue()
    {
        if (isTyping) return;                // 타이핑 중이면 중복 호출 방지

        // 현재 대화 리스트에서 인덱스 확인
        if (dialogueIndex < currentDialogue.Count)
        {
            backgroundBox.SetActive(true);   // 대사 배경 상자 활성화
            StartCoroutine(TypeDialogue(currentDialogue[dialogueIndex])); // 타이핑 애니메이션 시작
        }
        else
        {
            EndDialogue();                   // 대화가 끝난 경우 처리
        }
    }

    // 다음 대화 버튼 클릭 시 호출되는 메서드
    private void OnNextDialogueButtonClicked()
    {
        if (!isTyping)                       // 타이핑 중이 아닐 때만 인덱스를 증가
        {
            dialogueIndex++;                 // 다음 대화 인덱스로 이동
            DisplayCurrentDialogue();        // 다음 대화 출력
        }
    }

    // 대화 셋 선택에 따라 해당 리스트를 현재 대화로 설정하는 메서드
    private void SelectDialogue(int dialogueSet)
    {
        switch (dialogueSet)
        {
            case 0: currentDialogue = tuto; break;
            case 1: currentDialogue = dialogues1; break;
            case 2: currentDialogue = dialogues2; break;
            case 3: currentDialogue = dialogues3; break;
            case 4: currentDialogue = dialogues4; break;
            case 5: currentDialogue = ending; break;
            default: currentDialogue = tuto; break;
        }
        dialogueIndex = 0; // 새로운 대화 리스트로 시작할 때 인덱스를 초기화
    }

    // 텍스트를 한 글자씩 출력하는 코루틴
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

    // 대화가 끝났을 때 처리하는 메서드
    private void EndDialogue()
    {
        dialogueText.text = "";              // 대화창 내용 초기화
        backgroundBox.SetActive(false);      // 대사 배경 상자 비활성화
        dialogueIndex = 0;                   // 대화 인덱스 초기화
    }
}
