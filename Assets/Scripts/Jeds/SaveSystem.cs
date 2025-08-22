using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [Header("Save Settings")]
    public int totalItems = 4;
    public static SaveSystem Instance { get; private set; }


    private GameData gameData;
    private string savePath;

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"SaveSystem: Created singleton instance on {gameObject.name}");

            // Initialize save path
            savePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
        }
        else
        {
            Debug.Log($"SaveSystem: Destroying duplicate instance on {gameObject.name}. Keeping original on {Instance.gameObject.name}");
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        LoadGameData();
    }

    public void CompleteLevel(int itemIndex)
    {
        if (itemIndex >= 0 && itemIndex < totalItems && gameData != null)
        {
            if (!gameData.obtainedItems[itemIndex])
            {
                gameData.obtainedItems[itemIndex] = true;
                gameData.totalItemsCollected++;
                SaveGameData();

                Debug.Log($"Item {itemIndex + 1} obtained! Progress: {gameData.totalItemsCollected}/{totalItems}");
            }
            else
            {
                Debug.Log($"Item {itemIndex + 1} already obtained!");
            }
        }
    }

    public bool HasItem(int itemIndex)
    {
        if (gameData != null && itemIndex >= 0 && itemIndex < totalItems)
        {
            bool hasItem = gameData.obtainedItems[itemIndex];
            Debug.Log($"SaveSystem.HasItem({itemIndex}): {hasItem}");
            return hasItem;
        }
        Debug.Log($"SaveSystem.HasItem({itemIndex}): false (gameData null or invalid index)");
        return false;
    }

    public int GetItemCount()
    {
        return gameData != null ? gameData.totalItemsCollected : 0;
    }

    public bool HasAllItems()
    {
        return GetItemCount() >= totalItems;
    }

    public List<bool> GetAllItemsStatus()
    {
        return gameData != null ? new List<bool>(gameData.obtainedItems) : new List<bool>();
    }

    void SaveGameData()
    {
        try
        {
            string jsonData = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(savePath, jsonData);
            Debug.Log($"Game saved to: {savePath}");
            Debug.Log($"Saved data: {jsonData}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save game data: {e.Message}");
        }
    }

    void LoadGameData()
    {
        try
        {
            if (File.Exists(savePath))
            {
                string jsonData = File.ReadAllText(savePath);
                gameData = JsonUtility.FromJson<GameData>(jsonData);

                Debug.Log($"Loaded data: {jsonData}");

                // Ensure the loaded data has the correct number of items
                if (gameData.obtainedItems.Count != totalItems)
                {
                    Debug.LogWarning("Save file has different number of items than expected. Recreating...");
                    gameData = new GameData(totalItems);
                }

                Debug.Log($"Game loaded from: {savePath}");
                Debug.Log($"Items collected: {gameData.totalItemsCollected}/{totalItems}");
            }
            else
            {
                Debug.Log("No save file found. Creating new game data.");
                gameData = new GameData(totalItems);
                SaveGameData(); // Create the initial save file
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load game data: {e.Message}");
            Debug.Log("Creating new game data.");
            gameData = new GameData(totalItems);
        }
    }

    [ContextMenu("Reset Progress")]
    public void ResetProgress()
    {
        Debug.Log("=== RESET PROGRESS CALLED ===");
        Debug.Log($"Before reset - Item 0: {(gameData != null && gameData.obtainedItems.Count > 0 ? gameData.obtainedItems[0].ToString() : "null")}");

        gameData = new GameData(totalItems);

        Debug.Log($"After reset - Item 0: {gameData.obtainedItems[0]}");
        Debug.Log($"After reset - Total collected: {gameData.totalItemsCollected}");

        SaveGameData();
        Debug.Log("Progress reset!");
    }

    [ContextMenu("Show Progress")]
    public void ShowProgress()
    {
        if (gameData != null)
        {
            Debug.Log($"Save file location: {savePath}");
            Debug.Log($"Current Progress: {gameData.totalItemsCollected}/{totalItems} items collected");
            for (int i = 0; i < totalItems; i++)
            {
                Debug.Log($"Item {i + 1}: {(gameData.obtainedItems[i] ? "✓" : "✗")}");
            }
        }
    }
}