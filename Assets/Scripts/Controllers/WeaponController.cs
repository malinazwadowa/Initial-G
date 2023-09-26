using System.Collections.Generic;
using UnityEngine;
using System;


public class WeaponController : MonoBehaviour
{
    public List<GameObject> availableWeapons = new List<GameObject>();

    private IWeaponWielder myWeaponWielder;

    private List<Weapon> equippedWeapons = new List<Weapon>();


    private CombatStats combatStats;
    //public event Action OnCombatStatsChanged;

    public void Init(IWeaponWielder weaponWielder, CombatStats combatStats = null)
    {
        this.combatStats = combatStats ?? new CombatStats();

        myWeaponWielder = weaponWielder;
        

        EquipWeapon<Spear>();
        EquipWeapon<Rock>();
    }

    void Update()
    {
        Debug.Log("WEaponControllers combaststs: " + + combatStats.damageModifier);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (Weapon weapon in equippedWeapons)
            {
                weapon.RankUp();
            }
        }
        foreach (Weapon weapon in equippedWeapons)
        {
            weapon.WeaponTick();
        }
    }
    private void AddWeapon(Weapon weapon)
    {
        equippedWeapons.Add(weapon);
    }

    public void EquipWeapon<T>() where T : Weapon
    {
        GameObject weaponPrefab = GetWeaponPrefab<T>();
        if(weaponPrefab != null)
        {
            GameObject weapon = Instantiate(weaponPrefab, transform);
            T weaponScript = weapon.GetComponent<T>();
            weaponScript.Init(myWeaponWielder, combatStats);
            AddWeapon(weaponScript);
        }
        
    }
    private GameObject GetWeaponPrefab<T>() where T : Weapon
    {
        if (availableWeapons != null)
        {
            GameObject prefab = availableWeapons.Find(weaponPrefab => weaponPrefab.GetComponent<T>() != null);
            if (prefab != null)
            {
                return prefab;
            }
            else
            {
                Debug.LogError($"Weapon prefab for type {typeof(T)} not found in the available list!");
                return null;
            }
        }
        else
        {
            Debug.Log("No avilable weapons.");
            return null;
        }
    }
    /*
    public void UpdateCombatStat(StatType type, float additionalValue)
    {
        switch (type)
        {
            case StatType.DamageModifier:
                combatStats.damageModifier += additionalValue;
                break;
            case StatType.SpeedModifier:
                combatStats.speedModifier += additionalValue;
                break;
            case StatType.CooldownModifier:
                combatStats.cooldownModifier += additionalValue;
                break;
            default:
                Debug.LogError("Invalid stat type: " + type);
                break;
        }
        OnCombatStatsChanged?.Invoke();
    } */
}


