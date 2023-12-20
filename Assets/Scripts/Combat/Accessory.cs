
public class Accessory : Item
{
    protected CharacterStatsController characterStatsController;

    public virtual void Initalize(CharacterStatsController characterStatsController)
    {
        this.characterStatsController = characterStatsController;
        EquipmentControllerUI.Instance.AddItem(baseItemParameters.icon, GetType(), baseItemParameters.maxRank);
    }

    public virtual void ApplyEffect()
    {

    }

}
