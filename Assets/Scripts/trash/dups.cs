using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dups : MonoBehaviour
{ /*
    private object itemUnlockParameters;
    private IEnumerable<GameObject> allWeapons;
    private object unlockedWeapons;
    private IEnumerable<GameObject> allAccessories;
    private object unlockedAccessories;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initalize()
    {
        CheckWeaponUnlocks();
        CheckAccessoryUnlocks();
    }

    private void CheckWeaponUnlocks()
    {
        foreach (GameObject weapon in allWeapons)
        {
            Weapon myScript = weapon.GetComponent<Weapon>();
            WeaponType type = myScript.weaponType;

            Condition condition = itemUnlockParameters.GetWeaponCondition(type);
            Debug.Log(condition.amount);
            switch (condition.conditionType)
            {
                case ConditionType.UnlockedByDefault:
                    unlockedWeapons.Add(weapon);
                    break;
                case ConditionType.UnlockedWithEnemyKilled:
                    if (GameManager.Instance.gameStatsController.gameStats.enemyKilledCounts[condition.enemyType] >= condition.amount)
                    {
                        unlockedWeapons.Add(weapon);
                    }
                    break;
                case ConditionType.UnlockedWithWeaponKills:
                    if (GameManager.Instance.gameStatsController.gameStats.weaponKillCounts[condition.weaponType] >= condition.amount)
                    {
                        unlockedWeapons.Add(weapon);
                    }
                    break;
                default:
                    Debug.Log("Unhandled condition");
                    break;
                    //case ConditionType.UnlockedWithMaxRankOfWeapon:
            }
        }
    }

    private void CheckAccessoryUnlocks()
    {
        foreach (GameObject accessory in allAccessories)
        {
            Accessory myScript = accessory.GetComponent<Accessory>();
            AccessoryType type = myScript.accessoryType;

            Condition condition = itemUnlockParameters.GetAccessoryCondition(type);

            switch (condition.conditionType)
            {
                case ConditionType.UnlockedByDefault:
                    unlockedAccessories.Add(accessory);
                    break;
                case ConditionType.UnlockedWithEnemyKilled:
                    if (GameManager.Instance.gameStatsController.gameStats.enemyKilledCounts[condition.enemyType] >= condition.amount)
                    {
                        unlockedAccessories.Add(accessory);
                    }
                    break;
                case ConditionType.UnlockedWithWeaponKills:
                    if (GameManager.Instance.gameStatsController.gameStats.weaponKillCounts[condition.weaponType] >= condition.amount)
                    {
                        unlockedAccessories.Add(accessory);
                    }
                    break;
                default:
                    Debug.Log("Unhandled condition");
                    break;
                    //case ConditionType.UnlockedWithMaxRankOfWeapon:
            }
        }
    } */
}
