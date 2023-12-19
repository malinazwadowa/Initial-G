using UnityEngine;

public class CharacterStats
{
    public float moveSpeedModifier;
    public float weaponSpeedModifier;
    public float damageModifier;
    public float cooldownModifier;
    public float durationModifier;
    public float pickUpRadius;

    public CharacterStats(float moveSpeedModifier, float weaponSpeedModifier, float damageModifier, float cooldownModifier, float durationModifier, float pickUpRadius)
    {
        this.moveSpeedModifier = moveSpeedModifier;
        this.weaponSpeedModifier = weaponSpeedModifier;
        this.damageModifier = damageModifier;
        this.cooldownModifier = cooldownModifier;
        this.durationModifier = durationModifier;
        this.pickUpRadius = pickUpRadius;
    }

    public CharacterStats() : this(1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f)
    {
        Debug.Log("Initalizing character stats with default values");
    }
}
