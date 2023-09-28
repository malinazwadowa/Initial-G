using UnityEngine;
using System;


[CreateAssetMenu(fileName = "newRockData", menuName = "Weapon Data/Rock Data/Base Data")]

//Possibly should be changed to generic WeaponData with optional fields. Or should it ? O.o 
public class RockData : ScriptableObject
{
    public event Action OnWeaponDataChanged;
    private void OnValidate()
    {
        for (int i = 0; i < rockRanks.Length; ++i)
        {
            rockRanks[i].name = "Rank " + (i + 1);
        }
        OnWeaponDataChanged?.Invoke();
    }

    [Header("Rank independend settings")]
    public float spawnDelayForAdditionalRocks;
    public float spawnOffsetRangeForAdditionalRocks;

    [Header("Settings for each rank")]
    public RockRank[] rockRanks;
}
[System.Serializable]
public class RockRank
{
    [HideInInspector] public string name;

    public GameObject projectilePrefab;
    public float speed;
    public float cooldown;
    public int amount;
    public float damage;
    public float knockbackPower;
}

