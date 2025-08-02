using UnityEngine;

public class MoveInteractable : MonoBehaviour
{
    [Header("Setup")]
    [Header("Block In World")]
    [Tooltip("PreFlipFlop Coords")]
    public Vector3 movableBlockCoordsA;

    [Tooltip("PostFlipFlop Coords")]
    public Vector3 movableBlockCoordsB;

    [Header("Block On Screen")]
    public GameObject movableBlock;

    [Tooltip("PreFlipFlop Coords")]
    public GameObject movableBlockScreenObjA;
    private Vector3 movableBlockScreenObjACoords;

    [Tooltip("PostFlipFlop Coords")]
    public GameObject movableBlockScreenObjB;
    private Vector3 movableBlockScreenObjBCoords;

    private BlockTouch blockTouchRef;
    private bool flipFlop;

    void Start()
    {
        movableBlockScreenObjACoords = movableBlockScreenObjA.transform.position;
        movableBlockScreenObjBCoords = movableBlockScreenObjB.transform.position;
        blockTouchRef = movableBlock.GetComponent<BlockTouch>();

        if (blockTouchRef == null)
        {
            Debug.LogWarning("Block Touch not found");
        }
    }

    void OnMouseDown()
    {

        if (flipFlop)
        {
            blockTouchRef.correspondingUICoords = movableBlockScreenObjBCoords;
            movableBlock.transform.position = movableBlockCoordsB;
        }
        else
        {
            blockTouchRef.correspondingUICoords = movableBlockScreenObjACoords;
            movableBlock.transform.position = movableBlockCoordsA;
        }

        flipFlop = !flipFlop;
    }
    
    void OnMouseUp()
    {
        
    }
}
