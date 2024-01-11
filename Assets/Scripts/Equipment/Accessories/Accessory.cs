using UnityEngine;

public class Accessory : Item
{
    protected CharacterStatsController characterStatsController;
    [HideInInspector] public AccessoryType accessoryType;

    public virtual void Initialize(CharacterStatsController characterStatsController)
    {
        this.characterStatsController = characterStatsController;
        EquipmentControllerUI.Instance.AddItem(baseItemParameters.icon, GetType(), baseItemParameters.amountOfRanks);
    }

    public virtual void ApplyEffect()
    {

    }

}
