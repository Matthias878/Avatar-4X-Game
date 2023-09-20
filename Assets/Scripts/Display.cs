using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro; 

public class Display : MonoBehaviour
{
    public static Display errorfixer;
    public Tilemap tilemap;
    public GameObject footstepsCanGo;

    public GameObject Armypicture;
    private static List<GameObject> todestroy = new List<GameObject>();
    private static List<((int, int), GameObject)> Armies = new List<((int, int), GameObject)>();

    public TextMeshProUGUI textMeshProComponent; // Reference to your TextMeshPro component in the Unity Inspector


    
    void Start()
    {
        errorfixer = this;
        errorfixer.tilemap = this.tilemap;
        errorfixer.footstepsCanGo = this.footstepsCanGo;
        errorfixer.Armypicture = this.Armypicture;
        errorfixer.textMeshProComponent = this.textMeshProComponent;
        TileData.Startwait();


    }
    // Use this function to change the text
    public static void UpdateCurrentState(string newText)
    {
        if (errorfixer.textMeshProComponent != null)
        {
            errorfixer.textMeshProComponent.text = "Current turn = " + Input_Menu.currentTurn + "Current State: " + newText;
        }
    }


    public static void showFootstep(int x, int y)
    {

        GameObject duplicate = Instantiate(errorfixer.footstepsCanGo);
        duplicate.transform.position = errorfixer.tilemap.CellToWorld(new Vector3Int(x - 20, y - 20, 3));
        todestroy.Add(duplicate);
    }

    public static void showArmy(int x, int y)
    {

        GameObject duplicate = Instantiate(errorfixer.Armypicture);
        duplicate.transform.position = errorfixer.tilemap.CellToWorld(new Vector3Int(x - 20, y - 20, 3));
        Armies.Add(((x,y), duplicate));
        //todestroy.Add(duplicate);
    }

    public static void ClearAll(){
        todestroy.ForEach(obj => Destroy(obj));
        todestroy.Clear();
    }

    public static void ClearArmy(int x, int y){
    var matchingElements = Armies.FindAll(item => item.Item1 == (x, y));
    
    foreach (var element in matchingElements)
    {
        Destroy(element.Item2);
    }
        Armies.RemoveAll(item => item.Item1 == (x,y));
    }

}
