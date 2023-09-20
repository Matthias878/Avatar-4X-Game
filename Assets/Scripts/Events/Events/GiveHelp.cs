using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GiveHelp : Event
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
        SystemEV.One.Text.text = "General, der Erdkönig hat ihnen Unterstüzung zukommen lassen";
        SystemEV.One.ButtonOneText.text = "Wurde aber auch Zeit!";
        return;
    }

    public override void Resolve(){
        SystemEV.One.OneOption.SetActive(false);
        activeIs = false;

    }

    public override bool ShouldActivate(){
        if(hasactivated == false && Input_Menu.currentTurn == 5)
            return true;
        return false;
    }
    public override bool IsActive(){
        return activeIs;
    }
}
