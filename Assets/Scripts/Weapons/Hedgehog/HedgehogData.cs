using UnityEngine;
using System;

[CreateAssetMenu(fileName = "newHedgehogData", menuName = "Weapon Data/Hedgehog Data/Base Data")]

//Possibly should be changed to generic WeaponData with optional fields. Or should it ? O.o 
public class HedgehogData : ScriptableObject
{
    public event Action OnWeaponDataChanged;
    private void OnValidate()
    {
        for (int i = 0; i < hedgehogRanks.Length; ++i)
        {
            hedgehogRanks[i].name = "Rank " + (i + 1);
        }
        OnWeaponDataChanged?.Invoke();
    }

    [Header("Rank independend settings")]
    public float spawnDelayForAdditionalProjectiles;
    public float projectileSpacing;

    [Header("Settings for each rank")]
    public HedgehogRank[] hedgehogRanks;
}
[System.Serializable]
public class HedgehogRank
{
    [HideInInspector] public string name;

    public GameObject projectilePrefab;
    public float speed;
    public float cooldown;
    public float radius;
    public int amount;
    public float damage;
    //public float knockbackStrenght;
}

