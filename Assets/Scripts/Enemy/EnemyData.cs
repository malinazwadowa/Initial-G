using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy/Regular Enemy")]
public class EnemyData : ScriptableObject
{
    public int tier;
    public float speed;

    [Header("Health")]
    public float maxHealth;

    [Header("Combat Stats")]
    public float armor;
}
