using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controller 
{

    public static GameMode currentState = GameMode.Overview;
    public enum GameMode
    {
        Building,
        Overview,
        Fighting,
        Menu,
        EndTurn,
        PlayerMove,
        AutoMove
    }
}
