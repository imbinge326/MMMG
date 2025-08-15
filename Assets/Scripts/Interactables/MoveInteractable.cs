using UnityEngine;
using static CheckPlayerAndBlock;

public class MoveInteractable : MonoBehaviour
{
    [Header("Setup")]
    [Header("Block In World")]
    [Tooltip("PreFlipFlop Coords")]
    public GameObject movableBlockA;
    private Vector3 movableBlockCoordsA;

    [Tooltip("PostFlipFlop Coords")]
    public GameObject movableBlockB;
    private Vector3 movableBlockCoordsB;

    [Header("Block On Screen")]
    public GameObject movableBlock;

    [Tooltip("PreFlipFlop Coords")]
    public GameObject movableBlockScreenObjA;
    private Vector3 movableBlockScreenObjACoords;

    [Tooltip("PostFlipFlop Coords")]
    public GameObject movableBlockScreenObjB;
    private Vector3 movableBlockScreenObjBCoords;

    private BlockTouch blockTouchRef;
    private bool flipFlop = true;

    void Start()
    {
        movableBlockCoordsA = movableBlockA.transform.position;
        movableBlockCoordsB = movableBlockB.transform.position;

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
            CheckPlayerAndBlockInstance.playerUICoords = movableBlockScreenObjBCoords;
            movableBlock.transform.position = movableBlockCoordsB;
        }
        else
        {
            blockTouchRef.correspondingUICoords = movableBlockScreenObjACoords;
            CheckPlayerAndBlockInstance.playerUICoords = movableBlockScreenObjACoords;
            movableBlock.transform.position = movableBlockCoordsA;
        }

        flipFlop = !flipFlop;
    }
    
}
