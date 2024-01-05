using UnityEngine;

public class Hook : Accessory
{
    private SO_HookParameters parameters;
    private StatModifier modifierType = StatModifier.LootingRadius;
    private float value;
    private int numberOfRanks;

    public override void Initialize(CharacterStatsController characterStatsController)
    {
        base.Initialize(characterStatsController);

        parameters = (SO_HookParameters)baseItemParameters;
        value = parameters.value;
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
            Debug.Log("Ranking up hook.");
        }
        else
        {
            Debug.Log("Maximum Hook rank reached.");
        }
    }
}
