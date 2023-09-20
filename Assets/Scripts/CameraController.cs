using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float zoomSpeed = 4000.0f;
    public float zoomMin = 10f;
    public float zoomMax = 80f;


    void Update()
    {
        if (Input_Menu.currentState != Input_Menu.GameMode.Overview)
            return;
        // Scroll to zoom in and out


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if((Camera.main.orthographicSize  -(scroll * Time.deltaTime * zoomSpeed))>0){
        Camera.main.orthographicSize -= scroll * Time.deltaTime * zoomSpeed;
        }


        // Move camera with arrow keys or mouse position at the edge of the screen
        if (Input.GetKey("w") || (Input.mousePosition.y >= Screen.height - panBorderThickness && Input.mousePosition.y <= Screen.height))
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || (Input.mousePosition.y >= 0 && Input.mousePosition.y <= panBorderThickness))
        {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || (Input.mousePosition.x >= Screen.width - panBorderThickness && Input.mousePosition.x <= Screen.width))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || (Input.mousePosition.x >= 0 && Input.mousePosition.x <= panBorderThickness))
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    }

    }
