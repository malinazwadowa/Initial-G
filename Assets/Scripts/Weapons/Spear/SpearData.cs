using UnityEngine;
using System;

[CreateAssetMenu(fileName = "newSpearData", menuName = "Weapon Data/Spear Data/Base Data")]

//Possibly should be changed to generic WeaponData with optional fields. Or should it ? O.o 
public class SpearData : ScriptableObject
{
    public event Action OnWeaponDataChanged;
    private void OnValidate()
    {
        for (int i = 0; i < spearRanks.Length; ++i)
        {
            spearRanks[i].name = "Rank " + (i + 1);
        }
        OnWeaponDataChanged?.Invoke();
    }

    [Header("Rank independend settings")]
    public float spawnDelayForAdditionalProjectiles;
    public float projectileSpacing;
    
    [Header("Settings for each rank")]
    public SpearRank[] spearRanks;
}
[System.Serializable]
public class SpearRank
{
    [HideInInspector] public string name;

    public GameObject projectilePrefab;
    public float speed;
    public float cooldown;
    public int amount;
    public float damage;
    //public float knockbackStrenght;
}

