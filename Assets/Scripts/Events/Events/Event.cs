using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Event : MonoBehaviour
{
    public bool activeIs;
    public static DisplayEvents SystemEV;
    public  DisplayEvents Tester;
    void Start(){SystemEV = Tester;}
    public virtual bool ShouldActivate(){
        Debug.Log("Event.ShouldActivate() here");
        return false;
    }
    public virtual void Execute(){
        Debug.Log("Event.Execute() here");
        return;
    }
    public virtual void Resolve(){
        Debug.Log("Event.Resolve() here");
        return;
    }
    public virtual bool IsActive(){
        return false;
    }
}
