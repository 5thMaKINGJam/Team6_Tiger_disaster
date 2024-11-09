using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class prologue : MonoBehaviour
{
    public Text dialogueText;               // 대화창 텍스트

    private string[] dialogues = {
        "으으... 여기가 어디지. 분명 난 호랑이한테 잡혀갔는데...\n얼른 산 아래로 내려가야겠다."
    };

    private int dialogueIndex = 0;
    private bool isTyping = false;

    private void Start()
    {
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogueIndex < dialogues.Length)
        {
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
            dialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // 글자 타이핑 속도 조절
        }

        isTyping = false;
    }

    private void EndDialogue()
    {
        
        dialogueText.text = "";
    }
}
