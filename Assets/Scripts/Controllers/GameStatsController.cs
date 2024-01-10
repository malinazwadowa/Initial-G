using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsController : MonoBehaviour, ISaveable
{
    //private Dictionary<EnemyType, int> enemyKilledCounts = new Dictionary<EnemyType, int>();
    //private Dictionary<ItemType, int> weaponKillCounts = new Dictionary<ItemType, int>();
    private GameStats sessionStats;
    public GameStats gameStats { get; private set; }

    public class GameStats
    {
        public Dictionary<EnemyType, int> enemyKilledCounts;
        public Dictionary<string, int> weaponKillCounts;
        public List<string> itemsFullyRankedUp;
        public Dictionary<CollectibleType, int> collectibleCounts;

        public GameStats()
        {
            enemyKilledCounts = new Dictionary<EnemyType, int>();
            weaponKillCounts = new Dictionary<string, int>();
            itemsFullyRankedUp = new List<string>();
            collectibleCounts = new Dictionary<CollectibleType, int>();
        }
    }

    public SaveData SaveMyData()
    {
        StoreSessionStats();

        GameStatsSaveData gameStatsSaveData = new GameStatsSaveData
        {
            gameStats = this.gameStats
        };

        return gameStatsSaveData;
    }

    public void LoadMyData(SaveData savedData)
    {
        if (savedData is GameStatsSaveData gameStatsSaveData)
        {
            gameStats = gameStatsSaveData.gameStats;
        }
    }

    public int GetEnemyKilledCountOfType(EnemyType enemyType)
    {
        gameStats.enemyKilledCounts.TryGetValue(enemyType, out int value);
        return value;
    }
    
    public int GetWeaponKillCount(string weaponType)
    {
        gameStats.weaponKillCounts.TryGetValue(weaponType, out int value); 
        return value;
    }

    [Serializable]
    public class GameStatsSaveData : SaveData
    {
        public GameStats gameStats;
    }

    public void Initalize()
    {
        sessionStats = new GameStats();

        if(gameStats == null)
        {
            gameStats = new GameStats();
        }
        
    }

    private void StoreSessionStats()
    {
        foreach(EnemyType enemyType in sessionStats.enemyKilledCounts.Keys)
        {
            gameStats.enemyKilledCounts.TryGetValue(enemyType, out int currentCount);
            gameStats.enemyKilledCounts[enemyType] = currentCount + sessionStats.enemyKilledCounts[enemyType];
        }

        foreach (string itemType in sessionStats.weaponKillCounts.Keys)
        {
            gameStats.weaponKillCounts.TryGetValue(itemType, out int currentCount);
            gameStats.weaponKillCounts[itemType] = currentCount + sessionStats.weaponKillCounts[itemType];
        }

        foreach(string itemType in sessionStats.itemsFullyRankedUp)
        {
            if (!gameStats.itemsFullyRankedUp.Contains(itemType))
            {
                gameStats.itemsFullyRankedUp.Add(itemType);
            }
        }

        foreach (CollectibleType collectibleType in sessionStats.collectibleCounts.Keys)
        {
            gameStats.collectibleCounts.TryGetValue(collectibleType, out int currentCount);
            gameStats.collectibleCounts[collectibleType] = currentCount + sessionStats.collectibleCounts[collectibleType];
        }
    }

    public void RegisterFullyRankedUpItem(string type)
    {
        sessionStats.itemsFullyRankedUp.Add(type);
    }

    public void RegisterEnemyKill(EnemyType type)
    {
        sessionStats.enemyKilledCounts.TryGetValue(type, out int currentCount);
        sessionStats.enemyKilledCounts[type] = currentCount + 1;
    }

    public void RegisterWeaponKill(string type)
    {
        sessionStats.weaponKillCounts.TryGetValue(type, out int currentCount);
        sessionStats.weaponKillCounts[type] = currentCount + 1;
    }

    public void RegisterCollectiblePickUp(CollectibleType collectibleType)
    {
        sessionStats.collectibleCounts.TryGetValue(collectibleType, out int currentCount);
        sessionStats.collectibleCounts[collectibleType] = currentCount + 1;
    }
}
