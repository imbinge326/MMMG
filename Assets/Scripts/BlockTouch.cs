using UnityEngine;

public class BlockTouch : MonoBehaviour
{
    public GameObject correspondingUICoordsGameObject;
    private Vector3 correspondingUICoords;
    public CheckPlayerAndBlock checkPlayerAndBlockRef;

    void Awake()
    {
        correspondingUICoords = correspondingUICoordsGameObject.transform.position;
    }

    void OnMouseDown()
    {
        print("Mouse Click");
        checkPlayerAndBlockRef.CheckBlockWalkable(correspondingUICoords);
    }

    void OnMouseUp()
    {
        print("Mouse Leave");
    }
}
