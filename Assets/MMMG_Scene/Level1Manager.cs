using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    private SaveSystem saveSystem;
    private bool hasItem;


    private void Start()
    {
        saveSystem = FindFirstObjectByType<SaveSystem>();

        if (saveSystem == null)
        {
            Debug.LogError("SaveSystem not found in scene!");
            return;
        }

        hasItem = saveSystem.HasItem(0);
        ItemCheck();
    }

    private void ItemCheck()
    {
        if (saveSystem != null)
        {
            if (hasItem == true)
            {
                Debug.Log("Obtained Item");
            }
            else if (hasItem == false)
            {

                Debug.Log("Not Obtained Item");
            }
        }
    }

    public void OnCompleteButtonClicked()
    {
        if (saveSystem != null)
        {
            // Give the player the item for Level 1 (index 0)
            saveSystem.CompleteLevel(0);
        }
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
