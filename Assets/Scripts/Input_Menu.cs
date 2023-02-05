using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Menu : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
        if (Controller.currentState == Controller.GameMode.Building)
        {
            Controller.currentState = Controller.GameMode.Overview;
        }
        else {
            Controller.currentState = Controller.GameMode.Building;
        }
    }
    }

}
