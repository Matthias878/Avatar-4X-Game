using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameStart : Event
{
    new public bool activeIs = false;

    private bool hasactivated = false;
    public override void Execute(){
        hasactivated = true;
        if(activeIs == true){
            return;
        }
        activeIs = true;
        SystemEV.One.OneOption.SetActive(true);
        SystemEV.One.Text.text = "Willkommen General, der Erdkönig hat ihnen befohlen diese Region von der Feuernation zu befreien!";
        SystemEV.One.ButtonOneText.text = "Jawohl Sir";
        return;
    }

    public override void Resolve(){
        SystemEV.One.OneOption.SetActive(false);
        activeIs = false;
        return;
    }

    public override bool ShouldActivate(){
        if(hasactivated == false && Input_Menu.currentTurn == 0)
            return true;
        return false;
    }
    public override bool IsActive(){
        return activeIs;
    }
}
