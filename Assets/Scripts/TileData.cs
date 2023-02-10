using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileData : MonoBehaviour
{
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




    public static (bool, int)[,] Showmoveable(int X, int Y, int BewPthis)
    {
       
        int x = X + 20;
        int y = Y + 20;
        (bool, int)[,] visited = new (bool, int)[41, 41];
        for (int i = 0; i < 41; i++) { for (int j = 0; j < 41; j++) { visited[i, j] = (false, 0); } }
        visited[x, y] = (true,3);
        int tempx = (y % 2 == 0) ? x - 1 : x + 1;
        (bool, int)[,] result = BinaryOr(visited, (Moves(x + 1, y, BewPthis, visited)));
        result = BinaryOr(result, (Moves(x - 1, y, BewPthis, visited)));
        result = BinaryOr(result, (Moves(x, y + 1, BewPthis, visited)));
        result = BinaryOr(result, (Moves(x, y - 1, BewPthis, visited)));
        result = BinaryOr(result, (Moves(tempx, y + 1, BewPthis, visited)));
        result = BinaryOr(result, (Moves(tempx, y - 1, BewPthis, visited)));
        return result;
    }

    static (bool, int)[,] Moves(int x, int y, int movementPoints, (bool, int)[,] visited )
    {
        //how many movepoints after the ones for current tile abgezogen
        int BewPthis = movementPoints - calculateTileMoveCost(x, y);
        //alle Gründe warum dieses Tile nicht gegangen werden kann: 1. ist kein Land 2. ein anderer Weg is schneller 3. Tile ist mit Bewegungspunkten nicht erreichbar
        if ((tiles[x, y].island == false)|| (visited[x, y].Item1 == true && ((visited[x, y].Item2 > BewPthis)))|| (BewPthis < 0)){return visited;}
            //Hat noch kein Symbol, dass dieses erreicht werden kann, Symbol wird erstellt
            if (visited[x, y].Item1 == false){Display.showFootstep(x, y);}
            //dieses Tile kann mit so und so vielen Bewegungspunkten erreicht werden
            visited[x, y] = (true, BewPthis);
            //hat keine Punkte mehr
            if (BewPthis == 0){return visited;}
            //Füge der Liste alle Nachbarn an und gib sie zurück
            int tempx = (y % 2 == 0) ? x - 1 : x + 1;
            (bool, int)[,] result = BinaryOr(visited, (Moves(x + 1, y, BewPthis, visited)));
            result = BinaryOr(result, (Moves(x - 1, y, BewPthis, visited)));
            result = BinaryOr(result, (Moves(x, y + 1, BewPthis, visited)));
            result = BinaryOr(result, (Moves(x, y - 1, BewPthis, visited)));
            result = BinaryOr(result, (Moves(tempx, y+1, BewPthis, visited)));
            result = BinaryOr(result, (Moves(tempx, y-1, BewPthis, visited)));
            return result;
    }

    private static (bool, int)[,] BinaryOr((bool,int)[,] array1, (bool,int)[,] array2)
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

