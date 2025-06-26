using UnityEditor;
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

    public void Move(Vector3 screenPos, float time)
    {
        Vector3 raycast = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        Ray ray = camMain.ScreenPointToRay(raycast);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            transform.position = hit.point;
        }
    }
}
