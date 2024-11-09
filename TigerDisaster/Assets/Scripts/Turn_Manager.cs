using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Turn_Manager : MonoBehaviour
{
    private int currentnum = 0;
    private int currentday = 0;
    public Button button;
    public GameObject circularImageObject;
    public GameObject bg;
    public GameObject RedDoor0;
    public GameObject RedDoor1;
    private SpriteRenderer bgSpriter;
    public Sprite[] BackGroundSprite;
    public Image circularImage;
    private float duration = 0.5f;  // 슬라이더가 1에서 0으로 줄어드는 시간
    public GameObject[] Ghosts;

    
    private EventManager eventManager;

    // public List<GameObject> panels;
    private List<int> currentDayList;
    private List<int> day0 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 0, 0, 0, 0 };
    private List<int> day1 = new List<int> { 0, 0, 0,0,0,0,0,0 ,0,1,2,0,0,0,0};
    private List<int> day2 = new List<int> { 6, 6, 4,5,6,6,6,3,6,6,4 };
    private List<int> day3 = new List<int> { 0, 0, 0, 0, 0, 6, 6, 3, 6, 6, 4 };






    void Awake()
    {

        eventManager = FindObjectOfType<EventManager>();
        bgSpriter = bg.GetComponent<SpriteRenderer>();
        //get오늘 turn 


    }






    public void ShowPanel()
    {
        AppearImage();

        SaveManager.setDayAndTurn(currentday, currentnum);
        if (currentday == 0)
        {
            currentDayList = day0;
        }
        else if (currentday == 1)
        {
            currentDayList = day1;
        }
        else if (currentday == 2)
        {
            currentDayList = day2;
        }
        else
        {
            Debug.LogError("Invalid currentday value.");
            return;
        }

        if (currentnum == 0 && currentDayList != day0)
        {
            RedDoor1.SetActive(false);
            RedDoor0.SetActive(true);
        }
        else if (currentDayList == day0 && currentnum == 1) {
            RedDoor1.SetActive(false);
            RedDoor0.SetActive(true);
        }
        else if (currentnum == currentDayList.Count - 1)
        {
            RedDoor0.SetActive(false);
            RedDoor1.SetActive(true);
        }
        else
        {
            RedDoor0.SetActive(false);
            RedDoor1.SetActive(false);
        }

        if (currentDayList[currentnum] == 0)  //기본
        {
            bgSpriter.sprite = bgSpriter.sprite == BackGroundSprite[0] ? BackGroundSprite[1] : BackGroundSprite[0];
        }
        else if (currentDayList[currentnum] == 1) //꺽인길 1
        {
            bgSpriter.sprite = BackGroundSprite[2];
        }
        else if (currentDayList[currentnum] == 2) //꺽인길 2
        {
            bgSpriter.sprite = BackGroundSprite[3];
        }
        else if (currentDayList[currentnum] == 3) //절벽
        {
            bgSpriter.sprite = BackGroundSprite[4];
        }
        else if (currentDayList[currentnum] == 4) //2일차 꺽인길 1
        {
            bgSpriter.sprite = BackGroundSprite[5];
        }
        else if (currentDayList[currentnum] == 5) //2일차 꺽인길 2
        {
            bgSpriter.sprite = BackGroundSprite[6];
        }
        else if (currentDayList[currentnum] == 6) { //2일차 기본
            if (bgSpriter.sprite == BackGroundSprite[7])
            {
                bgSpriter.sprite = BackGroundSprite[8];
            }
            else
            {
                bgSpriter.sprite = BackGroundSprite[7];
            }
        } 

        //정리 기본1,2 , 꺽인길 1,2, 절벽 ,2일차 꺽인길 1,2, 2일차 기본 1,2,
        currentnum++;
        


        if (currentnum >= currentDayList.Count)
        {
            currentnum = 0;
            currentday++;
        }

        circularImageObject.SetActive(true);
        circularImage.fillAmount = 1f;

        StopCoroutine(FillAmountLerp());
        StartCoroutine(FillAmountLerp());

        button.enabled = false;

        Invoke("createButton", 1f);

        
    }






    public void createButton()
    {
        button.enabled = true;
    }


    IEnumerator FillAmountLerp()
    {
        float elapsedTime = 0f;
        float startValue = 1f;
        float endValue = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            circularImage.fillAmount = newValue;
            yield return null;
        }

        // 최종 값을 정확하게 맞춰줌
        circularImage.fillAmount = endValue;


    }
    
  

    void AppearImage() {

        RectTransform rt = Ghosts[0].GetComponent<RectTransform>();

        Tuple<int, int> dayAndTurn = SaveManager.getDayAndTurn();




        int day = dayAndTurn.Item1;
        int turn = dayAndTurn.Item2;

        if (day == 0  && turn == 6)
        {
            rt.localScale = new Vector3(1f, 1f, 1f);
            Ghosts[0].SetActive(true);
            Debug.Log("HI");
        }

         if (day == 0 && turn == 7)
        {
            rt.localScale = new Vector3(2f, 2f, 1f);
        }

        if (day == 0 && turn == 8)
        {
            Ghosts[0].SetActive(false);
        }

        if (day == 0 && turn == 9)
        {
            eventManager.Event0_9();
        }

        if (day == 0 && turn == 10)
        {
            eventManager.StartCoroutine(eventManager.Event0_10());
        }

       




    }
    
}




