using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTreasureContainerData", menuName = "Data /Treasure Container/Base Data")]
public class TreasureContainerData : ScriptableObject
{
    [Header("Base Settings")]
    public float health;

    [Header("Loot Settings")]
    public List<ObjectWithWeight> lootTable;
}

