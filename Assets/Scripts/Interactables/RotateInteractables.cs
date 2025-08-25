using UnityEngine;
using static CheckPlayerAndBlock;

public class RotateInteractables : MonoBehaviour
{
    [Header("Setup")]

    [Header("State Enum")]
    public ChooseState chooseState;

    [Header("In World Transform")]
    public GameObject stateAObject;
    private Vector3 stateAPos;
/// <summary>
/// 
/// </summary>
    public GameObject stateBObject;
    private Vector3 stateBPos;
/// <summary>
/// 
/// </summary>
    public GameObject stateCObject;
    private Vector3 stateCPos;
/// <summary>
/// 
/// </summary>
    public GameObject stateDObject;
    private Vector3 stateDPos;

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
    private AbleToRotate ableToRotateRef;

    void Start()
    {
        stateAPos = stateAObject.transform.position;
        stateBPos = stateBObject.transform.position;
        stateCPos = stateCObject.transform.position;
        stateDPos = stateDObject.transform.position;



        stateAScreenCoords = stateAScreenObject.transform.position;
        stateBScreenCoords = stateBScreenObject.transform.position;
        stateCScreenCoords = stateCScreenObject.transform.position;
        stateDScreenCoords = stateDScreenObject.transform.position;



        blockTouchRef = rotatableBlock.GetComponent<BlockTouch>();

        if (blockTouchRef == null)
        {
            Debug.LogWarning("Block Touch not found");
        }
        
        ableToRotateRef = GetComponent<AbleToRotate>();
    }

    public void OnMouseDown()
    {
        foreach (var obj in ableToRotateRef.rotatables)
        {
            if (obj.GetComponentInChildren<TouchScreenControls>())
            {
                ableToRotateRef.canRotate = false;
                break;
            }
            else
            {
                ableToRotateRef.canRotate = true;
            }
        }
        
        if (ableToRotateRef.canRotate)
        {
            switch (chooseState)
            {
                case ChooseState.StateA:
                    blockTouchRef.correspondingUICoords = stateBScreenCoords;
                    rotatableBlock.transform.position = stateBPos;

                    chooseState = ChooseState.StateB;
                    break;

                case ChooseState.StateB:
                    blockTouchRef.correspondingUICoords = stateCScreenCoords;
                    rotatableBlock.transform.position = stateCPos;

                    chooseState = ChooseState.StateC;
                    break;

                case ChooseState.StateC:
                    blockTouchRef.correspondingUICoords = stateDScreenCoords;
                    rotatableBlock.transform.position = stateDPos;

                    chooseState = ChooseState.StateD;
                    break;

                case ChooseState.StateD:
                    blockTouchRef.correspondingUICoords = stateAScreenCoords;
                    rotatableBlock.transform.position = stateAPos;

                    chooseState = ChooseState.StateA;
                    break;
            }
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
