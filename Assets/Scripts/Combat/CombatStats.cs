using UnityEngine;
using System;

public class CombatStats
{
    public float damageModifier;
    public float speedModifier;
    public float cooldownModifier;

    public event Action OnCombatStatsChanged;
    public CombatStats(float damageModifier, float speedModifier, float cooldownModifier)
    {
        this.damageModifier = damageModifier;
        this.speedModifier = speedModifier;
        this.cooldownModifier = cooldownModifier;
    }
    public CombatStats() : this(1.0f, 1.0f, 1.0f)
    {
    }

    public void UpdateCombatStat(StatType type, float additionalValue)
    {
        switch (type)
        {
            case StatType.DamageModifier:
                damageModifier += additionalValue;
                break;
            case StatType.SpeedModifier:
                speedModifier += additionalValue;
                break;
            case StatType.CooldownModifier:
                cooldownModifier += additionalValue;
                break;
            default:
                Debug.LogError("Invalid stat type: " + type);
                break;
        }
        OnCombatStatsChanged?.Invoke();
    }
}
