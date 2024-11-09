using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    public Button continueButton;
    public Button exitButton;

    public void Start()
    {
        // 이전 기록 확인 후 이어하기 버튼 활성화/비활성화
        if (PlayerPrefs.HasKey("SaveData"))
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }

        // 버튼 클릭 시 호출할 함수 등록
        startButton.onClick.AddListener(StartNewGame);
        continueButton.onClick.AddListener(ContinueGame);
        exitButton.onClick.AddListener(ExitGame); // 나가기 버튼에 함수 연결
    }

    // 새로운 게임 시작
    public void StartNewGame()
    {
        // 이전 기록 초기화 및 첫 씬으로 이동
        PlayerPrefs.DeleteKey("SaveData"); // 기존 기록 초기화
        LoadGameScene();
    }

    // 이전 기록 이어서 시작
    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            LoadGameScene();
        }
    }

    // 게임 씬 로드 함수
    public void LoadGameScene()
    {
        // 여기서 "GameScene"은 로드할 게임 씬의 이름입니다.
        SceneManager.LoadScene("stage");
    }

    // 게임 종료 함수
    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
