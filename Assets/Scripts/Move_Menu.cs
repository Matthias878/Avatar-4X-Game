using UnityEngine;
using UnityEngine.Tilemaps;

public class Move_Menu : MonoBehaviour
{
    public Tilemap tilemap;
    private bool doIfFirstExecuted = false;
    private GameObject tomove;
    void Update()
    {
        if (!(Controller.currentState == Controller.GameMode.PlayerMove))
        {
            doIfFirstExecuted = false;
            return;
        }
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);
        if (!doIfFirstExecuted)
        {
            doIfFirstExecuted = true;
            tomove = Army_Menu.GetPlayerMoveable(cellPos.x, cellPos.y);
            TileData.Showmoveable(cellPos.x, cellPos.y, 3);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
            cellCenterPos.z = 1;
            Vector3Int oldPosofOb = tilemap.WorldToCell(tomove.transform.position);
            //TileData.Showmoveable(cellPos.x, cellPos.y, 3);
            Army_Menu.UpdateMoveables(cellPos.x, cellPos.y, oldPosofOb.x, oldPosofOb.y);
            tomove.transform.position = cellCenterPos;
        }

    }
}
