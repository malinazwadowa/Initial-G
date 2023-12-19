using System.Collections.Generic;
using UnityEngine;

public class AccessoryController : MonoBehaviour
{
    private CharacterStatsController characterStatsController;

    [HideInInspector] public List<Accessory> equippedAccessories = new List<Accessory>();
    public List<GameObject> availableAccessories = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach(Accessory accessory in equippedAccessories)
            {
                accessory.RankUp();
            }
        }
    }

    public void Initialize(CharacterStatsController characterStatsController)
    {
        this.characterStatsController = characterStatsController;

        EquipAccessory<Amulet>();
        EquipAccessory<Clock>();
    }

    public void EquipAccessory<T>() where T : Accessory
    {
        GameObject accesorryPrefab = GetAccessoryPrefab<T>();
        if (accesorryPrefab != null)
        {
            GameObject accessory = Instantiate(accesorryPrefab, transform);
            T accessoryScript = accessory.GetComponent<T>();
            accessoryScript.Initalize(characterStatsController);
            equippedAccessories.Add(accessoryScript);
        }
    }

    private GameObject GetAccessoryPrefab<T>() where T : Accessory
    {
        if (availableAccessories != null)
        {
            GameObject prefab = availableAccessories.Find(weaponPrefab => weaponPrefab.GetComponent<T>() != null);
            if (prefab != null)
            {
                return prefab;
            }
            else
            {
                Debug.LogError($"Accessory prefab for type {typeof(T)} not found in the available list!");
                return null;
            }
        }
        else
        {
            Debug.Log("No avilable accessories found.");
            return null;
        }
    }
}
