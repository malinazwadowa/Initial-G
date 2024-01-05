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
            Debug.Log("Ranking up amuleto.");
        }
        else
        {
            Debug.Log("Maximum Amulet rank reached.");
        }
    }
}
