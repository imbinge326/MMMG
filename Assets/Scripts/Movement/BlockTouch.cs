using UnityEngine;
using static CheckPlayerAndBlock;

public class BlockTouch : MonoBehaviour
{
    public GameObject correspondingUICoordsGameObject;
    public Vector3 correspondingUICoords;

    void Start()
    {
        correspondingUICoords = correspondingUICoordsGameObject.transform.position;
    }

    void OnMouseDown()
    {
        print("Mouse Click");
        CheckPlayerAndBlockInstance.CheckBlockWalkable(correspondingUICoords);
    }

    void OnMouseUp()
    {
        print("Mouse Leave");
    }
}
