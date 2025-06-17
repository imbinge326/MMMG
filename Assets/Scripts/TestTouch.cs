using UnityEngine;

public class TestTouch : MonoBehaviour
{
    private InputManager inputManager;
    private Camera camMain;

    void Awake()
    {
        inputManager = InputManager.Instance;
        camMain = Camera.main;
    }

    void OnEnable()
    {
        inputManager.OnStartTouch += Move;
    }

    void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

    public void Move(Vector2 screenPos, float time)
    {
        //fix to orthographic view
        Vector3 screenCoordinates = new Vector3(screenPos.x, screenPos.y, camMain.nearClipPlane);
        Vector3 worldCoordinates = camMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        transform.position = worldCoordinates;
    }
}
