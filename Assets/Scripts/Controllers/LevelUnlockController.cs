using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockController : MonoBehaviour, ISaveable
{
    private GameLevel startingLevel = GameLevel.Cementary;
    private Dictionary<GameLevel, bool> currentLevelUnlockStatus = new Dictionary<GameLevel, bool>();


    public ObjectData SaveMyData()
    {
        ObjectData saveData = new LevelUnlockData
        {
            levelUnlockStatus = currentLevelUnlockStatus,
        };

        return saveData;
    }

    public void LoadMyData(ObjectData savedData)
    {
        if (savedData is LevelUnlockData levelUnlockData)
        {
            currentLevelUnlockStatus = levelUnlockData.levelUnlockStatus;
            UpdateLevelUnlockDictionary();
        }
    }

    public void WipeMyData()
    {
        Debug.Log("my data is wiped leveunlockcontr)");
        currentLevelUnlockStatus.Clear();
        UpdateLevelUnlockDictionary();
    }

    [Serializable]
    public class LevelUnlockData : ObjectData
    {
        public Dictionary<GameLevel, bool> levelUnlockStatus;
    }

    private void Start()
    {
        UpdateLevelUnlockDictionary();
    }

    private Dictionary<GameLevel, bool> UpdateLevelUnlockDictionary()
    {
        foreach (GameLevel level in Enum.GetValues(typeof(GameLevel)))
        {
            if (!currentLevelUnlockStatus.ContainsKey(level))
            {
                currentLevelUnlockStatus.Add(level, false);
                if(level  == startingLevel) { UnlockGameLevel(startingLevel); }
            }
        }
        return currentLevelUnlockStatus;
    }

    public void UnlockGameLevel(GameLevel levelName)
    {
        currentLevelUnlockStatus[levelName] = true;
    }

    public Dictionary<GameLevel, bool> GetCurrentLevelUnlockStatus()
    {
        return currentLevelUnlockStatus;
    }
}
