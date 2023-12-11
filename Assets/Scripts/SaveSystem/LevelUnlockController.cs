using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockController : MonoBehaviour, ISaveable
{
    private GameLevel baseLevel = GameLevel.Cementary;
    private Dictionary<GameLevel, bool> currentLevelUnlockStatus = new Dictionary<GameLevel, bool>();

    public SaveData SaveMyData()
    {
        SaveData saveData = new LevelUnlockData
        {
            levelUnlockStatus = currentLevelUnlockStatus,
        };

        return saveData;
    }

    public void LoadMyData(SaveData savedData)
    {
        LevelUnlockData loadedData = (LevelUnlockData)savedData;
        
        currentLevelUnlockStatus = loadedData.levelUnlockStatus;

        UpdateLevelUnlockDictionary();
    }

    [Serializable]
    public class LevelUnlockData : SaveData
    {
        public Dictionary<GameLevel, bool> levelUnlockStatus;
    }

    private Dictionary<GameLevel, bool> UpdateLevelUnlockDictionary()
    {
        foreach (GameLevel level in Enum.GetValues(typeof(GameLevel)))
        {
            //levelUnlockStatus.Add(level, false);
            if (!currentLevelUnlockStatus.ContainsKey(level))
            {
                currentLevelUnlockStatus.Add(level, false);
            }
        }

        UnlockGameLevel(baseLevel, currentLevelUnlockStatus);

        return currentLevelUnlockStatus;
    }

    public void UnlockGameLevel(GameLevel levelName, Dictionary<GameLevel, bool> levelUnlockStatus)
    {
        levelUnlockStatus[levelName] = true;
    }

    public Dictionary<GameLevel, bool> GetCurrentLevelUnlockStatus()
    {
        return currentLevelUnlockStatus;
    }
}
