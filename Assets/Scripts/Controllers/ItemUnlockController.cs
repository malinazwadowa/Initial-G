using System.Collections.Generic;
using UnityEngine;

public class ItemUnlockController : MonoBehaviour
{
    public SO_ItemUnlockParameters itemUnlockParameters;

    //all weapons
    //all accessories? 
    public List<GameObject> allItems = new List<GameObject>();
    //public List<GameObject> allAccessories = new List<GameObject>();

    [HideInInspector] public List<GameObject> unlockedItems = new List<GameObject>();
    //[HideInInspector] public List<GameObject> unlockedAccessories = new List<GameObject>();


    //private Dictionary<Item, bool> itemUnlockStatus = new Dictionary<Item, bool>();

    public void Initalize()
    {
        Debug.Log(unlockedItems.Count);
        CheckItemUnlocks();
        Debug.Log(unlockedItems.Count);
    }

    private void CheckItemUnlocks()
    {
        foreach(GameObject item in allItems)
        {
            Item myScript = item.GetComponent<Item>();
            UnlockCondition condition = myScript.baseItemParameters.unlockCondition;

            switch (condition.conditionType)
            {
                case ConditionType.UnlockedByDefault:
                    unlockedItems.Add(item);
                    break;
                case ConditionType.UnlockedWithEnemyKilled:
                    if (GameManager.Instance.gameStatsController.gameStats.enemyKilledCounts[condition.enemyType] >= condition.amount)
                    {
                        unlockedItems.Add(item);
                    }
                    break;
                case ConditionType.UnlockedWithWeaponKills:
                    if (GameManager.Instance.gameStatsController.gameStats.weaponKillCounts[condition.weaponType] >= condition.amount)
                    {
                        unlockedItems.Add(item);
                    }
                    break;
                default:
                    Debug.Log("Unhandled condition");
                    break;
                    //case ConditionType.UnlockedWithMaxRankOfWeapon:
            }
        }
    }
}
