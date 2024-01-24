using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private IItemWielder wielder;
    private CharacterStatsController characterStatsController;
    private CharacterStats characterStats;

    [HideInInspector] public WeaponController WeaponController { get; private set; }
    [HideInInspector] public AccessoryController AccessoryController { get; private set; }
    [HideInInspector] public List<Item> EquippedItems { get; private set; } = new List<Item>();

    public void Initialize(IItemWielder itemWielder, CharacterStatsController characterStatsController)
    {
        this.wielder = itemWielder;
        this.characterStatsController = characterStatsController;
        characterStats = characterStatsController.GetStats();

        InitializeControllers();
        EquipItem(typeof(Spear));
    }

    private void InitializeControllers()
    {
        WeaponController = gameObject.AddComponent<WeaponController>();
        AccessoryController = gameObject.AddComponent<AccessoryController>();

        WeaponController.Initialize(wielder, characterStats);
        AccessoryController.Initialize(characterStatsController);
    }

    public void EquipItem(Type typeOfItem)
    {
        GameObject itemPrefab = GetItemPrefab(typeOfItem);

        if (itemPrefab.TryGetComponent<Item>(out Item itemScript))
        {
            Item instantiatedItem = InstantiateItem(itemPrefab);

            if (itemScript is Weapon)
            {
                WeaponController.EquipWeapon(instantiatedItem as Weapon);
            }
            else if (itemScript is Accessory)
            {
                AccessoryController.EquipAccessory(instantiatedItem as Accessory);
            }

            EquippedItems.Add(instantiatedItem);
        }
    }

    private Item InstantiateItem(GameObject itemPrefab)
    {
        GameObject itemObject = Instantiate(itemPrefab, transform);
        var itemScript = itemObject.GetComponent<Item>();

        return itemScript;
    }

    public List<Item> GetUpgradableItems()
    {
        List<Item> items = new List<Item>();

        foreach (Item item in EquippedItems)
        {
            if (item.CurrentRank < item.baseItemParameters.amountOfRanks - 1)
            {
                items.Add(item);
            }
        }
        
        return items;
    }

    public List<Item> GetEquippableItems()
    {
        List<Item> equippableItems = new();

        if (WeaponController.EquippedWeapons.Count < 4)
        {
            List<Weapon> weapons = GameManager.Instance.itemDataController.GetUnlockedWeapons();
            foreach (Weapon weapon in weapons)
            {
                if (!EquippedItems.Any(equippedItem => equippedItem.GetType() == weapon.GetType()))
                {
                    equippableItems.Add(weapon);
                }
            }
        }

        if (AccessoryController.EquippedAccessories.Count < 4)
        {
            List<Accessory> accessories = GameManager.Instance.itemDataController.GetUnlockedAccessories();

            foreach (Accessory accessory in accessories)
            {
                if (!EquippedItems.Any(equippedItem => equippedItem.GetType() == accessory.GetType()))
                {
                    equippableItems.Add(accessory);
                }
            }
        }

        return equippableItems;
    }

    private GameObject GetItemPrefab(Type itemType)
    {
        if (GameManager.Instance.itemDataController.allItemPrefabs != null)
        {
            GameObject prefab = GameManager.Instance.itemDataController.allItemPrefabs.Find(itemPrefab => itemPrefab.GetComponent(itemType) != null);

            if (prefab != null)
            {
                return prefab;
            }
            else
            {
                Debug.LogError($"Prefab for type {itemType} not found in the item prefabs list!");
                return null;
            }
        }
        else
        {
            Debug.LogError("No item prefabs list!");
            return null;
        }
    }
}
