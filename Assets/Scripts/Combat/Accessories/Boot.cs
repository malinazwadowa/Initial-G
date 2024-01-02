using UnityEngine;

public class Boot : Accessory
{
    private SO_BootParameters parameters;
    private StatModifier modifierType = StatModifier.MoveSpeedModifier;
    private float value;
    private int numberOfRanks;

    public override void Initalize(CharacterStatsController characterStatsController)
    {
        base.Initalize(characterStatsController);

        parameters = (SO_BootParameters)baseItemParameters;
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
            Debug.Log("Ranking up Boot.");
        }
        else
        {
            Debug.Log("Maximum Boot rank reached.");
        }
    }
}
