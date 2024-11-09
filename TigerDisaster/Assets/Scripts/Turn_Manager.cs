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
    private SpriteRenderer bgSpriter;
    public Sprite[] BackGroundSprite;
    public Image circularImage;
    private float duration = 1f;  // 슬라이더가 1에서 0으로 줄어드는 시간
    public GameObject[] Ghosts;
    
    private EventManager eventManager;

   // public List<GameObject> panels;

    private List<int> day0 = new List<int> { 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 0, 0, 0, 0 };
    private List<int> day1 = new List<int> { 0, 0, 0,0,0,0,0,0 ,0,1,2,0,1,0,0};
    private List<int> day2 = new List<int> { 1, 0, 1 };



    


    void Awake()
    {

        eventManager = FindObjectOfType<EventManager>();
        bgSpriter = bg.GetComponent<SpriteRenderer>();
        //get오늘 turn 


    }









    public void ShowPanel()
    {

        AppearImage();


        List<int> currentDayList = day0;  // 기본적으로 day0로 시작




        if (currentnum >= day0.Count)
        {
            currentDayList = day1;  // day1이 끝나면 day2로
            currentnum = 0;
            currentday = 1;
        }

        if (currentnum >= day1.Count)
        {
            currentDayList = day2;  // day2가 끝나면 day3으로
            currentnum = 0;
            currentday = 2;
        }

        if (currentnum == 0 || currentnum == currentDayList.Count - 1)
        {
            bgSpriter.sprite = BackGroundSprite[4];
        }






        // 해당 day의 패널을 가져옴
        if (currentDayList[currentnum] == 0) {

            if (bgSpriter.sprite == BackGroundSprite[0])
            {

                bgSpriter.sprite = BackGroundSprite[1];
            }
            else {
                bgSpriter.sprite = BackGroundSprite[0];
            }

        }
        else if (currentDayList[currentnum] == 1) {
            bgSpriter.sprite = BackGroundSprite[2];
        }

        else if (currentDayList[currentnum] == 2)
        {
            bgSpriter.sprite = BackGroundSprite[3];
        }
        currentnum++;




        circularImageObject.SetActive(true);
        circularImage.fillAmount = 1f;

        StopCoroutine(FillAmountLerp());
        StartCoroutine(FillAmountLerp());
       
        button.enabled = false;



        
        Invoke("createButton", 1f);
        
        
        
        SaveManager.setDayAndTurn(currentday,currentnum);


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




