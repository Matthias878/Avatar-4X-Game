using UnityEngine;
using UnityEngine.Tilemaps;

public class Build_menu : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject Build_Menu;
    private Vector3Int clickedCellPos = new Vector3Int(0, 0, -5);

    void Update()
    {
        if (!(Controller.currentState == Controller.GameMode.Building))
        {
            Build_Menu.SetActive(false); 
        clickedCellPos = new Vector3Int(0, 0, -5);
            return;
        }
        else
            Build_Menu.SetActive(true);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);
        cellPos.z = 1;
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
        cellCenterPos.z = 1;

        if (clickedCellPos.z < -1)
        {
            Build_Menu.transform.position = cellCenterPos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (cellPos == clickedCellPos)
            {
                clickedCellPos = new Vector3Int(0, 0, -5);
            }
            else
            {
                clickedCellPos = cellPos;
                Build_Menu.transform.position = cellCenterPos;
            }
        }
    }
}
