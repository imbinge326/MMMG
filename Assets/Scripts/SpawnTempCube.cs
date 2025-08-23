using Unity.VisualScripting;
using UnityEngine;

public class SpawnTempCube : MonoBehaviour
{
    private float time = 2;
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            //Destroy(this.gameObject);
        }
    }
}
