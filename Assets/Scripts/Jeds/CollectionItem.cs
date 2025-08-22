using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class CollectionItem
{
    [Header("Item Settings")]
    [Tooltip("Index in the save system (0-3)")]
    public int itemIndex;

    [Header("UI References")]
    public Button itemButton;
    public Image itemImage;
    public TextMeshProUGUI textMeshProComponent;
    public GameObject popUpCanvas;

    [Header("Item Data")]
    public string itemName;
    public string blackName; // Name when not obtained
    public string itemDescription;
    public Sprite itemSprite;
    public Sprite blackSprite;

    [Header("Pop-up References")]
    public TextMeshProUGUI popUpName;
    public TextMeshProUGUI popUpDescription;
    public Image popUpImage;

    [Header("Colors (if no sprites)")]
    public Color obtainedColor = Color.green;
    public Color notObtainedColor = Color.red;

    /// <summary>
    /// Updates this item's UI based on whether it's obtained or not
    /// </summary>
    /// <param name="hasItem">Whether the player has this item</param>
    public void UpdateUI(bool hasItem)
    {
        if (hasItem)
        {
            // Item is obtained
            if (itemSprite != null)
            {
                itemImage.sprite = itemSprite;
                if (popUpImage != null)
                    popUpImage.sprite = itemSprite;
            }
            else
            {
                itemImage.color = obtainedColor;
                if (popUpImage != null)
                    popUpImage.color = obtainedColor;
            }

            textMeshProComponent.text = itemName;

            if (popUpName != null)
                popUpName.text = itemName;
            if (popUpDescription != null)
                popUpDescription.text = itemDescription;

            itemButton.interactable = true;
        }
        else
        {
            // Item is not obtained
            if (blackSprite != null)
            {
                itemImage.sprite = blackSprite;
            }
            else if (itemSprite != null)
            {
                itemImage.sprite = itemSprite;
                itemImage.color = notObtainedColor;
            }
            else
            {
                itemImage.color = notObtainedColor;
            }

            textMeshProComponent.text = blackName;
            itemButton.interactable = false;
        }
    }
}