using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Input_Menu : MonoBehaviour
{
    public Button endturnbutton;
    public TileData tileData;
    public Tilemap tilemap;
    private void Start()
    {
        endturnbutton.onClick.AddListener(EndTurnButtonclicked);
    }

    void EndTurnButtonclicked()
    {
        Debug.Log(Controller.currentState);
    }
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            if (Controller.currentState == Controller.GameMode.Building)
            {
                Controller.currentState = Controller.GameMode.Overview;
            }
            else
            {
                Controller.currentState = Controller.GameMode.Building;
            }
        }

        if (Input.GetMouseButtonDown(0))
            {
                if (Controller.currentState == Controller.GameMode.Overview)
                {
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int cellPos = tilemap.WorldToCell(worldPos);
                    if (TileData.Elementispart(cellPos.x, cellPos.y))
                    {
                        Controller.currentState = Controller.GameMode.PlayerMove;
                    }
                }
            }
        }
}
