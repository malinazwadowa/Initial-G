using UnityEngine;

public class WeaponProperties
{
    public float damage;
    public float speed;
    public float cooldown;
    public float radius;
    public float duration;
    public int amount;
    public float knockbackPower;
    public GameObject prefab;
}
public class Weapon : MonoBehaviour
{
    protected IWeaponWielder myWeaponWielder;

    protected CombatStats combatStats;


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
