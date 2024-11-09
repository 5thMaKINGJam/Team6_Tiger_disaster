using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public static class SaveManager
{
    public static void initDayAndTurn(){
        PlayerPrefs.SetInt("CurrentDay", 0);
        PlayerPrefs.SetInt("CurrentTurn", 1);
    }
    public static void setDayAndTurn(int currentDay, int currentTurn)
    {
        // PlayerPrefs에 현재 일차와 턴 저장
        Debug.Log("현재 일차-현재 턴: " + currentDay + "-" + currentTurn);
        PlayerPrefs.SetInt("CurrentDay", currentDay);
        PlayerPrefs.SetInt("CurrentTurn", currentTurn);
        
    }

    public static Tuple<int, int> getDayAndTurn()
    {
        // PlayerPrefs에 값이 없으면 기본값 (0, 1)을 반환
        int day = PlayerPrefs.GetInt("CurrentDay");
        int turn = PlayerPrefs.GetInt("CurrentTurn");

        return Tuple.Create(day, turn);
    }
}
