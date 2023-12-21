using UnityEngine;

[CreateAssetMenu(fileName = "newRockParameters", menuName = "ScriptableObjects/Weapons/Rock Parameters")]

public class SO_RockParameters : SO_ItemParameters
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
    public float spawnDelayForAdditionalRocks;
    public float spawnOffsetRangeForAdditionalRocks;

    [Header("Settings for each rank")]
    public RockRank[] ranks;
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