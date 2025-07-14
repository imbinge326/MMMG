using UnityEditor;
using UnityEngine;

public class TouchScreenControls : MonoBehaviour
{
    public InputManager inputManager;
    private Camera camMain;
    private Vector3 newPosition;
    public float baseSpeed = 1;
    public GameObject tempCube;
    public CheckPlayerAndBlock checkPlayerAndBlockRef;

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
        inputManager.OnStartTouch += Move;
    }

    void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

    public void Move(Vector3 screenPos, float time)
    {
        Vector3 raycast = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        Ray ray = camMain.ScreenPointToRay(raycast);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            newPosition = hit.point;
            Vector3 screenPointCoords = camMain.WorldToScreenPoint(newPosition);
            screenPointCoords.z = 0;
            print(screenPointCoords);
            Instantiate(tempCube, screenPointCoords, Quaternion.identity);
        }
    }

    public void MoveLerp(Vector3 moveCharacter, float speed)
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, moveCharacter.x, Time.deltaTime * speed), Mathf.Lerp(transform.position.y, moveCharacter.y, Time.deltaTime * speed), Mathf.Lerp(transform.position.z, moveCharacter.z, Time.deltaTime * speed));

        if (Time.deltaTime <= 0)
        {
            checkPlayerAndBlockRef.canWalk = false;
        }
    }

    void Update()
    {
        if (transform.position != newPosition)
        {
            if (checkPlayerAndBlockRef.canWalk == true)
            {
                MoveLerp(newPosition, baseSpeed);
            }
        }

    }
}
