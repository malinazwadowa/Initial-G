using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStats
{
    public float damageModifier;
    public float speedModifier;
    public float cooldownModifier;

    public CombatStats(float damageModifier, float speedModifier, float cooldownModifier)
    {
        this.damageModifier = damageModifier;
        this.speedModifier = speedModifier;
        this.cooldownModifier = cooldownModifier;
    }
}
