using NaughtyAttributes;
using System;
using UnityEngine;

[Serializable]
public class UnlockCondition
{
    [HideInInspector] public string name;
    public ConditionType conditionType;
    private bool unlockedByDefault;
    private bool unlockedWithWeaponKills;
    private bool unlockedWithEnemyKilled;
    //unlocked with max rank of item instead od below tbd later
    private bool unlockedWithMaxRankOfWeapon;
    private bool unlockedWithMaxRankOfAccessory;

    [AllowNesting]
    [ShowIf(EConditionOperator.Or, "unlockedWithWeaponKills", "unlockedWithMaxRankOfWeapon")]
    public WeaponType weaponType;

    [AllowNesting]
    [ShowIf("unlockedWithMaxRankOfAccessory")]
    public AccessoryType accessoryType;

    [AllowNesting]
    [ShowIf("unlockedWithEnemyKilled")]
    public EnemyType enemyType;

    [AllowNesting]
    [ShowIf(EConditionOperator.Or, "unlockedWithWeaponKills", "unlockedWithEnemyKilled")]
    public int amount;
    public void Validate()
    {
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