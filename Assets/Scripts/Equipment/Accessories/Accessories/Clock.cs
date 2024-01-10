using UnityEngine;

public class Clock : Accessory
{
    private SO_ClockParameters parameters;
    private StatModifier modifierType = StatModifier.CooldownModifier;
    private float value;
    private int numberOfRanks;

    public override void Initialize(CharacterStatsController characterStatsController)
    {
        base.Initialize(characterStatsController);

        parameters = (SO_ClockParameters)baseItemParameters;
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
}
