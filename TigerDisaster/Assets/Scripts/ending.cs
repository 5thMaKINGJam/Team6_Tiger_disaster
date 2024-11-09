using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ending : MonoBehaviour
{
    private FadeController fadeController;
    private dialogueManager dialogueManager;
    private SceneMove sceneMove;
    private int count;
    void Start()
    {
        fadeController = FindObjectOfType<FadeController>();
        dialogueManager = FindObjectOfType<dialogueManager>();
        sceneMove = FindObjectOfType<SceneMove>();

        count = 0;

        if (fadeController != null)
        {
            fadeController.StartCoroutine(fadeController.CoFadeIn());
        }
    }
}
