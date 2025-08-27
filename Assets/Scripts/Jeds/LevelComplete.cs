using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private int itemIndex;
    [SerializeField] private GameObject winUI;
    [SerializeField] private bool isFirstItem;
    [SerializeField] private bool isLastItem;
    [SerializeField] private GameObject itemToEnable; // Reference to the item you want to enable

    [Header("Audio Settings")]
    [SerializeField] private AudioClip firstItemSound;
    [SerializeField] private AudioClip winSound;

    private SaveSystem saveSystem;
    private bool collected = false; // Safety flag to prevent multiple triggers
    private AudioSource audioSource;

    void Start()
    {
        saveSystem = FindFirstObjectByType<SaveSystem>();
        if (saveSystem == null)
        {
            Debug.LogError("SaveSystem not found");
        }

        // Setup audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && (firstItemSound != null || winSound != null))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
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

        // Start the collection process with audio
        StartCoroutine(HandleCollection());
    }

    private IEnumerator HandleCollection()
    {
        // Play appropriate sound and wait for it to finish
        if (isLastItem || (isFirstItem && isLastItem))
        {
            // This is the last item or only item - play win sound
            if (winSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(winSound);
                yield return new WaitForSeconds(winSound.length);
            }
        }
        else if (isFirstItem)
        {
            // This is the first item - play first item sound
            if (firstItemSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(firstItemSound);
                yield return new WaitForSeconds(firstItemSound.length);
            }
        }

        // Now handle the game logic after audio finishes
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