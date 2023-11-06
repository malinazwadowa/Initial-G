using System.Collections.Generic;
using UnityEngine;
using System;


public class WeaponController : MonoBehaviour
{
    public List<GameObject> availableWeapons = new List<GameObject>();

    private IWeaponWielder myWeaponWielder;

    [HideInInspector] public List<Weapon> equippedWeapons = new List<Weapon>();

    private CombatStats combatStats;

    public void Init(IWeaponWielder weaponWielder, CombatStats combatStats = null)
    {
        this.combatStats = combatStats ?? new CombatStats();

        myWeaponWielder = weaponWielder;

        EquipWeapon<Hedgehog>();
        EquipWeapon<Spear>();
        EquipWeapon<Rock>();
    }

    void Update()
    {
        //TEMP Ranking up for weapons
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
}


