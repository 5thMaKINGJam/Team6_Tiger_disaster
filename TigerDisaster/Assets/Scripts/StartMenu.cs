using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public SceneMove sceneMove;
    public Button continueBtn;
    public void Start()
    {
        // 이전 기록 확인 후 이어하기 버튼 활성화/비활성화
        if (PlayerPrefs.HasKey("SaveData")){
            continueBtn.interactable = true;
        }
        else{
            continueBtn.interactable = false;
        }
    }
    // 새로운 게임 시작
    public void newGame(){
        // 이전 기록 초기화 및 첫 씬으로 이동
        SaveManager.initDayAndTurn();
        sceneMove.ChangeScene();
    }

    // 이전 기록 이어서 시작
    public void continueGame(){
        int currentDay = PlayerPrefs.GetInt("currentDay");
        int currentTurn = PlayerPrefs.GetInt("currentTurn");
        Debug.Log("저장된 Day-Turn: " + currentDay + "-" + currentTurn);
        sceneMove.ChangeScene();
    }

    // 게임 종료 함수
    public void ExitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
