using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataController : MonoBehaviour
{
    [Expandable][SerializeField] private SO_ItemsList allItemsList;

    [HideInInspector] public List<GameObject> allItemPrefabs = new List<GameObject>() ;
    [HideInInspector] public List<Item> allItems = new List<Item>();

    [HideInInspector] public List<Item> unlockedItems = new List<Item>();
    [HideInInspector] public List<Accessory> unlockedAccessories = new List<Accessory>();
    [HideInInspector] public List<Weapon> unlockedWeapons = new List<Weapon>();

    [HideInInspector] public Dictionary<string, GameObject> typeOfItems = new Dictionary<string, GameObject>();
    [HideInInspector] public List<string> accessoryTypes = new List<string>();
    [HideInInspector] public List<string> weaponTypes = new List<string>();

    private void OnValidate()
    {
        SetDataFromSO();
    }

    public void Initalize()
    { 
        SetDataFromSO();
        CheckItemUnlocks();
        SortUnlockedItems();
    }

    public void CheckForNewUnlocks()
    {
        CheckItemUnlocks();
        SortUnlockedItems();
    }

    private void SetDataFromSO()
    {
        allItems = allItemsList.allItems;
        allItemPrefabs = allItemsList.allItemPrefabs;
        typeOfItems = allItemsList.typeOfItems;
        accessoryTypes = allItemsList.accessoryTypes;
        weaponTypes = allItemsList.weaponTypes;
    }

    public void SortUnlockedItems()
    {
        unlockedWeapons.Clear();
        unlockedAccessories.Clear();

        foreach (Item unlockedItem in unlockedItems)
        {
            if (unlockedItem is Weapon weapon)
            {
                unlockedWeapons.Add(weapon);
            }
            else if (unlockedItem is Accessory accessory)
            {
                unlockedAccessories.Add(accessory);
            }
        }
    }

    public List<Weapon> GetUnlockedWeapons()
    {
        List<Weapon> list = new List<Weapon>();
        foreach(Item item in unlockedItems)
        {
            if(item is Weapon weapon)
            {
                list.Add(weapon);
            }
        }
        return list;
    }

    public List<Accessory> GetUnlockedAccessories()
    {
        List<Accessory> list = new List<Accessory>();
        foreach (Item item in unlockedItems)
        {
            if (item is Accessory accessory)
            {
                list.Add(accessory);
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
        unlockedItems.Clear();

        foreach(Item item in allItems)
        {
            Item myScript = item.GetComponent<Item>();
            UnlockCondition condition = myScript.baseItemParameters.unlockCondition;

            switch (condition.conditionType)
            {
                case ConditionType.UnlockedByDefault:
                    UnlockItem(item);
                    break;

                case ConditionType.UnlockedWithEnemyKilled:
                    if (GameManager.Instance.gameStatsController.OverallStats.enemyKilledCounts.TryGetValue(condition.enemyType, out int enemyKills) && enemyKills >= condition.amount)
                    {
                        UnlockItem(item);
                    }
                    break;

                case ConditionType.UnlockedWithWeaponKills:
                    if (GameManager.Instance.gameStatsController.OverallStats.weaponKillCounts.TryGetValue(condition.weaponType, out int weaponKills) && weaponKills >= condition.amount)
                    {
                        UnlockItem(item);
                    }
                    break;

                case ConditionType.UnlockedWithMaxRankOfWeapon:
                    if(GameManager.Instance.gameStatsController.OverallStats.itemsFullyRankedUp.Contains(condition.weaponType))
                    {
                        UnlockItem(item);
                    }
                    break;

                case ConditionType.UnlockedWithMaxRankOfAccessory:
                    if (GameManager.Instance.gameStatsController.OverallStats.itemsFullyRankedUp.Contains(condition.accessoryType))
                    {
                        UnlockItem(item);
                    }
                    break;

                case ConditionType.UnlockedWithCollectedItems:
                    if (GameManager.Instance.gameStatsController.OverallStats.collectibleCounts.TryGetValue(condition.collectibleType, out int amount) && amount >= condition.amount)
                    {
                        //unlockedItems.Add(item);
                        UnlockItem(item);
                    }
                    break;

                default:
                    Debug.Log("Unhandled condition");
                    break;
                    //case ConditionType.UnlockedWithMaxRankOfWeapon:
            }
        }
    }

    private void UnlockItem(Item item)
    {
        if (!GameManager.Instance.gameStatsController.OverallStats.unseenItems.Contains(item.GetType().Name) && !GameManager.Instance.gameStatsController.OverallStats.seenItems.Contains(item.GetType().Name))
        {
            GameManager.Instance.gameStatsController.OverallStats.unseenItems.Add(item.GetType().Name);
        } 

        unlockedItems.Add(item);
    }
}
