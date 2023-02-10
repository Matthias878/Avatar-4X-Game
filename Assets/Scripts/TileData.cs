using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class TileData : MonoBehaviour
{

    public GameObject startArmy;
    //public GameObject footstepsCanGo = new GameObject();
    private List<GameObject> todestroy= new List<GameObject>();
    public static List<(int, int, int, GameObject)> player_moveable = new List<(int, int, int, GameObject)>(); //xKoordinate, yKoordinate, movementPoints
    public class Tile
    {
        public int xCoordinate;
        public int yCoordinate;
        public int farmLevel;
        public int streetLevel;
        public string terrain;
        public bool island;

        public Tile(int x, int y, int farm, int street, string ter, bool land)
        {
            xCoordinate = x;
            yCoordinate = y;
            farmLevel = farm;
            streetLevel = street;
            terrain = ter;
            island = land;
        }
    }

    static public Tile[,] tiles;
    

    void Start()
    {

        player_moveable.Add((-2, -2, 8, startArmy));
        tiles = new Tile[41, 41];
        string terrain = "flat";

        for (int x = -20; x <= 20; x++)
        {
            for (int y = -20; y <= 20; y++)
            {
                tiles[x + 20, y + 20] = new Tile(x, y, 1, 1, terrain, true);
            }
        }
        tiles[19, 19] = new Tile(-1, -1, 1, 1, terrain, false);
        tiles[19, 18] = new Tile(-1, -2, 1, 1, terrain, false);
        tiles[19, 17] = new Tile(-1, -3, 1, 1, terrain, false);
        tiles[18, 17] = new Tile(-2, -3, 1, 1, terrain, false);
        tiles[17, 17] = new Tile(-3, -3, 1, 1, terrain, false);
        tiles[17, 16] = new Tile(-3, -4, 1, 1, terrain, false);
        tiles[16, 15] = new Tile(-4, -5, 1, 1, terrain, false);
        tiles[16, 14] = new Tile(-4, -6, 1, 1, terrain, false);
    }


    public static bool Elementispart(int x, int y)
    {
        return player_moveable.Exists(element => element.Item1 == x && element.Item2 == y);
    }

    public static void  UpdateMoveables(int new1, int new2, int old1, int old2)
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


    public static bool[,] Showmoveable(int X, int Y, int movementPoints)
    {
        int x = X + 20;
        int y = Y + 20;
        int step = 0;
        Debug.Log("START = " + x+ " "+y );
        bool[,] visited = new bool[41, 41];
        for (int i = 0; i < 41; i++) { for (int j = 0; j < 41; j++) { visited[i, j] = false; } }
        visited[x, y] = true;
        bool[,] a = BinaryOr(visited, (ShowmoveableHelp(x + 1, y, movementPoints - calculateTileMoveCost(x, y), visited, step)));
        bool[,] b = BinaryOr(a, (ShowmoveableHelp(x - 1, y, movementPoints - calculateTileMoveCost(x, y), visited, step)));
        if (y % 2 == 0)
        {
            bool[,] g = BinaryOr(b, (ShowmoveableHelp(x, y + 1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] h = BinaryOr(g, (ShowmoveableHelp(x, y - 1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] i = BinaryOr(h, (ShowmoveableHelp(x - 1, y + 1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] j = BinaryOr(i, (ShowmoveableHelp(x - 1, y - 1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            return j;
        }
        else
        {

            bool[,] c = BinaryOr(b, (ShowmoveableHelp(x, y + 1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] d = BinaryOr(c, (ShowmoveableHelp(x, y - 1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] e = BinaryOr(d, (ShowmoveableHelp(x + 1, y + 1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] f = BinaryOr(e, (ShowmoveableHelp(x + 1, y - 1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            return f;
        }
    }

    static bool[,] ShowmoveableHelp(int x, int y, int movementPoints, bool[,] visited, int step)
    {
        step++;
        if (tiles[x, y].island == false)
        {

            Debug.Log("Tile is not Land");
            return visited;

        }
        if (visited[x, y ]== true)
        {
            return visited;
        }
        if (movementPoints - calculateTileMoveCost(x, y) < 0)
        {
            return visited;
        }
        else if (movementPoints - calculateTileMoveCost(x, y) == 0)
        {
            //Show on map
           
             //GameObject duplicate = Instantiate(TileData.footstepsCanGo);
             //duplicate.transform.position = originalObject.transform.position + new Vector3(2, 0, 0);
           
            Debug.Log("ENDE: "+(x-20) + " "+ (y-20)+ " Step: "+ step);
            visited[x, y] = true;
            return visited;
        } else {
            //Show on map
            Debug.Log((x - 20) + " " + (y - 20) + " Step: " + step);
            visited[x, y] = true;
            bool[,] a = BinaryOr(visited, (ShowmoveableHelp(x + 1, y, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] b = BinaryOr(a, (ShowmoveableHelp(x - 1, y, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            if (y % 2 == 0)
            {
                bool[,] g = BinaryOr(b, (ShowmoveableHelp(x , y+1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
                bool[,] h = BinaryOr(g, (ShowmoveableHelp(x , y-1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
                bool[,] i = BinaryOr(h, (ShowmoveableHelp(x - 1, y+1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
                bool[,] j = BinaryOr(i, (ShowmoveableHelp(x - 1, y-1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
                return j;
            }
            else { 

            bool[,] c = BinaryOr(b, (ShowmoveableHelp(x, y+1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] d = BinaryOr(c, (ShowmoveableHelp(x, y-1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] e = BinaryOr(d, (ShowmoveableHelp(x + 1, y+1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
            bool[,] f = BinaryOr(e, (ShowmoveableHelp(x + 1, y-1, movementPoints - calculateTileMoveCost(x, y), visited, step)));
                return f;
            }
        }
    }

    static bool[,] BinaryOr(bool[,] array1, bool[,] array2)
    {
        int rows = array1.GetLength(0);
        int columns = array1.GetLength(1);

        bool[,] result = new bool[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = array1[i, j] || array2[i, j];
            }
        }

        return result;
    }

    private static int calculateTileMoveCost(int x, int y)
    {
        return 1;
    }
}

