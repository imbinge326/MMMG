using UnityEngine;

public class TestTouch : MonoBehaviour
{
    public InputManager inputManager;
    private Camera camMain;

    void Awake()
    {
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
        Vector3 screenCoordinates = new Vector3(screenPos.x, screenPos.y, camMain.nearClipPlane + 10);
        Vector3 worldCoordinates = camMain.ScreenToWorldPoint(screenCoordinates);
        transform.position = worldCoordinates;
    }
}
