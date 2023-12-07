using System;
using System.Collections.Generic;

public class UnlocksManager : SingletonMonoBehaviour<UnlocksManager>
{
    private Dictionary<GameLevel, bool> LevelUnlockStatus;
    
    void Start()
    {
        SetLevelUnlocks();
    }

    private void SetLevelUnlocks()
    {
        Dictionary<GameLevel, bool> savedLevelUnlockStatus = GameManager.Instance.loadedData.levelUnlocks.LevelUnlockStatus;

        if (savedLevelUnlockStatus != null)
        {
            LevelUnlockStatus = savedLevelUnlockStatus;
        }
        else
        {
            LevelUnlockStatus = new Dictionary<GameLevel, bool>();

            foreach (GameLevel level in Enum.GetValues(typeof(GameLevel)))
            {
                LevelUnlockStatus.Add(level, false);
            }

            UnlockGameLevel(GameLevel.Cementary);
        }
    }

    public void UnlockGameLevel(GameLevel levelName)
    {
        LevelUnlockStatus[levelName] = true;
    }

    public Dictionary<GameLevel, bool> GetCurrentLevelUnlocks()
    {
        return LevelUnlockStatus;
    }
}
