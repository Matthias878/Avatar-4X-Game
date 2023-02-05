using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Build_menu : MonoBehaviour
{ 
    public Tilemap tilemap;
    public GameObject Build_Menu;

    void Start()
    {
    }

    void Update()
    {

        if (!(Controller.currentState == Controller.GameMode.Building))
        {
            Debug.Log("A");
            Build_Menu.SetActive(false);
            return;
        }
        else
        {
            Debug.Log("B");
            Build_Menu.SetActive(true);
        }


        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cellPos);
        cellCenterPos.z = 1;
        Build_Menu.transform.position = cellCenterPos;
    


       /* if (Input.GetMouseButtonDown(0))
        {
            OnLeftClick();
        }*/
    }

    void OnLeftClick()
    {
       
    }
}
