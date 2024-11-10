using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public Text dialogueText;                // 대화창 텍스트
    public GameObject backgroundBox;         // 대사 배경 네모 상자
    public Button nextDialogueButton;        // 다음 대화로 넘어가는 버튼
    public GameObject convoPanel;
    private SceneMove sceneMove;
    private FadeController fadeController;

    private List<string> tuto = new List<string> { "으으... 여기가 어디지. 분명 난 호랑이한테 잡혀갔는데...", "얼른 산 아래로 내려가야겠다." };
    private List<string> dialogues1 = new List<string> { "어...? 분명 왔던 길 아닌가?", "이상하다..." };
    private List<string> dialogues2 = new List<string> { "뒤에서 숨결이 느껴진다."};
    private List<string> dialogues3 = new List<string> { "여기까지는 쫓아오지 못하는 것 같다..." };
    private List<string> dialogues4 = new List<string> { "절벽이다... 길을 잘못들었나?", "돌아가야겠다." };
    private List<string> dialogues5 = new List<string> { "(그냥 옆으로 지나가야겠다.)" };
    private List<string> ending = new List<string> { "옆 마을 총각이 글쎄 호랑이한테 잡혀갔대.", " 일주일이 지났는데도 안 돌아온 걸 봐서는 \n       이미 잡아먹힌게 틀림 없어!" ,"          자네도 호환 조심하게." };
    private List<string> currentDialogue;    // 현재 선택된 대화 리스트
    private int dialogueIndex = 0;           // 현재 대화 인덱스
    private bool isTyping = false;           // 텍스트 타이핑 중인지 여부

    private void Start()
    {
        sceneMove = FindObjectOfType<SceneMove>();
        fadeController = FindObjectOfType<FadeController>();
    }

    // 현재 대화 출력 메서드 - 인덱스를 증가시키지 않고 현재 대화만 표시
    public void DisplayCurrentDialogue()
    {
        if (isTyping) return;                // 타이핑 중이면 중복 호출 방지
        convoPanel.SetActive(true);
        nextDialogueButton.interactable = false;
        // 현재 대화 리스트에서 인덱스 확인
        if (dialogueIndex < currentDialogue.Count)
        {
            // backgroundBox.SetActive(true);   // 대사 배경 상자 활성화
            StartCoroutine(TypeDialogue(currentDialogue[dialogueIndex])); // 타이핑 애니메이션 시작
        }
        else
        {
            EndDialogue();                   // 대화가 끝난 경우 처리
        }
    }

    // 다음 대화 버튼 클릭 시 호출되는 메서드
    public void OnNextDialogueButtonClicked()
    {
        
        if (!isTyping)                       // 타이핑 중이 아닐 때만 인덱스를 증가
        {
            dialogueIndex++;                 // 다음 대화 인덱스로 이동
            DisplayCurrentDialogue();        // 다음 대화 출력
        }
        else{
            Debug.Log("엔드 호출");
            EndDialogue();
        }
    }

    // 대화 셋 선택에 따라 해당 리스트를 현재 대화로 설정하는 메서드
    public void SelectDialogue(int dialogueSet)
    {
        switch (dialogueSet)
        {
            case 0: currentDialogue = tuto; break;
            case 1: currentDialogue = dialogues1; break;
            case 2: currentDialogue = dialogues2; break;
            case 3: currentDialogue = dialogues3; break;
            case 4: currentDialogue = dialogues4; break;
            case 5: currentDialogue = dialogues5; break;
            case 6: currentDialogue = ending; break;
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
            if (letter != ' ')               // 현재 글자가 빈칸이 아닌 경우에만 소리 재생
            {
                AudioManager.Instance.PlaySFX("WordSound");
            }
            yield return new WaitForSeconds(0.1f); // 글자 타이핑 속도 조절
        }

        isTyping = false;                    // 타이핑 상태 종료
        nextDialogueButton.interactable = true;
        
    }

    // 대화가 끝났을 때 처리하는 메서드
    private void EndDialogue()
    {
        dialogueText.text = "";              // 대화창 내용 초기화
        // backgroundBox.SetActive(false);      // 대사 배경 상자 비활성화
        dialogueIndex = 0;                   // 대화 인덱스 초기화
        convoPanel.SetActive(false);

        //만약 엔딩 씬일경우
        if (currentDialogue == ending) {
            sceneMove.ChangeScene();
        }   
    }
}
