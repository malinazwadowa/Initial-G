using UnityEngine;

public class CharacterStatsController : MonoBehaviour
{
    public CharacterStats CharacterStats { get; private set; }

    public void Initialize(CharacterStats characterStats = null)
    {
        this.CharacterStats = characterStats ?? new CharacterStats();
    }

    public CharacterStats GetStats()
    {
        return CharacterStats;
    }

    public void AddStatValue(StatModifier statModifier, float value)
    {
        switch (statModifier)
        {
            case StatModifier.MoveSpeedModifier:
                CharacterStats.moveSpeedModifier += value;
                break;

            case StatModifier.WeaponSpeedModifier:
                CharacterStats.weaponSpeedModifier += value;
                break;

            case StatModifier.DamageModifier:
                CharacterStats.damageModifier += value;
                break;

            case StatModifier.CooldownModifier:
                if (CharacterStats.cooldownModifier - value < 0.1f) { return; }
                CharacterStats.cooldownModifier -= value;
                break;
                
            case StatModifier.LootingRadius:
                CharacterStats.lootingRadiusModifier += value;

                //HMMM
                gameObject.GetComponentInChildren<LootCollisionHandler>().UpdateRadiusValue();
                break;

            case StatModifier.RegenerationModifier:
                CharacterStats.healthRegenerationModifier += value;

                break;

            default:
                Debug.LogError("Invalid stat type: " + statModifier);
                break;
        }
    }
}
