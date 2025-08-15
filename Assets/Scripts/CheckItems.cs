using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckItems : MonoBehaviour
{
    public int winConCount;
    public WinCon winConRef;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            winConRef.items.Add(true);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("WinCon"))
        {
            if (winConRef.items.Count == winConCount)
            {
                SceneManager.LoadScene("Win");
            }
        }

    }
}
