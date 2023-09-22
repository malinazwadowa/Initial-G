using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStats
{
    public float damageModifier = 1.0f;
    public float speedModifier = 1.0f;
    public float cooldownModifier = 1.0f;

    public CombatStats(float damageModifier, float speedModifier, float cooldownModifier)
    {
        this.damageModifier = damageModifier;
        this.speedModifier = speedModifier;
        this.cooldownModifier = cooldownModifier;
    }
}
