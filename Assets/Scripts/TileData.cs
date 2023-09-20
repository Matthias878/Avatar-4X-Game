using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
//using QuickGraph;
//using QuickGraph.Algorithms;
//using QuickGraph.Algorithms.ShortestPath;

public class TileData : MonoBehaviour
{
    public class CustomTile
    {
        public string terrain;
        public int farmLevel;
        public int streetLevel;
        public int Festungsstufe;
        public bool IsLand;
        public Army army;
        public bool flag; //Can be used for temporary saving, like footsteps

        public CustomTile(int farm, int street, string ter, bool land)
        {
            farmLevel = farm;
            streetLevel = street;
            terrain = ter;
            IsLand = land;
            army = null;
            flag = false;
            Festungsstufe = 1 ;
        }

        public CustomTile()
        {
            farmLevel = 1;
            streetLevel = 1;
            terrain = "flat";
            IsLand = true;
            army = null;
            flag = false;
            Festungsstufe =1 ;
        }

        public int increaseFarmTile(){farmLevel += 1;return farmLevel;}
        public int increaseStreetTile(){streetLevel += 1;return streetLevel;}
        public int decreaseFarmTile(){farmLevel -= 1;return farmLevel;}
        public int decreaseStreetTile(){streetLevel -= 1;return streetLevel;}
        public void ClearFlag(){flag = false;}
        public bool HasArmy (){if(army == null){return false;}return true;}
        public bool movearmyhere(Army newarmy) //picture needs to be moved seperatly DONE
        {                                      // reduce movement points
            if (army != null)                  // redraw footsteps
                return false;                  //Check if inside of footsteps DONE
            if (flag == false)
                return false;
            army = newarmy;
            Display.ClearAll();
            Display.showArmy(GetPosition(this).Item1, GetPosition(this).Item2);
            TileData.Clearflags();
            return true;
        }
        public Army GetArmy(){
            return army; //Should never return null
        }
        public bool removearmy(){
            if (this.HasArmy()){ 
                Display.ClearArmy(GetPosition(this).Item1, GetPosition(this).Item2); 
                army = null; 
                return true;
                }
            return false;
        }
    }

    static public CustomTile[,] tiles= new CustomTile[41, 41];
    //Take X/Y from tilemap and add it to 20 to get it in tiles

    public static CustomTile GetTile(int x, int y){//Tilepos to array
        return tiles[20+x,20 +y];
    }

    public static void Clearflags(){
        int rows = tiles.GetLength(0);
        int columns = tiles.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (tiles[i, j] != null)
                {
                    tiles[i, j].ClearFlag();
                }
            }
        }
    }
    
    void Start()
    {
        string terrain = "flat";

        for (int x = -20; x <= 20; x++)
        {
            for (int y = -20; y <= 20; y++)
            {
                tiles[x + 20, y + 20] = new CustomTile();
            }
        }
        tiles[19, 19] = new CustomTile(1, 1, terrain, false);
        tiles[19, 18] = new CustomTile(1, 1, terrain, false);
        tiles[19, 17] = new CustomTile(1, 1, terrain, false);
        tiles[18, 17] = new CustomTile(1, 1, terrain, false);
        tiles[17, 17] = new CustomTile(1, 1, terrain, false);
        tiles[17, 16] = new CustomTile(1, 1, terrain, false);
        tiles[16, 15] = new CustomTile(1, 1, terrain, false);
        tiles[16, 14] = new CustomTile(1, 1, terrain, false);
    }

    public static void Startwait(){
        tiles[18, 18].flag = true;
        tiles[18, 18].movearmyhere(new Army ("Matthias Limmer"));//TIMING ISSUE WITH DISPLAY
        tiles[23, 15].flag = true;
        tiles[23, 15].movearmyhere(new Army ("Lanna"));//TIMING ISSUE WITH DISPLAY

    }


    public static (int, int) GetPosition(CustomTile Tile){
    int rows = tiles.GetLength(0);
    int columns = tiles.GetLength(1);
    for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (tiles[i, j] != null)
                {
                    if (tiles[i, j].Equals(Tile))
                     return (i, j);

                }
            }
        }
        Debug.Log("Error! Tile not found! Using 0,0 as Start");
        return (0,0);
    }

    //get's a point and movementPoints draws all reachable tiles
    public static (bool, int)[,] Showmoveable(int X, int Y, int BewPthis)
    {
       
        int x = X + 20;int y = Y + 20;
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

    private static (bool, int)[,] Moves(int x, int y, int movementPoints, (bool, int)[,] visited )
    {
        //how many movepoints after the ones for current tile abgezogen
        int BewPthis = movementPoints - calculateTileMoveCost(x, y);
        //alle Gr�nde warum dieses Tile nicht gegangen werden kann: 1. ist kein Land 2. ein anderer Weg is schneller 3. Tile ist mit Bewegungspunkten nicht erreichbar
        if ((tiles[x, y].IsLand == false)|| (visited[x, y].Item1 == true && ((visited[x, y].Item2 > BewPthis)))|| (BewPthis < 0)){return visited;}
        //Hat noch kein Symbol, dass dieses erreicht werden kann, Symbol wird erstellt
        if (visited[x, y].Item1 == false){Display.showFootstep(x, y);}
        tiles[x, y].flag = true;
        //dieses Tile kann mit so und so vielen Bewegungspunkten erreicht werden
        visited[x, y] = (true, BewPthis);
        //hat keine Punkte mehr
        if (BewPthis == 0){return visited;}
        //F�ge der Liste alle Nachbarn an und gib sie zur�ck
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

