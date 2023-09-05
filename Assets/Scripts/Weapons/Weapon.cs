using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //public BaseWeaponData weaponData;

    public float baseCooldownMultiplier = 1f;
    public float baseDamageMultiplier = 1f;
    public float baseSpeedMultiplier = 1f;

    [HideInInspector] public int currentRank = 0;

    public virtual void WeaponTick(Vector3 position, Quaternion rotation) 
    {
        
    }
    public virtual void RankUp() 
    { 
        currentRank++;
    }
    public virtual void Initialize() { }
}
