using UnityEngine;

[CreateAssetMenu(fileName = "newBootParameters", menuName = "ScriptableObjects/Accessories/Boot Parameters")]

public class SO_BootParameters : SO_AccessoryParameters
{
    protected override void OnValidate()
    {
        base.OnValidate();
        amountOfRanks = numberOfRanks;
    }

    [Header("Amount of ranks")]
    public int numberOfRanks;

    [Header("Per rank cooldown reduction in %")]
    public float value;
}