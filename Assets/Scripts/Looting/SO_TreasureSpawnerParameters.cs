using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTreasureSpawnerParameters", menuName = "ScriptableObjects/Loot/Treasure Spawner Parameters")]
public class SO_TreasureSpawnerParameters : ScriptableObject
{
    [Header("Base Settings")]
    public float spawnTimeInterval;
    public float spawnTimeWindowSize;

    public List<ObjectWithWeight> treasureContainers;
}

