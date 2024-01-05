using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    public static Dictionary<string, GameObject> TypeOfItems { get; private set; }
    public static List<string> AccessoryTypes { get; private set; }
    public static List<string> WeaponTypes { get; private set; }


    public static void SetData(Dictionary<string, GameObject> typeOfItems, List<string> accessoryTypes, List<string> weaponTypes)
    {
        TypeOfItems = typeOfItems;
        AccessoryTypes = accessoryTypes;
        WeaponTypes = weaponTypes;
    }
}
