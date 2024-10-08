using UnityEngine;

public class Accessory : Item
{
    protected CharacterStatsController characterStatsController;
    [HideInInspector] public AccessoryType accessoryType;

    public virtual void Initialize(CharacterStatsController characterStatsController)
    {
        this.characterStatsController = characterStatsController;
        EquipmentUI.Instance.AddItem(baseItemParameters.icon, GetType(), baseItemParameters.amountOfRanks);
        SetEquippedTime();
    }

    public virtual void ApplyEffect()
    {

    }

    public virtual (StatModifier statModifier, float value) GetParameters(int rank)
    {
        return (default(StatModifier), default(float));
    }
}
