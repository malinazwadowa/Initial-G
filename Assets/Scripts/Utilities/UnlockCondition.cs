using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnlockCondition
{
    private List<string> accessoryTypes;
    private List<string> weaponTypes;


    [HideInInspector] public string name;
    public ConditionType conditionType;
    private bool unlockedByDefault;
    private bool unlockedWithWeaponKills;
    private bool unlockedWithEnemyKilled;
    //unlocked with max rank of item instead od below tbd later
    private bool unlockedWithMaxRankOfWeapon;
    private bool unlockedWithMaxRankOfAccessory;

    [AllowNesting]
    [ShowIf(EConditionOperator.Or, "unlockedWithWeaponKills", "unlockedWithMaxRankOfWeapon")][Dropdown("weaponTypes")] 
    public string weaponType;

    [AllowNesting]
    [ShowIf("unlockedWithMaxRankOfAccessory")][Dropdown("accessoryTypes")] 
    public string accessoryType;
    //public AccessoryType accessoryType;

    [AllowNesting]
    [ShowIf("unlockedWithEnemyKilled")]
    public EnemyType enemyType;

    [AllowNesting]
    [ShowIf(EConditionOperator.Or, "unlockedWithWeaponKills", "unlockedWithEnemyKilled")]
    public int amount;

    public void Validate()
    {
        accessoryTypes = ItemDatabase.AccessoryTypes;
        weaponTypes = ItemDatabase.WeaponTypes;

        if (conditionType == ConditionType.UnlockedWithWeaponKills)
        {
            unlockedWithWeaponKills = true;
            unlockedByDefault = false;
            unlockedWithEnemyKilled = false;
            unlockedWithMaxRankOfAccessory = false;
            unlockedWithMaxRankOfWeapon = false;
        }
        else if (conditionType == ConditionType.UnlockedWithEnemyKilled)
        {
            unlockedWithEnemyKilled = true;
            unlockedByDefault = false;
            unlockedWithWeaponKills = false;
            unlockedWithMaxRankOfAccessory = false;
            unlockedWithMaxRankOfWeapon = false;
        }
        else if (conditionType == ConditionType.UnlockedWithMaxRankOfAccessory)
        {
            unlockedWithMaxRankOfAccessory = true;
            unlockedByDefault = false;
            unlockedWithWeaponKills = false;
            unlockedWithEnemyKilled = false;
            unlockedWithMaxRankOfWeapon = false;
        }
        else if (conditionType == ConditionType.UnlockedByDefault)
        {
            unlockedByDefault = true;
            unlockedWithMaxRankOfAccessory = false;
            unlockedWithWeaponKills = false;
            unlockedWithEnemyKilled = false;
            unlockedWithMaxRankOfWeapon = false;
        }
        else if (conditionType == ConditionType.UnlockedWithMaxRankOfWeapon)
        {
            unlockedWithMaxRankOfWeapon = true;
            unlockedWithMaxRankOfAccessory = false;
            unlockedWithWeaponKills = false;
            unlockedWithEnemyKilled = false;
            unlockedByDefault = false;
        }
        else
        {
            unlockedByDefault = true;
        }
    }
}