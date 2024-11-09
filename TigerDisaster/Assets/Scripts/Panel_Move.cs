using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Panel_Move : MonoBehaviour
{
    public List<GameObject> panels;
    private List<int> conditions = new List<int> { 0, 1, 0, 1 };
    private int currentnum = 1;
    public Button button;
    public GameObject circularImageObject;
    public Image circularImage;
    public float duration = 1f;  // �����̴��� 1���� 0���� �پ��� �ð�
    public GameObject[] Ghosts;
    


    void Update()
    {
        AppearImage();
    }







    public void ShowPanel()
    {


        GameObject paneli = panels[conditions[currentnum]];


        if (currentnum == 0)
        {
            GameObject panel_pre = panels[conditions[conditions.Count - 1]];
            panel_pre.SetActive(false);
        }

        else
        {
            GameObject panel_pre = panels[conditions[currentnum - 1]];
            panel_pre.SetActive(false);

        }
        
        circularImageObject.SetActive(true);
        circularImage.fillAmount = 1f;

        StopCoroutine(FillAmountLerp());
        StartCoroutine(FillAmountLerp());
        paneli.SetActive(true);
        button.enabled = false;


        currentnum++;
        Invoke("createButton", 1f);
        

        if (currentnum >= conditions.Count)
        {
            currentnum = 0;








        }
        SaveManager.setDayAndTurn();


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

        // ���� ���� ��Ȯ�ϰ� ������
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

       


    }
    
}




