using UnityEngine;
using static CheckPlayerAndBlock;
using static InputManager;

public class TouchScreenControls : MonoBehaviour
{
    private Camera camMain;
    private Vector3 newPosition;
    public float baseSpeed = 1;
    public GameObject tempCube;

    void Awake()
    {
        camMain = Camera.main;
    }

    void Start()
    {
        newPosition = transform.position;
    }

    void OnEnable()
    {
        InputManagerInstance.OnStartTouch += Move;
    }

    void OnDisable()
    {
        InputManagerInstance.OnEndTouch -= Move;
    }

    public void Move(Vector3 screenPos)
    {
        Vector3 raycast = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        Ray ray = camMain.ScreenPointToRay(raycast);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Blocks")))
        {
            newPosition = hit.point;

            // remove below code in final build
            Vector3 screenPointCoords = camMain.WorldToScreenPoint(newPosition);
            screenPointCoords.z = 0;
            print(screenPointCoords);
            Instantiate(tempCube, screenPointCoords, Quaternion.identity);
            // end section

        }
        else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Interactables")))
        {
            
        }
    }

    public void MoveLerp(Vector3 moveCharacter, float speed)
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, moveCharacter.x, Time.deltaTime * speed), Mathf.Lerp(transform.position.y, moveCharacter.y, Time.deltaTime * speed), Mathf.Lerp(transform.position.z, moveCharacter.z, Time.deltaTime * speed));

    }

    void Update()
    {
        
        if (transform.position != newPosition)
        {
            if (CheckPlayerAndBlockInstance.canWalk == true)
            {
                newPosition = transform.parent.position;
                MoveLerp(newPosition, baseSpeed);
            }
        }
        /*
        else if (transform.position == newPosition)
        {
            CheckPlayerAndBlockInstance.SetCanWalkFalse();
        }
        */
    }
}
