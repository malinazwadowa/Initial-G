using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;

    [Header("Health")]
    public float maxHealth;

    [Header("Combat Stats")]
    public float armor;
}
