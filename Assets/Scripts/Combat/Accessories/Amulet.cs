using UnityEngine;

public class Amulet : Accessory
{
    private SO_AmuletParameters parameters;
    private StatModifier modifierType = StatModifier.DamageModifier;
    private float value;
    private float numberOfRanks;

    public override void Initalize(CharacterStatsController characterStatsController)
    {
        base.Initalize(characterStatsController);
        parameters = ItemParametersList.Instance.SO_AmuletParameters;
        value = parameters.value;
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
