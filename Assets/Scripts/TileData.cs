using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{


    static List<(int, int, int)> player_moveable = new List<(int, int, int)>(); //xKoordinate, yKoordinate, movementPoints
    public class Tile
    {
        public int xCoordinate;
        public int yCoordinate;
        public int farmLevel;
        public int streetLevel;
        public string terrain;

        public Tile(int x, int y, int farm, int street, string ter)
        {
            xCoordinate = x;
            yCoordinate = y;
            farmLevel = farm;
            streetLevel = street;
            terrain = ter;
        }
    }

    static public Tile[,] tiles;

    void Start()
    {

        player_moveable.Add((5, 0, 8));
        tiles = new Tile[41, 41];
        string terrain = "flat";

        for (int x = -20; x <= 20; x++)
        {
            for (int y = -20; y <= 20; y++)
            {
                tiles[x + 20, y + 20] = new Tile(x, y, 1, 1, terrain);
            }
        }
    }


    public static bool Elementispart(int x, int y)
    {
        return player_moveable.Exists(element => element.Item1 == x && element.Item2 == y);
    }
}
