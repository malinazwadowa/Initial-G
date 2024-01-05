using System;
using UnityEngine;

public class LootCollisionHandler : MonoBehaviour
{
    private float lootingRadius;
    private CharacterStats characterStats;
    [SerializeField] private CircleCollider2D myCollider;

    public void Initialize(float lootingRadius, CharacterStats characterStats)
    {
        this.lootingRadius = lootingRadius;
        this.characterStats = characterStats;
        UpdateRadiusValue();
    }

    public void UpdateRadiusValue()
    {
        myCollider.radius = lootingRadius * characterStats.lootingRadiusModifier;
    }
}
