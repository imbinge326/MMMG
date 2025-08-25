using System;
using System.Collections;
using UnityEngine;
using static CheckPlayerAndBlock;
using static InputManager;

public class TouchScreenControls : MonoBehaviour
{
    private Camera camMain;
    private Vector3 newPosition;
    public float baseSpeed = 1;
    public GameObject tempCube;
    private Animator playerAnim;
    public GameObject posXRef;
    public GameObject posZRef;
    private float oriXDiff;
    private float oriZDiff;
    private float newXDiff;
    private float newZDiff;
    void Awake()
    {
        camMain = Camera.main;
    }

    void Start()
    {
        newPosition = transform.parent.position;
        playerAnim = GetComponentInChildren<Animator>();
        oriXDiff = posXRef.transform.position.x - transform.position.x;
        oriZDiff = posZRef.transform.position.z - transform.position.z;
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
            playerAnim.SetTrigger("isWalking");
            playerAnim.SetTrigger("isIdle");

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

    public void RotateCharacter()
    {
        newXDiff = posXRef.transform.position.x - transform.position.x;
        newZDiff = posZRef.transform.position.z - transform.position.z;

        if (newXDiff > oriXDiff)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, 270f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        else if (oriXDiff > newXDiff)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, 90f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        else
        {
        }

        if (newZDiff > oriZDiff)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        else if (oriZDiff > newZDiff)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        else
        {
        }

        oriXDiff = newXDiff;
        oriZDiff = newZDiff;
    }

    void Update()
    {
        if (transform.position != newPosition)
        {
            if (CheckPlayerAndBlockInstance.canWalk == true)
            {
                newPosition = transform.parent.position;
                MoveLerp(newPosition, baseSpeed);
                
                RotateCharacter();
            }
        }
    }
}
