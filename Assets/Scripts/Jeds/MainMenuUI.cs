using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class MainMenuUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private RectTransform startButton;
    [SerializeField] private RectTransform collectionButton;
    [SerializeField] private RectTransform resetButton;
    [SerializeField] private RectTransform exitButton;

    [Header("Canvas")]
    [SerializeField] private GameObject collectionCanvas;
    [SerializeField] private GameObject selectionCanvas;

    void OnEnable()
    {
        // Kill any existing animations first
        startButton.DOKill();
        collectionButton.DOKill();
        resetButton.DOKill();
        exitButton.DOKill();

        //Start Button
        startButton.DOAnchorPos(new Vector2(0, 200), 1.2f)
            .SetEase(Ease.OutQuad);
        //Collection Button
        collectionButton.DOAnchorPos(new Vector2(0, 0), 1.2f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.5f);
        //Setting Button
        resetButton.DOAnchorPos(new Vector2(0, -200), 1.2f)
            .SetEase(Ease.OutQuad)
            .SetDelay(0.75f);
        //Exit Button
        exitButton.DOAnchorPos(new Vector2(0, -400), 1.2f)
            .SetEase(Ease.OutQuad)
            .SetDelay(1f);
    }

    private void OnDisable()
    {
        // Kill animations before setting positions
        startButton.DOKill();
        collectionButton.DOKill();
        resetButton.DOKill();
        exitButton.DOKill();

        // Then set reset positions
        startButton.anchoredPosition = new Vector2(0, -1200);
        collectionButton.anchoredPosition = new Vector2(0, -1200);
        resetButton.anchoredPosition = new Vector2(0, -1200);
        exitButton.anchoredPosition = new Vector2(0, -1200);
    }

    public void OpenCollection()
    {
        Debug.Log("MainMenuUI: Opening Collection");
        gameObject.SetActive(false);
        collectionCanvas.SetActive(true);
    }

    public void CloseCollection()
    {
        Debug.Log("MainMenuUI: Closing Collection");
        gameObject.SetActive(true);
        collectionCanvas.SetActive(false);
    }

    public void OpenSelection()
    {
        gameObject.SetActive(false);
        selectionCanvas.SetActive(true);
    }

    public void CloseSelection()
    {
        gameObject.SetActive(true);
        selectionCanvas.SetActive(false);
    }

    /// <summary>
    /// Method to connect to your reset button
    /// </summary>
    public void ResetButtonPressed()
    {
        Debug.Log("MainMenuUI: Reset button pressed");

        SaveSystem saveSystem = FindFirstObjectByType<SaveSystem>();
        if (saveSystem != null)
        {
            saveSystem.ResetProgress();
            Debug.Log("MainMenuUI: Progress reset called");

            // Force refresh collection UI if it exists and is active
            CollectionUI collectionUI = FindFirstObjectByType<CollectionUI>();
            if (collectionUI != null)
            {
                Debug.Log("MainMenuUI: Found CollectionUI, calling RefreshItems");
                collectionUI.RefreshItems();
            }
            else
            {
                Debug.Log("MainMenuUI: CollectionUI not found or not active");
            }
        }
        else
        {
            Debug.LogError("MainMenuUI: SaveSystem not found!");
        }
    }
}