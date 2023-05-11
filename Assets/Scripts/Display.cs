using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Display : MonoBehaviour
{
    private static Display errorfixer;
    public Tilemap tilemap;
    public GameObject footstepsCanGo;
    private static List<GameObject> todestroy = new List<GameObject>();
    void Start()
    {
        errorfixer = new Display();
       errorfixer.tilemap = this.tilemap;
       errorfixer.footstepsCanGo = this.footstepsCanGo;

    }
    public static void showFootstep(int x, int y)
    {

        GameObject duplicate = Instantiate(errorfixer.footstepsCanGo);
        duplicate.transform.position = errorfixer.tilemap.CellToWorld(new Vector3Int(x - 20, y - 20, 3));
        todestroy.Add(duplicate);
    }
}
