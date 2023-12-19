using UnityEngine;

public class Clock : Accessory
{
    private SO_ClockParameters parameters;
    private StatModifier modifierType = StatModifier.CooldownModifier;
    private float value;
    private float numberOfRanks;

    public override void Initalize(CharacterStatsController characterStatsController)
    {
        base.Initalize(characterStatsController);
        parameters = ItemParametersList.Instance.SO_ClockParameters;
        value = parameters.value / 100;
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
