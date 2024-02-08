using UnityEngine;

[CreateAssetMenu(fileName = "newSwordParameters", menuName = "ScriptableObjects/Weapons/Sword Parameters")]

public class SO_SwordParameters : SO_WeaponParameters
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

    [Header("Rank independent settings")]
    public float speedModifier;

    [Header("Settings for each rank")]
    public SwordRank[] ranks;
}

[System.Serializable]
public class SwordRank
{
    [HideInInspector] public string name;

    public GameObject projectilePrefab;
    public float radius;
    public float cooldown;
    public float damage;
    public float knockbackPower;
}