using UnityEngine;

[CreateAssetMenu(fileName = "newHedgehogParameters", menuName = "ScriptableObjects/Weapons/Hedgehog Parameters")]

public class SO_HedgehogParameters : SO_ItemParameters
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
    

    [Header("Settings for each rank")]
    public HedgehogRank[] ranks;
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

