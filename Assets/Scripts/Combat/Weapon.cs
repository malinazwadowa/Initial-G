public class Weapon : Item
{
    protected IWeaponWielder weaponWielder;
    protected CharacterStats characterStats;

    public virtual void Initialize(IWeaponWielder weaponWielder, CharacterStats characterStats)
    {
        this.weaponWielder = weaponWielder;
        this.characterStats = characterStats;
    }

    public virtual void WeaponTick()
    {


    }
}
