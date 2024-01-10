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
                    if (GameManager.Instance.gameStatsController.gameStats.enemyKilledCounts.TryGetValue(condition.enemyType, out int enemyKills) && enemyKills >= condition.amount)
                    {
                        unlockedItems.Add(item);
                    }
                    break;
                case ConditionType.UnlockedWithWeaponKills:
                    if (GameManager.Instance.gameStatsController.gameStats.weaponKillCounts.TryGetValue(condition.weaponType, out int weaponKills) && weaponKills >= condition.amount)
                    {
                        unlockedItems.Add(item);
                    }
                    break;
                case ConditionType.UnlockedWithMaxRankOfWeapon:
                    if(GameManager.Instance.gameStatsController.gameStats.itemsFullyRankedUp.Contains(condition.weaponType))
                    {
                        unlockedItems.Add(item);
                    }
                    break;
                case ConditionType.UnlockedWithMaxRankOfAccessory:
                    if (GameManager.Instance.gameStatsController.gameStats.itemsFullyRankedUp.Contains(condition.accessoryType))
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
