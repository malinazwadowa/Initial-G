using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public BaseWeaponData weaponData;

    protected IWeaponWielder myWeaponWielder;

    public float baseCooldownMultiplier = 1f;
    public float baseDamageMultiplier = 1f;
    public float baseSpeedMultiplier = 1f;

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
    public virtual void Initialize() { }
}
