using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject movableBlock;
    public GameObject movableBlockScreenObjA;
    private Vector3 movableBlockScreenObjACoords;
    public GameObject movableBlockScreenObjB;
    private Vector3 movableBlockScreenObjBCoords;
    public Vector3 movableBlockCoordsA;
    public Vector3 movableBlockCoordsB;
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
