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
    
}
