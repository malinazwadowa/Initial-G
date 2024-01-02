using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    public SO_ItemControllerParameters itemUnlockParameters;

    //all weapons
    //all accessories? 
    public List<GameObject> allItemPrefabs = new List<GameObject>();

    [HideInInspector] public List<Item> allItems = new List<Item>();
    //public List<GameObject> allAccessories = new List<GameObject>();

    [HideInInspector] public List<Item> unlockedItems = new List<Item>();
    //[HideInInspector] public List<GameObject> unlockedAccessories = new List<GameObject>();


    //private Dictionary<Item, bool> itemUnlockStatus = new Dictionary<Item, bool>();

    public void Initalize()
    {
        SetAllItems();
        Debug.Log(unlockedItems.Count);
        CheckItemUnlocks();
        Debug.Log(unlockedItems.Count);
    }

    private void SetAllItems()
    {
        foreach(GameObject eqItemPrefab in allItemPrefabs)
        {
            Item itemComponent = eqItemPrefab.GetComponent<Item>();

            if (itemComponent != null)
            {
                allItems.Add(itemComponent);
            }
        }
    }

    public List<Weapon> GetUnlockedWeapons()
    {
        List<Weapon> list = new List<Weapon>();
        foreach(Item item in unlockedItems)
        {
            if(item is Weapon)
            {
                list.Add((Weapon)item);
            }
        }
        return list;
    }

    public List<Accessory> GetUnlockedAccessories()
    {
        List<Accessory> list = new List<Accessory>();
        foreach (Item item in unlockedItems)
        {
            if (item is Accessory)
            {
                list.Add((Accessory)item);
            }
        }
        return list;
    }

    public List<Item> GetUnlockedItems()
    {
        return unlockedItems;
    }

    private void CheckItemUnlocks()
    {
        foreach(Item item in allItems)
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
