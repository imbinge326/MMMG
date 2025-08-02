using UnityEngine;
using static CheckPlayerAndBlock;

public class BlockTouch : MonoBehaviour
{
    [Header("Block On Screen")]
    public GameObject correspondingUICoordsGameObject;
    
    [Tooltip("No Setup Needed Here")]
    public Vector3 correspondingUICoords;

    void Start()
    {
        correspondingUICoords = correspondingUICoordsGameObject.transform.position;
    }

    void OnMouseDown()
    {
        print("Mouse Click");
        CheckPlayerAndBlockInstance.CheckBlockWalkable(correspondingUICoords, this.transform);
    }

    void OnMouseUp()
    {
        print("Mouse Leave");
    }
}
