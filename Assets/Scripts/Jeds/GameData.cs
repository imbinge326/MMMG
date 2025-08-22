using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameData
{
    public List<bool> obtainedItems = new List<bool>();
    public int totalItemsCollected = 0;

    // Constructor to initialize with default values
    public GameData(int totalItems)
    {
        obtainedItems = new List<bool>();
        for (int i = 0; i < totalItems; i++)
        {
            obtainedItems.Add(false);
        }
        totalItemsCollected = 0;
    }
}
