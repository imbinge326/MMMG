using UnityEngine;

public class CheckPlayerAndBlock : MonoBehaviour
{
    public static CheckPlayerAndBlock CheckPlayerAndBlockInstance { get; private set; }
    public Vector3 playerUICoords;
    public GameObject startingPointUICoords;
    public bool canWalk;
    public float distance = 10;

    void Awake()
    {
        if (CheckPlayerAndBlockInstance != null && CheckPlayerAndBlockInstance != this)
        {
            Destroy(this);
        }
        else
        {
            CheckPlayerAndBlockInstance = this;
        }
    }

    void Start()
    {
        playerUICoords = startingPointUICoords.transform.position;
    }

    public void CheckBlockWalkable(Vector3 blockUICoords)
    {
        if (Vector3.Distance(playerUICoords, blockUICoords) <= distance)
        {
            canWalk = true;
            playerUICoords = blockUICoords;
        }
        else
        {
            //need set soft lock so player keeps moving to original destination
            canWalk = false;
        }
    }
}
