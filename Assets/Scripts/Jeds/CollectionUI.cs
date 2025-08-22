using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
    [Header("Collectibles Animation")]
    [SerializeField] private RectTransform first;
    [SerializeField] private RectTransform second;
    [SerializeField] private RectTransform third;
    [SerializeField] private RectTransform fourth;

    [Header("Collection Items")]
    [SerializeField] private List<CollectionItem> collectionItems = new List<CollectionItem>();

    private RectTransform[] options;
    private SaveSystem saveSystem;

    void Awake()
    {
        options = new RectTransform[] { first, second, third, fourth };
        Debug.Log("CollectionUI: Awake called");
    }

    void OnEnable()
    {
        Debug.Log("CollectionUI: OnEnable called");

        saveSystem = FindFirstObjectByType<SaveSystem>();

        if (saveSystem == null)
        {
            Debug.LogError("SaveSystem not found!");
            return;
        }

        UpdateAllItems();
        PlayEntranceAnimations();
    }

    /// <summary>
    /// Updates all collection items based on save data
    /// </summary>
    private void UpdateAllItems()
    {
        Debug.Log("CollectionUI: UpdateAllItems called");

        for (int i = 0; i < collectionItems.Count; i++)
        {
            CollectionItem item = collectionItems[i];
            bool hasItem = saveSystem.HasItem(item.itemIndex);

            Debug.Log($"CollectionUI: Item {item.itemIndex + 1}: {hasItem}");

            item.UpdateUI(hasItem);
        }
    }

    /// <summary>
    /// Plays the entrance animations for the collection items
    /// </summary>
    private void PlayEntranceAnimations()
    {
        // Kill any existing animations first
        foreach (RectTransform option in options)
        {
            option.DOKill();
        }

        // Animate each item with increasing delays
        for (int i = 0; i < options.Length; i++)
        {
            options[i].DOScale(Vector3.one, 1f)
                .SetEase(Ease.OutQuad)
                .SetDelay(i * 0.1f);
        }
    }

    private void OnDisable()
    {
        Debug.Log("CollectionUI: OnDisable called");

        // Kill animations before setting scale
        foreach (RectTransform option in options)
        {
            option.DOKill();
            option.localScale = Vector3.zero;
        }
    }

    /// <summary>
    /// Opens a specific item's popup by index
    /// </summary>
    /// <param name="itemIndex">Index in the collectionItems list</param>
    public void OpenItemPopup(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < collectionItems.Count)
        {
            CollectionItem item = collectionItems[itemIndex];
            if (item.popUpCanvas != null)
            {
                item.popUpCanvas.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Button methods for each item (for backwards compatibility)
    /// </summary>
    public void FirstButton() => OpenItemPopup(0);
    public void SecondButton() => OpenItemPopup(1);
    public void ThirdButton() => OpenItemPopup(2);
    public void FourthButton() => OpenItemPopup(3);

    /// <summary>
    /// Closes all active popups
    /// </summary>
    public void ClosePopUp()
    {
        foreach (CollectionItem item in collectionItems)
        {
            if (item.popUpCanvas != null && item.popUpCanvas.activeInHierarchy)
            {
                item.popUpCanvas.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Force refresh all items (useful when save data changes)
    /// </summary>
    public void RefreshItems()
    {
        Debug.Log("CollectionUI: RefreshItems called manually");

        if (saveSystem == null)
        {
            saveSystem = FindFirstObjectByType<SaveSystem>();
        }

        if (saveSystem != null)
        {
            UpdateAllItems();
        }
        else
        {
            Debug.LogError("CollectionUI: SaveSystem still not found during refresh!");
        }
    }

    /// <summary>
    /// Get how many items are currently obtained
    /// </summary>
    /// <returns>Number of obtained items</returns>
    public int GetObtainedItemsCount()
    {
        if (saveSystem == null) return 0;

        int count = 0;
        foreach (CollectionItem item in collectionItems)
        {
            if (saveSystem.HasItem(item.itemIndex))
                count++;
        }
        return count;
    }

    /// <summary>
    /// Check if a specific collection item is obtained
    /// </summary>
    /// <param name="listIndex">Index in the collectionItems list</param>
    /// <returns>True if obtained</returns>
    public bool IsItemObtained(int listIndex)
    {
        if (saveSystem == null || listIndex < 0 || listIndex >= collectionItems.Count)
            return false;

        return saveSystem.HasItem(collectionItems[listIndex].itemIndex);
    }
}