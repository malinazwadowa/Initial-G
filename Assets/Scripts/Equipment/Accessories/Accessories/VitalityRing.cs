public class VitalityRing : Accessory
{
    private SO_VitalityRingParameters parameters;
    private StatModifier modifierType = StatModifier.RegenerationModifier;
    private float value;
    private int numberOfRanks;

    public override void Initialize(CharacterStatsController characterStatsController)
    {
        base.Initialize(characterStatsController);

        parameters = (SO_VitalityRingParameters)baseItemParameters;
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
