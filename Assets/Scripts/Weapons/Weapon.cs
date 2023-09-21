using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public BaseWeaponData weaponData;

    protected IWeaponWielder myWeaponWielder;

    public static float baseCooldownMultiplier = 1f;
    public static float baseDamageMultiplier = 1f;
    public static float baseSpeedMultiplier = 1f;

    [HideInInspector] public int currentRank = 0;

    public void Init(IWeaponWielder myWeaponWielder)
    {
        this.myWeaponWielder = myWeaponWielder;
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
