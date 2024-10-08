using UnityEngine;

public class CharacterStats
{
    public float moveSpeedModifier;
    public float weaponSpeedModifier;
    public float damageModifier;
    public float cooldownModifier;
    public float durationModifier;
    public float lootingRadiusModifier;
    public float healthRegenerationModifier;

    public CharacterStats(float moveSpeedModifier, float weaponSpeedModifier, float damageModifier, float cooldownModifier, float durationModifier, float lootingRadiusModifier, float healthRegenerationModifier)
    {
        this.moveSpeedModifier = moveSpeedModifier;
        this.weaponSpeedModifier = weaponSpeedModifier;
        this.damageModifier = damageModifier;
        this.cooldownModifier = cooldownModifier;
        this.durationModifier = durationModifier;
        this.lootingRadiusModifier = lootingRadiusModifier;
        this.healthRegenerationModifier = healthRegenerationModifier;
    }

    public CharacterStats() : this(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f)
    {
        Debug.Log("Initializing character stats with default values");
    }
}
