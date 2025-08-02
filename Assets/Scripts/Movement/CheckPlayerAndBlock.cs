using UnityEngine;

public class CheckPlayerAndBlock : MonoBehaviour
{
    [Header("Setup")]
    public static CheckPlayerAndBlock CheckPlayerAndBlockInstance { get; private set; }
    
    [Header("Screen Coords")]
    public Vector3 playerUICoords;
    public GameObject startingPointUICoords;

    [Header("Player Object")]
    public GameObject player;

    [Header("Walkable Distance")]
    public float distance = 10;

    [Header("Can Walk Bool")]
    [Tooltip("No Setup Needed Here")]
    public bool canWalk;

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

    public void CheckBlockWalkable(Vector3 blockUICoords, Transform blockRef)
    {
        if (Vector3.Distance(playerUICoords, blockUICoords) <= distance)
        {
            canWalk = true;
            playerUICoords = blockUICoords;
            player.transform.SetParent(blockRef);
        }
        else
        {
            //need set soft lock so player keeps moving to original destination
            canWalk = false;
        }
    }
}
