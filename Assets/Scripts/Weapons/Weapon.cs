using UnityEngine;

//TODO: BASE MODIFIERS CANNOT BE STATIC IN CASE I HAVE MULTIPLE WIELDERS DUH....
//PROB GONNA NEED TO POINT TO PLAYER AS SOURCE FOR THEM ? ?
//ADD EQUP BASE WEAPON WHILE YOUR AT IT I SUPPOSE
//MAYBE JUST CREATE INSTANCE OF COMBAT STATS AND POINT TO IT, EACH WIELDER WILL GET NEW INSTANCE OF STATS EASY.
public class Weapon : MonoBehaviour
{
    //public BaseWeaponData weaponData;

    protected IWeaponWielder myWeaponWielder;

    protected static float baseCooldownMultiplier = 1f;
    protected static float baseDamageMultiplier = 1f;
    protected static float baseSpeedMultiplier = 1f;

    [HideInInspector] protected int currentRank = 0;

    public void Init(IWeaponWielder myWeaponWielder)
    {
        this.myWeaponWielder = myWeaponWielder;
        //baseCooldownMultiplier = combatModifiers.cooldownModifier;
        //baseDamageMultiplier = combatModifiers.damageModifier;
        //baseSpeedMultiplier = combatModifiers.speedModifier;
    }

    public virtual void WeaponTick() 
    {
        
    }
    public virtual void RankUp() 
    { 
        currentRank++;
    }


    public void SetModifiers(CombatStats mods)
    {
        baseCooldownMultiplier = mods.cooldownModifier;
        baseDamageMultiplier = mods.damageModifier;
        baseSpeedMultiplier = mods.speedModifier;
    }


    //public virtual void 
    public virtual void Initialize() { }
}
