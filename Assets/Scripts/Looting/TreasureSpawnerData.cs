using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTreasureSpawnerData", menuName = "Data /Treasure Spawner/Base Data")]
public class TreasureSpawnerData : ScriptableObject
{
    [Header("Base Settings")]
    public float spawnTimeInterval;
    public float spawnTimeWindowSize;

    public List<ObjectWithWeight> treasureContainers;
}

