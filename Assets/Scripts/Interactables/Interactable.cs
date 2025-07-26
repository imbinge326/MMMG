using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject movableBlock;
    private Vector3 movableBlockCoords;
    public GameObject movableBlockScreenObj;
    private Vector3 movableBlockScreenObjCoords;
    private BlockTouch blockTouchRef;

    void Start()
    {
        movableBlockCoords = movableBlock.transform.position;
        movableBlockScreenObjCoords = movableBlockScreenObj.transform.position;
        blockTouchRef = movableBlock.GetComponent<BlockTouch>();

        if (blockTouchRef == null)
        {
            Debug.LogWarning("Block Touch not found");
        }
    }

    void OnMouseDown()
    {
        
    }
    
    void OnMouseUp()
    {
        
    }
}
