using UnityEngine;

[CreateAssetMenu(fileName = "newAmuletParameters", menuName = "ScriptableObjects/Accessories/Amulet Parameters")]

public class SO_AmuletParameters : SO_AccessoryParameters
{
    protected override void OnValidate()
    {
        base.OnValidate();
        amountOfRanks = numberOfRanks;
    }

    [Header("Amount of ranks")]
    public int numberOfRanks;
    
    [Header("Per rank damage done increase in %")]
    public float value;
}