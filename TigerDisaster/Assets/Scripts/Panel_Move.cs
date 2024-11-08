using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Move : MonoBehaviour
{
    public List<GameObject> panels;
    public List<int> conditions = new List<int>();
    public int currentnum = 0;
   


    public void ShowPanel() {

        
            GameObject paneli = panels[conditions[currentnum]];
            

            paneli.SetActive(true);

            if (currentnum != 0) {
                GameObject panel_pre = panels[conditions[currentnum - 1]];
                panel_pre.SetActive(false);
               
            }
            currentnum++;

            if (currentnum >= conditions.Count) {
                currentnum = 0;

            }


         TurnManager.setDayAndTurn();

        
     
    
    }
}