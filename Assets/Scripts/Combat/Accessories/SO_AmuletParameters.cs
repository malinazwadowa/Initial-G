using UnityEngine;

[CreateAssetMenu(fileName = "newAmuletParameters", menuName = "ScriptableObjects/Accessories/Amulet Parameters")]

public class SO_AmuletParameters : SO_Item
{
    private void OnValidate()
    {
        maxRank = numberOfRanks;
    }

    public int numberOfRanks;

    [Header("Increase per rank of item")]
    public float value;
}