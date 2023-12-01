using UnityEngine;
using System;

[CreateAssetMenu(fileName = "newHedgehogParameters", menuName = "ScriptableObjects/Weapons/Hedgehog Parameters")]

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
    public float duration;
    public int amount;
    public float damage;
    public float knockbackPower;
}

