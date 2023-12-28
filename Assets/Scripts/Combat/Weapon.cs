using UnityEngine;

public class Weapon : Item
{
    protected IWeaponWielder weaponWielder;
    protected CharacterStats characterStats;
    [HideInInspector] public WeaponType weaponType;

    public virtual void Initialize(IWeaponWielder weaponWielder, CharacterStats characterStats)
    {
        this.weaponWielder = weaponWielder;
        this.characterStats = characterStats;
        EquipmentControllerUI.Instance.AddItem(baseItemParameters.icon, GetType(), baseItemParameters.maxRank);
    }

    public virtual void WeaponTick()
    {


    }
}
