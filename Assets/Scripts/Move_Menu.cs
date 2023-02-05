using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.currentState == Controller.GameMode.PlayerMove) 
        {
            Debug.Log("Can Move TEST");
        }
        
    }
}
