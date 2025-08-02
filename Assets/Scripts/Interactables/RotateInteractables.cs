using UnityEngine;

public class RotateInteractables : MonoBehaviour
{
    [Header("Setup")]

    [Header("State Enum")]
    public ChooseState chooseState;

    [Header("In World Transform")]
    public GameObject stateAObject;
    private Transform stateATransform;
/// <summary>
/// 
/// </summary>
    public GameObject stateBObject;
    private Transform stateBTransform;
/// <summary>
/// 
/// </summary>
    public GameObject stateCObject;
    private Transform stateCTransform;
/// <summary>
/// 
/// </summary>
    public GameObject stateDObject;
    private Transform stateDTransform;

    [Header("On Screen Coords")]
    public GameObject stateAScreenObject;
    private Vector3 stateAScreenCoords;
/// <summary>
/// 
/// </summary>
    public GameObject stateBScreenObject;
    private Vector3 stateBScreenCoords;
/// <summary>
/// 
/// </summary>
    public GameObject stateCScreenObject;
    private Vector3 stateCScreenCoords;
/// <summary>
/// 
/// </summary>
    public GameObject stateDScreenObject;
    private Vector3 stateDScreenCoords;
    

    [Header("Rotatable Block")]
    public GameObject rotatableBlock;
    private BlockTouch blockTouchRef;

    void Start()
    {
        stateATransform = stateAObject.transform;
        stateBTransform = stateBObject.transform;
        stateCTransform = stateCObject.transform;
        stateDTransform = stateDObject.transform;
        

        stateAScreenCoords = stateAScreenObject.transform.position;
        stateBScreenCoords = stateBScreenObject.transform.position;
        stateCScreenCoords = stateCScreenObject.transform.position;
        stateDScreenCoords = stateDScreenObject.transform.position;



        blockTouchRef = rotatableBlock.GetComponent<BlockTouch>();

        if (blockTouchRef == null)
        {
            Debug.LogWarning("Block Touch not found");
        }
    }

    public void OnMouseDown()
    {
        switch (chooseState)
        {
            case ChooseState.StateA:
                blockTouchRef.correspondingUICoords = stateBScreenCoords;
                rotatableBlock.transform.position = stateBTransform.position;
                rotatableBlock.transform.rotation = stateBTransform.rotation;
                chooseState = ChooseState.StateB;
                break;

            case ChooseState.StateB:
                blockTouchRef.correspondingUICoords = stateCScreenCoords;
                rotatableBlock.transform.position = stateCTransform.position;
                rotatableBlock.transform.rotation = stateCTransform.rotation;
                chooseState = ChooseState.StateC;
                break;

            case ChooseState.StateC:
                blockTouchRef.correspondingUICoords = stateDScreenCoords;
                rotatableBlock.transform.position = stateDTransform.position;
                rotatableBlock.transform.rotation = stateDTransform.rotation;
                chooseState = ChooseState.StateD;
                break;

            case ChooseState.StateD:
                blockTouchRef.correspondingUICoords = stateAScreenCoords;
                rotatableBlock.transform.position = stateATransform.position;
                rotatableBlock.transform.rotation = stateATransform.rotation;
                chooseState = ChooseState.StateA;
                break;
        }
    }
}

public enum ChooseState
{
    StateA,
    StateB,
    StateC,
    StateD
}
