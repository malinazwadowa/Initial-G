using UnityEngine;


[CreateAssetMenu(fileName = "newSpearData", menuName = "Weapon Data/Spear Data/Base Data")]

public class SpearData : ScriptableObject
{
    private void OnValidate()
    {
        for (int i = 0; i < spearRanks.Length; ++i)
        {
            spearRanks[i].name = "Rank " + (i + 1);
        }
    }

    [Header("Rank independend settings")]
    public float spawnDelayMultiplier;
    public float projectileSpacing;
    
    [Header("Settings for each rank")]
    public SpearRank[] spearRanks;
}
[System.Serializable]
public class SpearRank
{
    [HideInInspector] public string name;

    public PoolableObject projectilePrefab;
    public float speed;
    public float cooldownTime;
    public int amount;
}

