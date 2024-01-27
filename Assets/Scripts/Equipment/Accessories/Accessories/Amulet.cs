using UnityEngine;

public class Amulet : Accessory
{
    private SO_AmuletParameters parameters;
    private StatModifier modifierType = StatModifier.DamageModifier;
    private float value;
    private int numberOfRanks;

    public override void Initialize(CharacterStatsController characterStatsController)
    {
        base.Initialize(characterStatsController);

        parameters = (SO_AmuletParameters)baseItemParameters;
        value = parameters.value / 100;
        numberOfRanks = parameters.numberOfRanks - 1;
        ApplyEffect();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        characterStatsController.AddStatValue(modifierType, value);
    }

    public override void RankUp()
    {
        base.RankUp();
        ApplyEffect();
    }

    public override (StatModifier statModifier, float value) GetParameters(int rank)
    {
        return (modifierType, value);
    }
}
