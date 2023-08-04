using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")] 
public class EnemyData : ScriptableObject
{
    public Vector2 initalSpawnPositon;
    public bool attackingAllowed = true;

    [Header("Any State")]
    public float maxHealth = 100;
    public float currentHealth;

    [Header("Wandering State")] 
    public float wanderSpeed = 0.5f;
    public float wanderStopDistance = 0.01f;
    public float wanderObsRadius = 0.01f;

    [Header("Chase State")]
    public float chaseSpeed = 2.5f;
    public float chaseStopDistance = 1f;
    public float chaseObsRadius = 0.1f;
}
