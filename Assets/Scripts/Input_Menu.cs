using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Input_Menu : MonoBehaviour
{

    public static GameMode currentState = GameMode.Overview;
    public enum GameMode
    {
        Building,
        Overview,
        Fighting,
        Menu,
        EndTurn,
        PlayerMove,
        AutoMove,
        TurnStart,
        ResolveEvents
    }

    public Button endturnbutton;
    public Button EventButtonOneOne;

    private bool EventButtonClicked = false;
    public Tilemap tilemap;
    public static int currentTurn = 0;
    public static bool NewTurn = true;
    private void Start()
    {
        endturnbutton.onClick.AddListener(EndTurnButtonclicked);
        EventButtonOneOne.onClick.AddListener(EventButtonsclicked);
    }

    void EndTurnButtonclicked()
    {
        NewTurn = true;//garantieren
        currentTurn++;
        NewTurn = false;
    }

    void EventButtonsclicked(){EventButtonClicked = true;}
    void Update()
    {
        Display.UpdateCurrentState(currentState.ToString());

        if (Input.GetKeyDown("b"))
        {
            if (currentState == GameMode.Building)
            {
                currentState = GameMode.Overview;
            }
            else
            {
                currentState = GameMode.Building;
            }
        }

        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (currentState == GameMode.Overview)
                {
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int cellPos = tilemap.WorldToCell(worldPos);
                    if(Move_Menu.IsArmy(cellPos.x, cellPos.y)){
                        currentState = GameMode.PlayerMove;
                    }


                }
            }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameMode.Overview)
            {
                currentState = GameMode.Menu;
            }
            else
            {
                currentState = GameMode.Overview;
            }
        }
        
        if(currentState == GameMode.Overview){
            if(Events.CheckEvents()){
                currentState = GameMode.ResolveEvents;
            }
        }

        if(currentState == GameMode.ResolveEvents && EventButtonClicked){
            Debug.Log("A");
            if(Events.ResolveEvents()){
                Debug.Log("B");
                EventButtonClicked = false;
                currentState = GameMode.Overview;
            }
        }
    }
}
