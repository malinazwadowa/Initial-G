using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemTypesDatabase
{
    public static Dictionary<string, GameObject> ItemsByType { get; private set; }
    public static List<string> AccessoryTypes { get; private set; }
    public static List<string> WeaponTypes { get; private set; }


    public static void SetItemsData(Dictionary<string, GameObject> typeOfItems, List<string> accessoryTypes, List<string> weaponTypes)
    {
        ItemsByType = typeOfItems;
        AccessoryTypes = accessoryTypes;
        WeaponTypes = weaponTypes;
    }
}
