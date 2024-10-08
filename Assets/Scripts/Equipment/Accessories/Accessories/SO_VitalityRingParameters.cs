using UnityEngine;

[CreateAssetMenu(fileName = "newVitalityRingParameters", menuName = "ScriptableObjects/Accessories/VitalityRing Parameters")]

public class SO_VitalityRingParameters : SO_AccessoryParameters
{
    protected override void OnValidate()
    {
        base.OnValidate();
        amountOfRanks = numberOfRanks;
    }

    [Header("Amount of ranks")]
    public int numberOfRanks;

    [Header("Per rank regeneration rate increase in %")]
    public float value;
}