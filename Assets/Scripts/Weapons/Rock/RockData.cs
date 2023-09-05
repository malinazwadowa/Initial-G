using UnityEngine;


[CreateAssetMenu(fileName = "newRockData", menuName = "Weapon Data/Rock Data/Base Data")]

//WeaponData!!!
public class RockData : ScriptableObject
{
    private void OnValidate()
    {
        for (int i = 0; i < rockRanks.Length; ++i)
        {
            rockRanks[i].name = "Rank " + (i + 1);
        }
    }

    [Header("Rank independend settings")]
    public float spawnDelay;
    public float spawnOffsetRange;

    [Header("Settings for each rank")]
    public RockRank[] rockRanks;
}
[System.Serializable]
public class RockRank
{
    [HideInInspector] public string name;

    public PoolableObject projectilePrefab;
    public float speed;
    public float cooldownTime;
    public int amount;
}

