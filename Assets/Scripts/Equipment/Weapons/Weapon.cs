using UnityEngine;

public class Weapon : Item
{
    protected IItemWielder weaponWielder;
    protected CharacterStats characterStats;

    public virtual void Initialize(IItemWielder weaponWielder, CharacterStats characterStats)
    {
        this.weaponWielder = weaponWielder;
        this.characterStats = characterStats;
        EquipmentControllerUI.Instance.AddItem(baseItemParameters.icon, GetType(), baseItemParameters.amountOfRanks);
    }

    public virtual void WeaponTick()
    {


    }
}
