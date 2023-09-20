using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Move_Menu : MonoBehaviour
{
    public Tilemap tilemap;
    private bool newclick = false;
    private TileData.CustomTile tomove;

    
    public static bool IsArmy(int x, int y){
        return TileData.GetTile(x, y).HasArmy();
    }
    void Update()
    {
        
        if(!(Input_Menu.currentState == Input_Menu.GameMode.PlayerMove)){Display.ClearAll();TileData.Clearflags();newclick = false;return;}

        if((!newclick) && Input_Menu.currentState == Input_Menu.GameMode.PlayerMove){
            //What army is mouse clicking
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(worldPos);
            tomove = TileData.GetTile(cellPos.x, cellPos.y);
            TileData.Showmoveable(cellPos.x, cellPos.y, tomove.GetArmy().movementPoints);
            newclick = true; 
        }

        //Where is army going
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(worldPos);
            if (TileData.GetTile(cellPos.x, cellPos.y).movearmyhere(tomove.GetArmy()))
                tomove.removearmy();
        }
    }

}

