using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private int itemIndex;
    [SerializeField] private GameObject winUI;
    [SerializeField] private bool isFirstItem;
    [SerializeField] private bool isLastItem;
    [SerializeField] private GameObject itemToEnable; // Reference to the item you want to enable
    private SaveSystem saveSystem;
    private bool collected = false; // Safety flag to prevent multiple triggers

    void Start()
    {
        saveSystem = FindFirstObjectByType<SaveSystem>();
        if (saveSystem == null)
        {
            Debug.LogError("SaveSystem not found");
        }

        // Only deactivate if it's ONLY the last item (not both first AND last)
        if (isLastItem && !isFirstItem)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected || !other.gameObject.CompareTag("Player")) return;
        collected = true;

        // Always complete the level first
        saveSystem.CompleteLevel(itemIndex);

        if (isFirstItem && isLastItem)
        {
            // Single item in scene - show win UI and destroy
            winUI.SetActive(true);
            Destroy(this.gameObject);
        }
        else if (isFirstItem)
        {
            // First item - enable the next item and destroy this one
            if (itemToEnable != null)
            {
                itemToEnable.SetActive(true);
                Debug.Log($"Enabled next item: {itemToEnable.name}");
            }
            else
            {
                Debug.LogWarning("itemToEnable is not assigned for first item!");
            }
            Destroy(this.gameObject);
        }
        else if (isLastItem)
        {
            // Last item - show win UI and destroy
            winUI.SetActive(true);
            Destroy(this.gameObject);
        }
        else
        {
            // Regular item (neither first nor last) - just show win UI and destroy
            winUI.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}