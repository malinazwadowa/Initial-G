using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTreasureContainerParameters", menuName = "ScriptableObjects/Loot/Treasure Container Parameters")]
[Serializable]
public class SO_TreasureContainerParameters : ScriptableObject
{
    [Header("Base Settings")]
    [SerializeField] public float health;

    [Header("Loot Settings")]
    [SerializeField] public List<ObjectWithWeight> lootTable;
}

