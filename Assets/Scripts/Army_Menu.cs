//depreciated


/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army_Menu : MonoBehaviour
{
    public GameObject startArmy;
    public static List<(int, int, int, GameObject)> player_moveable = new List<(int, int, int, GameObject)>(); //xKoordinate, yKoordinate, movementPoints

    void Start()
    {
        player_moveable.Add((-2, -2, 8, startArmy));
    }

    public static bool Elementispart(int x, int y)
    {
        return player_moveable.Exists(element => element.Item1 == x && element.Item2 == y);
    }

    public static void UpdateMoveables(int new1, int new2, int old1, int old2)
    {
        for (int i = 0; i < player_moveable.Count; i++)
        {
            if (player_moveable[i].Item1 == old1 && player_moveable[i].Item2 == old2)
            {
                player_moveable[i] = (new1, new2, player_moveable[i].Item3, player_moveable[i].Item4);
                break;
            }
        }
    }

    public static GameObject GetPlayerMoveable(int input1, int input2)
    {
        foreach ((int x, int y, int z, GameObject gameObject) in player_moveable)
        {
            if (input1 == x && input2 == y)
            {
                return gameObject;
            }
        }

        throw new ArgumentException("Input is incorrect");
    }
}
*/