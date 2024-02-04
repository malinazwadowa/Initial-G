using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectTypesDatabase
{
    public static Dictionary<string, GameObject> ItemsByType { get; private set; }
    public static List<string> AccessoryTypes { get; private set; }
    public static List<string> WeaponTypes { get; private set; }

    public static Dictionary<string, AudioClip> ClipsByName { get; private set; }
    public static List<string> SoundClipNames { get; private set; }
    public static List<string> MusicClipNames { get; private set; }


    public static void SetItemsData(Dictionary<string, GameObject> typeOfItems, List<string> accessoryTypes, List<string> weaponTypes)
    {
        ItemsByType = typeOfItems;
        AccessoryTypes = accessoryTypes;
        WeaponTypes = weaponTypes;
    }

    public static void SetClipsData(List<string> soundClipNames, List<string> musicClipNames)
    {
        SoundClipNames = soundClipNames;
        MusicClipNames = musicClipNames;
    }
}
