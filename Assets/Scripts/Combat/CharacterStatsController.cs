using UnityEngine;

public class CharacterStatsController : MonoBehaviour
{
    public CharacterStats characterStats { get; private set;}

    public void Initialize(CharacterStats characterStats = null)
    {
        this.characterStats = characterStats ?? new CharacterStats();   
    }

    public CharacterStats GetStats()
    {
        return characterStats;
    }

    public void AddStatValue(StatModifier statModifier, float value)
    {
        switch (statModifier)
        {
            case StatModifier.MoveSpeedModifier:
                characterStats.moveSpeedModifier += value;
                break;

            case StatModifier.WeaponSpeedModifier:
                characterStats.weaponSpeedModifier += value;
                break;

            case StatModifier.DamageModifier:
                characterStats.damageModifier += value;
                break;

            case StatModifier.CooldownModifier:
                if(characterStats.cooldownModifier - value < 0.1f) { return;}
                characterStats.cooldownModifier -= value;
                break;

            case StatModifier.PickUpRadius:
                characterStats.pickUpRadius += value;
                break;

            default:
                Debug.LogError("Invalid stat type: " + statModifier);
                break;
        }
    }
}
