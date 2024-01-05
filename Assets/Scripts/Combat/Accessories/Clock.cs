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
        accessoryType = parameters.accessoryType;
        numberOfRanks = parameters.numberOfRanks;
        ApplyEffect();
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        characterStatsController.AddStatValue(modifierType, value);
    }

    public override void RankUp()
    {
        if (currentRank < numberOfRanks - 1)
        {
            base.RankUp();
            ApplyEffect();
            Debug.Log("Ranking up Clock.");
        }
        else
        {
            Debug.Log("Maximum Clock rank reached.");
        }
    }
}
