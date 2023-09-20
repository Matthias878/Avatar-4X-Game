using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    private static List<Event> Turn_Events = new List<Event>();
    private static List<Event> Location_Events = new List<Event>();
    private static List<Event> Other_Events = new List<Event>();
    void Start()
    {
        Turn_Events.Add(new GameStart());
        Turn_Events.Add(new GiveHelp());
    }
    public static bool CheckEvents()
    {
        foreach (Event tet in Turn_Events)
        {
            if (tet.ShouldActivate()) 
            {
                tet.Execute();
                return true;
            }
        }
        return false;
    }
    public static bool ResolveEvents(){
        
        foreach (Event tet in Turn_Events)
        {
            Debug.Log("C");
            if (tet.IsActive()) 
            {
                Debug.Log("D");
                tet.Resolve();
                return true;
            }
        }
        return false;
    }


}
