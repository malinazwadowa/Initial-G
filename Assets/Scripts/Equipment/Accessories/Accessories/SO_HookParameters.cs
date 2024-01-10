using UnityEngine;

[CreateAssetMenu(fileName = "newHookParameters", menuName = "ScriptableObjects/Accessories/Hook Parameters")]

public class SO_HookParameters : SO_AccessoryParameters
{
    protected override void OnValidate()
    {
        base.OnValidate();
        amountOfRanks = numberOfRanks;
    }

    [Header("Amount of ranks")]
    public int numberOfRanks;

    [Header("Per rank looting radius modifier increase")]
    public float value;
}