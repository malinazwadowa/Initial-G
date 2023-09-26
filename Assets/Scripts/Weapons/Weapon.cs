using UnityEngine;
using System;
public class WeaponProperties
{
    public float damage;
    public float speed;
    public float cooldown;
    public int amount;
    public float strength;
    public GameObject prefab;
}
public class Weapon : MonoBehaviour
{
    protected IWeaponWielder myWeaponWielder;

    protected CombatStats combatStats;
    //protected WeaponController weaponController;


    [HideInInspector] protected int currentRank = 0;

    public void Init(IWeaponWielder myWeaponWielder, CombatStats combatStats = null)
    {
        this.combatStats = combatStats ?? new CombatStats();
        this.myWeaponWielder = myWeaponWielder;
        //this.weaponController = weaponController;

        combatStats.OnCombatStatsChanged += SetCurrentProperties;
    }

    public virtual void WeaponTick() 
    {
        
    }
    public virtual void RankUp() 
    { 
        currentRank++;
    }
    public virtual void SetCurrentProperties()
    {

    }
}
