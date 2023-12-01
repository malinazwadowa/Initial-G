using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTreasureContainerParameters", menuName = "ScriptableObjects/Loot/Treasure Container Parameters")]
public class TreasureContainerData : ScriptableObject
{
    [Header("Base Settings")]
    public float health;

    [Header("Loot Settings")]
    public List<ObjectWithWeight> lootTable;
}

