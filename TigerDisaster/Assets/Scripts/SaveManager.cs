using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public static class SaveManager
{
    public static void initDayAndTurn(){
        setDayAndTurn(0, 1);
        PlayerPrefs.SetInt("SaveData", 0);
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
        int day = PlayerPrefs.GetInt("CurrentDay");
        int turn = PlayerPrefs.GetInt("CurrentTurn");

        return Tuple.Create(day, turn);
    }
    public static void saveData(){
        int currentDay = PlayerPrefs.GetInt("CurrentDay");
        int currentTurn = PlayerPrefs.GetInt("CurrentTurn");
        SaveManager.setDayAndTurn(currentDay, currentTurn);
        PlayerPrefs.SetInt("SaveData", 1);
    }
}
