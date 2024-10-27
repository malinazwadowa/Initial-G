using UnityEngine;

[CreateAssetMenu(fileName = "newCrystalParameters", menuName = "ScriptableObjects/Weapons/Crystal Parameters")]

public class SO_CrystalParameters : SO_WeaponParameters
{
    protected override void OnValidate()
    {
        base.OnValidate();
        for (int i = 0; i < ranks.Length; ++i)
        {
            ranks[i].name = "Rank " + (i + 1);
        }
        amountOfRanks = ranks.Length;
    }

    [Header("Rank independend settings")]


    [Header("Settings for each rank")]
    public CrystalRank[] ranks;

    
}

[System.Serializable]
public class CrystalRank
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
