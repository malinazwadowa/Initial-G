using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataController : MonoBehaviour, ISaveable
{
    public GameLevel startingLevelType;
    [Expandable] public List<SO_GameLevel> allLevels;
    private Dictionary<GameLevel, bool> levelUnlockStatusByType = new Dictionary<GameLevel, bool>();


    public ObjectData SaveMyData()
    {
        ObjectData saveData = new LevelUnlockData
        {
            levelUnlockStatus = levelUnlockStatusByType,
        };

        return saveData;
    }

    public void LoadMyData(ObjectData savedData)
    {
        if (savedData is LevelUnlockData levelUnlockData)
        {
            levelUnlockStatusByType = levelUnlockData.levelUnlockStatus;
            UpdateLevelUnlockDictionary();
        }
    }

    public void WipeMyData()
    {
        levelUnlockStatusByType.Clear();
    }

    [Serializable]
    public class LevelUnlockData : ObjectData
    {
        public Dictionary<GameLevel, bool> levelUnlockStatus;
    }

    public Dictionary<GameLevel, bool> UpdateLevelUnlockDictionary()
    {
        foreach (SO_GameLevel level in allLevels)
        {
            if (!levelUnlockStatusByType.ContainsKey(level.type))
            {
                levelUnlockStatusByType.Add(level.type, false);
            }

            if (level.type == startingLevelType)
            {
                UnlockGameLevel(startingLevelType);
            }
            else if (GameManager.Instance.gameStatsController.OverallStats.completedLevels.Contains(level.unlockedBy))
            {
                UnlockGameLevel(level.type);
            }
        }
        return levelUnlockStatusByType;
    }

    public bool GetLevelUnlockStatus(GameLevel level)
    {
        return levelUnlockStatusByType.ContainsKey(level) ? levelUnlockStatusByType[level] : false;
    }

    public void UnlockGameLevel(GameLevel levelName)
    {
        levelUnlockStatusByType[levelName] = true;
    }

    public Dictionary<GameLevel, bool> GetLevelUnlockStatuses()
    {
        if(levelUnlockStatusByType.Count == 0)
        {
            UpdateLevelUnlockDictionary();
        }

        return levelUnlockStatusByType;
    }

    public SceneName GetLevelScene(GameLevel levelName)
    {
        foreach (SO_GameLevel level in allLevels)
        {
            if (level.type == levelName)
            {
                return level.myScene;
            }
        }
        Debug.Log("Error loading scene for the GameLevel loading MainMenu");
        return SceneName.MainMenu;
    }

    public SO_GameLevel GetCurrentLevelData()
    {
        GameLevel currentLevel = GameManager.Instance.CurrentGameLevel;
        foreach (SO_GameLevel level in allLevels)
        {
            if (level.type == currentLevel)
            {
                return level;
            }
        }
        Debug.LogError("Current level data not found in allLevels list!");
        return null;
    }
}
