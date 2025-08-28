
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private RectTransform level1;
    [SerializeField] private RectTransform level2;
    [SerializeField] private RectTransform level3;

    [Header("Level Buttons")]
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;

    [Header("Level Icons")]
    [SerializeField] private Image level1Icon;
    [SerializeField] private Image level2Icon;
    [SerializeField] private Image level3Icon;

    [Header("Icon Sprites")]
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private Sprite level1UnlockedSprite;
    [SerializeField] private Sprite level2UnlockedSprite;
    [SerializeField] private Sprite level3UnlockedSprite;

    private RectTransform[] options;
    private SaveSystem saveSystem;

    void Awake()
    {
        options = new RectTransform[] { level1, level2, level3 };

        // Get reference to SaveSystem
        saveSystem = FindFirstObjectByType<SaveSystem>();
        if (saveSystem == null)
        {
            Debug.LogError("SaveSystem not found in LevelSelection!");
        }
    }

    void OnEnable()
    {
        // Update level availability first
        UpdateLevelAvailability();

        // Kill any existing animations first
        foreach (RectTransform option in options)
        {
            option.DOKill();
        }

        // Animate levels (only animate first 3 since level4 is not used)
        level1.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutQuad);

        level2.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.1f);

        level3.DOScale(Vector3.one, 1f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.2f);
    }

    private void OnDisable()
    {
        // Kill animations before setting scale
        foreach (RectTransform option in options)
        {
            option.DOKill();
            option.localScale = Vector3.zero;
        }
    }

    private void UpdateLevelAvailability()
    {
        if (saveSystem == null) return;

        // Level 1 is always available
        level1Button.interactable = true;
        level1Icon.sprite = level1UnlockedSprite;

        // Level 2 unlocks when player has itemIndex 0
        bool hasItem0 = saveSystem.HasItem(0);
        level2Button.interactable = hasItem0;
        level2Icon.sprite = hasItem0 ? level2UnlockedSprite : lockSprite;

        // Level 3 unlocks when player has itemIndex 1
        bool hasItem1 = saveSystem.HasItem(1);
        level3Button.interactable = hasItem1;
        level3Icon.sprite = hasItem1 ? level3UnlockedSprite : lockSprite;

        // Debug info
        Debug.Log($"Level availability - Item 0: {hasItem0}, Item 1: {hasItem1}");
        Debug.Log($"Buttons - Level 1: Always, Level 2: {(hasItem0 ? "Unlocked" : "Locked")}, Level 3: {(hasItem1 ? "Unlocked" : "Locked")}");
    }

    public void Level1Button()
    {
        SceneManager.LoadScene(1);
    }

    public void Level2Button()
    {
        // Only load if unlocked (extra safety check)
        if (saveSystem != null && saveSystem.HasItem(0))
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("Level 2 is still locked!");
        }
    }

    public void Level3Button()
    {
        // Only load if unlocked (extra safety check)
        if (saveSystem != null && saveSystem.HasItem(1))
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            Debug.Log("Level 3 is still locked!");
        }
    }
}