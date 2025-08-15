using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckItems : MonoBehaviour
{
    public WinCon winConRef;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            winConRef.itemA = true;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("WinCon"))
        {
            if (winConRef.itemA)
            {
                SceneManager.LoadScene("Win");
            }
        }

    }
}
