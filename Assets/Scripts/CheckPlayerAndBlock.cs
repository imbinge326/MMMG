using UnityEngine;

public class CheckPlayerAndBlock : MonoBehaviour
{
    public Vector3 playerUICoords;
    public GameObject startingPointUICoords;
    public bool canWalk;
    public float distance = 10;

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
    }
}
