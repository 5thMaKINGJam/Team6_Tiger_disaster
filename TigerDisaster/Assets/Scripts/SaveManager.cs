using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SaveManager : MonoBehaviour
{
    private static int currentDay;
    private static int currentTurn;
    private const int turnsPerDay = 15;

    void Start()
    {
        // 현재 일차와 턴을 불러오기. 저장된 값이 없다면 초기값으로 설정
        currentDay = PlayerPrefs.GetInt("CurrentDay", 0);
        currentTurn = PlayerPrefs.GetInt("CurrentTurn", 1);

        //임시 초기화 용
        currentTurn = 0;
        currentDay = 0;
    }

    public static void setDayAndTurn()
    {
        currentTurn++;
        if (currentTurn >= turnsPerDay)
        {
            currentTurn = 0;
            currentDay++;
        }
        // PlayerPrefs에 현재 일차와 턴 저장
        Debug.Log("현재 일차-현재 턴: " + currentDay + "-" + currentTurn);
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetInt("CurrentTurn", currentTurn);
        
    }

    public static Tuple<int, int> getDayAndTurn()
    {   
        int day = PlayerPrefs.GetInt("CurrentDay");
        int turn = PlayerPrefs.GetInt("CurrentTurn");
        return Tuple.Create(day, turn);
    }
}
