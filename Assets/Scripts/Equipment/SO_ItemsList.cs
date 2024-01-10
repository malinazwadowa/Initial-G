using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemsList", menuName = "ScriptableObjects/ItemsList")]
public class SO_ItemsList : ScriptableObject
{
    [HideInInspector] public List<GameObject> allItemPrefabs = new List<GameObject>();

    public List<GameObject> allAccessoryPrefabs;
    public List<GameObject> allWeaponPrefabs;

    [HideInInspector] public List<Item> allItems = new List<Item>();

    [HideInInspector] public Dictionary<string, GameObject> typeOfItems = new Dictionary<string, GameObject>();
    [HideInInspector] public List<string> accessoryTypes = new List<string>();
    [HideInInspector] public List<string> weaponTypes = new List<string>();

    private void OnValidate()
    {
        CleanUpLists();
        SetAllItems();
        SetTypeOfItems();

        ItemTypesDatabase.SetItemsData(typeOfItems, accessoryTypes, weaponTypes);
    }
    private void CleanUpLists()
    {
        RemoveItemsExceptType(allAccessoryPrefabs, typeof(Accessory));
        RemoveItemsExceptType(allWeaponPrefabs, typeof(Weapon));

        allItemPrefabs.Clear();
        AddValidItemsToList(allWeaponPrefabs);
        AddValidItemsToList(allAccessoryPrefabs);
    }

    private void RemoveItemsExceptType(List<GameObject> itemList, Type targetType)
    {
        itemList.RemoveAll(item => item != null && !item.TryGetComponent(targetType, out _));
    }

    private void AddValidItemsToList(List<GameObject> itemList)
    {
        foreach (GameObject itemPrefab in itemList)
        {
            if (itemPrefab != null)
            {
                allItemPrefabs.Add(itemPrefab);
            }
        }
    }

    private void SetTypeOfItems()
    {
        typeOfItems.Clear();
        accessoryTypes.Clear();
        weaponTypes.Clear();

        foreach (Item item in allItems)
        {
            GameObject itemPrefab = item.gameObject;

            string itemType = item.GetType().Name;

            if (!typeOfItems.ContainsKey(itemType))
            {
                typeOfItems.Add(itemType, itemPrefab);

                if (item is Accessory)
                {
                    accessoryTypes.Add(itemType);
                }
                else if (item is Weapon)
                {
                    weaponTypes.Add(itemType);
                }
            }
            else
            {
                Debug.LogWarning("Duplicate itemType found: " + itemType);
            }
        }
    }

    private void SetAllItems()
    {
        allItems.Clear();
        foreach (GameObject eqItemPrefab in allItemPrefabs)
        {
            Item itemComponent = eqItemPrefab.GetComponent<Item>();

            if (itemComponent != null)
            {
                allItems.Add(itemComponent);
            }
        }
    }
}
