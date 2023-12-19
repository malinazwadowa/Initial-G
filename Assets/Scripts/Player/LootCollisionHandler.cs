using System;
using UnityEngine;

public class LootCollisionHandler : MonoBehaviour
{
    private float lootingRadius;
    [SerializeField] private CircleCollider2D myCollider;
    public void Initialize(float lootingRadius)
    {
        this.lootingRadius = lootingRadius;
        SetRadius(lootingRadius);
    }

    private void SetRadius(float lootingRadius)
    {
        myCollider.radius = lootingRadius;
    }
}
