using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsController : MonoBehaviour, ISaveable
{
    public GameStats SessionStats { get; private set; }
    public GameStats OverallStats { get; private set; }

    public class GameStats
    {
        public Dictionary<EnemyType, int> enemyKilledCounts;
        public Dictionary<string, int> weaponKillCounts;
        public List<string> itemsFullyRankedUp;
        public Dictionary<CollectibleType, int> collectibleCounts;
        public Dictionary<string, float> weaponDamageDone;
        
        public List<string> seenItems;

        public GameStats()
        {
            enemyKilledCounts = new Dictionary<EnemyType, int>();
            weaponKillCounts = new Dictionary<string, int>();
            itemsFullyRankedUp = new List<string>();
            collectibleCounts = new Dictionary<CollectibleType, int>();
            weaponDamageDone = new Dictionary<string, float>();
            seenItems = new List<string>();
        }
    }
    

    public ObjectData SaveMyData()
    {
        StoreSessionStats();

        GameStatsSaveData gameStatsSaveData = new GameStatsSaveData
        {
            gameStats = this.OverallStats
        };

        return gameStatsSaveData;
    }

    public void LoadMyData(ObjectData savedData)
    {
        if (savedData is GameStatsSaveData gameStatsSaveData)
        {
            OverallStats = gameStatsSaveData.gameStats;
        }
    }

    public void WipeMyData()
    {
        OverallStats = new GameStats();
        SessionStats = new GameStats();
    }

    [Serializable]
    public class GameStatsSaveData : ObjectData
    {
        public GameStats gameStats;
    }

    public void Initalize()
    {
        SessionStats = new GameStats();

        if(OverallStats == null)
        {
            OverallStats = new GameStats();
        }
    }

    public void StoreSessionStats()
    {
        foreach(EnemyType enemyType in SessionStats.enemyKilledCounts.Keys)
        {
            OverallStats.enemyKilledCounts.TryGetValue(enemyType, out int currentCount);
            OverallStats.enemyKilledCounts[enemyType] = currentCount + SessionStats.enemyKilledCounts[enemyType];
        }

        foreach (string itemType in SessionStats.weaponKillCounts.Keys)
        {
            OverallStats.weaponKillCounts.TryGetValue(itemType, out int currentCount);
            OverallStats.weaponKillCounts[itemType] = currentCount + SessionStats.weaponKillCounts[itemType];
        }

        foreach(string itemType in SessionStats.itemsFullyRankedUp)
        {
            if (!OverallStats.itemsFullyRankedUp.Contains(itemType))
            {
                OverallStats.itemsFullyRankedUp.Add(itemType);
            }
        }

        foreach (CollectibleType collectibleType in SessionStats.collectibleCounts.Keys)
        {
            OverallStats.collectibleCounts.TryGetValue(collectibleType, out int currentCount);
            OverallStats.collectibleCounts[collectibleType] = currentCount + SessionStats.collectibleCounts[collectibleType];
        }

        SessionStats = new GameStats();
    }

    public void RegisterFullyRankedUpItem(string type)
    {
        SessionStats.itemsFullyRankedUp.Add(type);
    }

    public void RegisterEnemyKill(EnemyType type)
    {
        SessionStats.enemyKilledCounts.TryGetValue(type, out int currentCount);
        SessionStats.enemyKilledCounts[type] = currentCount + 1;
    }

    public void RegisterWeaponKill(string type)
    {
        SessionStats.weaponKillCounts.TryGetValue(type, out int currentCount);
        SessionStats.weaponKillCounts[type] = currentCount + 1;
    }

    public void RegisterCollectiblePickUp(CollectibleType collectibleType)
    {
        SessionStats.collectibleCounts.TryGetValue(collectibleType, out int currentCount);
        SessionStats.collectibleCounts[collectibleType] = currentCount + 1;
    }

    public void RegisterWeaponDamage(string weaponType, float damageAmount)
    {
        SessionStats.weaponDamageDone.TryGetValue(weaponType, out float damageDone);
        SessionStats.weaponDamageDone[weaponType] = damageDone + damageAmount;
    }

    public int GetEnemyKilledCountOfType(EnemyType enemyType)
    {
        OverallStats.enemyKilledCounts.TryGetValue(enemyType, out int value);
        return value;
    }
    
    public int GetWeaponKillCount(string weaponType)
    {
        OverallStats.weaponKillCounts.TryGetValue(weaponType, out int value); 
        return value;
    }
}
