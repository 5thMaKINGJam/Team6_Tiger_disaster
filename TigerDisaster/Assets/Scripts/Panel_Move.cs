using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Move : MonoBehaviour
{
    public List<GameObject> panels;
    private List<int> conditions = new List<int> { 1, 0, 1, 0 };
    private int currentnum = 1;
    public GameObject button;


    public void ShowPanel()
    {


        GameObject paneli = panels[conditions[currentnum]];

        if (currentnum != 0)
        {
            GameObject panel_pre = panels[conditions[currentnum - 1]];
            panel_pre.SetActive(false);

        }


        paneli.SetActive(true);
        button.SetActive(false);

        
        currentnum++;
        Invoke("createButton", 2f);

        if (currentnum >= conditions.Count)
        {
            currentnum = 0;







        }
        // TurnManager.setDayAndTurn();

    }



    public void createButton()
    {
        button.SetActive(true);
    }
}




