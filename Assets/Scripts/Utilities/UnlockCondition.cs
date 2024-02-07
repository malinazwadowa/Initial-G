using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnlockCondition
{
    private List<string> WeaponTypes => ObjectTypesDatabase.WeaponTypes;
    private List<string> AccessoryTypes => ObjectTypesDatabase.AccessoryTypes;

    [HideInInspector] public string name;
    public ConditionType conditionType;
    private bool unlockedByDefault;
    private bool unlockedWithWeaponKills;
    private bool unlockedWithEnemyKilled;
    //unlocked with max rank of item instead od below tbd later
    private bool unlockedWithMaxRankOfWeapon;
    private bool unlockedWithMaxRankOfAccessory;
    private bool unlockedWithCollectedItems;
    
    private bool unlockedWithLevelCompletion;

    [AllowNesting]
    [ShowIf(EConditionOperator.Or, "unlockedWithWeaponKills", "unlockedWithMaxRankOfWeapon")]
    [Dropdown("WeaponTypes")] 
    public string weaponType;

    [AllowNesting]
    [ShowIf("unlockedWithMaxRankOfAccessory")]
    [Dropdown("AccessoryTypes")]
    public string accessoryType;

    [AllowNesting]
    [ShowIf("unlockedWithEnemyKilled")]
    public EnemyType enemyType;
    
    [AllowNesting]
    [ShowIf("unlockedWithCollectedItems")]
    public CollectibleType collectibleType;

    [AllowNesting]
    [ShowIf(EConditionOperator.Or, "unlockedWithWeaponKills", "unlockedWithEnemyKilled", "unlockedWithCollectedItems")]
    public int amount;

    [AllowNesting]
    [ShowIf(nameof(unlockedWithLevelCompletion))]
    public GameLevel levelToComplete;

    public void Validate()
    {
        unlockedByDefault = false;
        unlockedWithWeaponKills = false;
        unlockedWithEnemyKilled = false;
        unlockedWithMaxRankOfAccessory = false;
        unlockedWithMaxRankOfWeapon = false;
        unlockedWithCollectedItems = false;
        unlockedWithLevelCompletion = false;

        switch (conditionType)
        {
            case ConditionType.UnlockedWithWeaponKills:
                unlockedWithWeaponKills = true;
                break;

            case ConditionType.UnlockedWithEnemyKilled:
                unlockedWithEnemyKilled = true;
                break;

            case ConditionType.UnlockedWithMaxRankOfAccessory:
                unlockedWithMaxRankOfAccessory = true;
                break;

            case ConditionType.UnlockedByDefault:
                unlockedByDefault = true;
                break;

            case ConditionType.UnlockedWithMaxRankOfWeapon:
                unlockedWithMaxRankOfWeapon = true;
                break;
            
            case ConditionType.UnlockedWithCollectedItems:
                unlockedWithCollectedItems = true;
                break;
            
            case ConditionType.UnlockedWithLevelCompletion:
                unlockedWithLevelCompletion = true;
                break;

            default:
                unlockedByDefault = true;
                break;
        }
    } 
}