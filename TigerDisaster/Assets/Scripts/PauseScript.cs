using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool IsPaused = false;
    
    void Start(){
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !EventManager.isInEvent){
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void QuitGame(){

        //지금까지의 Day, Turn 저장
        SaveManager.saveData();
        Time.timeScale = 1f; // 게임 중지 해제
        SceneManager.LoadScene("startMenu");
    }
}
