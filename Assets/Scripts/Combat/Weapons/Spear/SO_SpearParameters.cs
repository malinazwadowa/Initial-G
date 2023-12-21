using UnityEngine;

[CreateAssetMenu(fileName = "newSpearParameters", menuName = "ScriptableObjects/Weapons/Spear Parameters")]

public class SO_SpearParameters : SO_ItemParameters
{
    private void OnValidate()
    {
        for (int i = 0; i < ranks.Length; ++i)
        {
            ranks[i].name = "Rank " + (i + 1);
        }
        maxRank = ranks.Length;
    }

    [Header("Rank independend settings")]
    public float spawnDelayForAdditionalProjectiles;
    public float projectileSpacing;
    
    [Header("Settings for each rank")]
    public SpearRank[] ranks;
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
    public float knockbackPower;
}