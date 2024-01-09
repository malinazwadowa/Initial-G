using UnityEngine;

[CreateAssetMenu(fileName = "newAmuletParameters", menuName = "ScriptableObjects/Accessories/Amulet Parameters")]

public class SO_AmuletParameters : SO_AccessoryParameters
{
    protected override void OnValidate()
    {
        base.OnValidate();
        maxRank = numberOfRanks;
    }

    [Header("Amount of ranks")]
    public int numberOfRanks;
    
    [Header("Per rank damage modifier increase")]
    public float value;
}