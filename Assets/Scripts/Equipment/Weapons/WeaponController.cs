using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    private IItemWielder myWielder;
    private CharacterStats characterStats;
    [HideInInspector] public List<Weapon> EquippedWeapons { get; private set; } = new List<Weapon>();

    public void Initialize(IItemWielder itemWielder, CharacterStats characterStats)
    {
        myWielder = itemWielder;
        this.characterStats = characterStats;
    }

    void Update()
    {
        //TEMP Ranking up for weapons
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (Weapon weapon in EquippedWeapons)
            {
                weapon.RankUp();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            
        }
        foreach (Weapon weapon in EquippedWeapons)
        {
            weapon.WeaponTick();
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        weapon.Initialize(myWielder, characterStats);
        EquippedWeapons.Add(weapon);
    }
}


