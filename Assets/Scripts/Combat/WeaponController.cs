using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    private IWeaponWielder myWeaponWielder;
    private CharacterStats characterStats;

    [HideInInspector] public List<Weapon> equippedWeapons = new List<Weapon>();
    public List<GameObject> availableWeapons = new List<GameObject>();

    public void Initalize(IWeaponWielder weaponWielder, CharacterStats characterStats)
    {
        myWeaponWielder = weaponWielder;

        this.characterStats = characterStats;

        EquipWeapon<Rock>();
        EquipWeapon<Spear>();

        EquipWeapon<Hedgehog>();
        
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            
        }
        foreach (Weapon weapon in equippedWeapons)
        {
            weapon.WeaponTick();
        }
    }

    public void EquipWeapon<T>() where T : Weapon
    {
        GameObject weaponPrefab = GetWeaponPrefab<T>();
        if(weaponPrefab != null)
        {
            GameObject weapon = Instantiate(weaponPrefab, transform);
            T weaponScript = weapon.GetComponent<T>();
            weaponScript.Initialize(myWeaponWielder, characterStats);

            if( weaponScript is Spear)
            {
                Spear spear = weaponScript as Spear;
                //EquipmentControllerUI.Instance.AddItem(spear.)
            }

            equippedWeapons.Add(weaponScript);
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


