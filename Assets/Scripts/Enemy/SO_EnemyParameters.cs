using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyParameters", menuName = "ScriptableObjects/Enemy Parameters/Regular Enemy")]
public class SO_EnemyParameters : ScriptableObject
{
    public EnemyType type;
    public int tier;
    public float speed;

    [Header("Health")]
    public float maxHealth;

    [Header("Combat Stats")]
    public float damage;
    public float attackCooldown;
    public float armor;
}
