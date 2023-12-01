using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerParameters", menuName = "ScriptableObjects/Player Parameters")]
public class PlayerParameters : ScriptableObject
{
    [Header("Health")]
    public float maxHealth = 100;

    [Header("Movement")]
    public float baseSpeed;
    public float baseRunRatio;

    [Header("Weapon Handling")]
    public float baseDamageModifier;
    public float baseCooldownModifier;
    public float baseWeaponSpeed;

    [Header("Loot")]
    public float lootingRadius;
}
