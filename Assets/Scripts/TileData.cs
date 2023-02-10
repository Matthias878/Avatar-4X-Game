using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData : MonoBehaviour
{
    private static TileData errorfixer;
    public  Tilemap tilemap;
    public GameObject startArmy;
    public  GameObject footstepsCanGo;
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
        errorfixer = new TileData();
        errorfixer.tilemap = this.tilemap;
        errorfixer.footstepsCanGo = this.footstepsCanGo;

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


    public static (bool, int)[,] Showmoveable(int X, int Y, int movementPoints)
    {
       
        int x = X + 20;
        int y = Y + 20;
        Debug.Log((x - 20) + " " + (y - 20));
        int step = 0;
        (bool, int)[,] visited = new (bool, int)[41, 41];
        for (int i = 0; i < 41; i++) { for (int j = 0; j < 41; j++) { visited[i, j] = (false, 0); } }
        visited[x, y] = (true,3);
        visited[18, 18] = (true,3);
        int tempx;
        if (y % 2 == 0)
        { tempx = x - 1; }
        else
        { tempx = x + 1; }
        (bool, int)[,] a = BinaryOr(visited, (ShowmoveableHelp(x + 1, y, movementPoints, visited)));
        (bool, int)[,] b = BinaryOr(a, (ShowmoveableHelp(x - 1, y, movementPoints, visited)));
        (bool, int)[,] g = BinaryOr(b, (ShowmoveableHelp(x, y + 1, movementPoints, visited)));
        (bool, int)[,] h = BinaryOr(g, (ShowmoveableHelp(x, y - 1, movementPoints, visited)));
        (bool, int)[,] e = BinaryOr(h, (ShowmoveableHelp(tempx, y + 1, movementPoints, visited)));
        (bool, int)[,] f = BinaryOr(e, (ShowmoveableHelp(tempx, y - 1, movementPoints, visited)));
        return f;
    }

    static (bool, int)[,] ShowmoveableHelp(int x, int y, int movementPoints, (bool, int)[,] visited )
    {
        Debug.Log("X: " + (x - 20) + " Y: " + (y - 20) + " currentPoints: " + (movementPoints-1));
        int endmovementPoints = movementPoints - calculateTileMoveCost(x, y);

        if (tiles[x, y].island == false)
        {
            Debug.Log("Ist kein Land");
            return visited;
        }

        if (visited[x, y].Item1 == true && ((visited[x,y].Item2> endmovementPoints)))
        {
            
            Debug.Log("Already here / more Points");
            return visited;
        }
        if (endmovementPoints < 0)
        {
            Debug.Log("Keine Punkte mehr");
            return visited;
        }
        else if (endmovementPoints >= 0)
        {
           GameObject duplicate = Instantiate(errorfixer.footstepsCanGo);
            duplicate.transform.position = errorfixer.tilemap.CellToWorld(new Vector3Int(x-20, y-20, 3)); if (endmovementPoints == 0)
                if (endmovementPoints == 0)
                { return visited; }
                    
            int tempx;
            if (y % 2 == 0)
            { tempx = x - 1; }
            else
            { tempx = x + 1; }
            (bool, int)[,] a = BinaryOr(visited, (ShowmoveableHelp(x + 1, y, endmovementPoints, visited)));
            (bool, int)[,] b = BinaryOr(a, (ShowmoveableHelp(x - 1, y, endmovementPoints, visited)));
            (bool, int)[,] g = BinaryOr(b, (ShowmoveableHelp(x, y + 1, endmovementPoints, visited)));
            (bool, int)[,] h = BinaryOr(g, (ShowmoveableHelp(x, y - 1, endmovementPoints, visited)));
            (bool, int)[,] e = BinaryOr(h, (ShowmoveableHelp(tempx, y+1, endmovementPoints, visited)));
            (bool, int)[,] f = BinaryOr(e, (ShowmoveableHelp(tempx, y-1, endmovementPoints, visited)));
            visited[x, y] = (true, endmovementPoints);
            return f;
         }
        Debug.Log("Massive Movement-Point Error"+ endmovementPoints + movementPoints + calculateTileMoveCost(x, y));
        return null;
    }

    static (bool, int)[,] BinaryOr((bool,int)[,] array1, (bool,int)[,] array2)
    {
        int rows = array1.GetLength(0);
        int columns = array1.GetLength(1);

        (bool, int)[,] result = new (bool, int)[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (array1[i, j].Item1 && array2[i, j].Item1)
                {
                    if (array1[i, j].Item2> array2[i,j].Item2)
                    {
                        result[i, j] = array1[i, j];
                    }else
                    {
                        result[i, j] = array2[i, j];
                    }
                }else if ((!(array1[i, j].Item1)) && array2[i, j].Item1)
                {
                    result[i, j] = array2[i, j];
                }else if (array1[i, j].Item1 && (!(array2[i, j].Item1)))
                {
                    result[i, j] = array1[i, j];
                }
                else
                {
                    result[i,j] = array2[i, j];
                }

            }
        }

        return result;
    }

    private static int calculateTileMoveCost(int x, int y)
    {
        return 1;
    }
}

