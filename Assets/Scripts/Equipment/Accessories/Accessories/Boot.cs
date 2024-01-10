using UnityEngine;

public class Boot : Accessory
{
    private SO_BootParameters parameters;
    private StatModifier modifierType = StatModifier.MoveSpeedModifier;
    private float value;
    private int numberOfRanks;

    public override void Initialize(CharacterStatsController characterStatsController)
    {
        base.Initialize(characterStatsController);

        parameters = (SO_BootParameters)baseItemParameters;
        value = parameters.value;
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
