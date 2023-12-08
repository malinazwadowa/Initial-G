using System;
using System.Collections.Generic;

public static class LevelUnlockHandler
{
    private static void SetLevelUnlocks(Dictionary<GameLevel, bool> levelUnlockStatus)
    {
        
        if (levelUnlockStatus != null)
        {
            return;
        }
        else
        {
            levelUnlockStatus = new Dictionary<GameLevel, bool>();

            foreach (GameLevel level in Enum.GetValues(typeof(GameLevel)))
            {
                levelUnlockStatus.Add(level, false);
            }

            UnlockGameLevel(GameLevel.Cementary, levelUnlockStatus);
        }
    }

    public static  void UnlockGameLevel(GameLevel levelName, Dictionary<GameLevel, bool> levelUnlockStatus)
    {
        levelUnlockStatus[levelName] = true;
    }

}
