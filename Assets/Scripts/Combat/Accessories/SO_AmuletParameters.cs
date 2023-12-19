using UnityEngine;

[CreateAssetMenu(fileName = "newAmuletParameters", menuName = "ScriptableObjects/Accessories/Amulet Parameters")]

public class SO_AmuletParameters : ScriptableObject
{
    public float numberOfRanks;
    [Header("Increase per rank of item")]
    public float value;
}