using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlockController : MonoBehaviour, ISaveable
{
    public SceneName startingLevel;

    private Dictionary<SceneName, bool> currentLevelUnlockStatus = new Dictionary<SceneName, bool>();


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
        currentLevelUnlockStatus.Clear();
        UpdateLevelUnlockDictionary();
    }

    [Serializable]
    public class LevelUnlockData : ObjectData
    {
        public Dictionary<SceneName, bool> levelUnlockStatus;
    }

    private void Start()
    {
        UpdateLevelUnlockDictionary();
    }

    private Dictionary<SceneName, bool> UpdateLevelUnlockDictionary()
    {
        foreach (SceneName scene in Enum.GetValues(typeof(SceneName)))
        {
            if(scene == SceneName.MainMenu) { continue; }

            if (!currentLevelUnlockStatus.ContainsKey(scene))
            {
                currentLevelUnlockStatus.Add(scene, false);
                if(scene  == startingLevel) { UnlockGameLevel(startingLevel); }
            }
        }
        return currentLevelUnlockStatus;
    }

    public void UnlockGameLevel(SceneName levelName)
    {
        currentLevelUnlockStatus[levelName] = true;
    }

    public Dictionary<SceneName, bool> GetCurrentLevelUnlockStatus()
    {
        return currentLevelUnlockStatus;
    }
}
