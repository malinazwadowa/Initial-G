using UnityEngine;

[CreateAssetMenu(fileName = "newClockParameters", menuName = "ScriptableObjects/Accessories/Clock Parameters")]

public class SO_ClockParameters : SO_AccessoryParameters
{
    protected override void OnValidate()
    {
        base.OnValidate();
        maxRank = numberOfRanks;
    }

    [Header("Amount of ranks")]
    public int numberOfRanks;
    
    [Header("Per rank cooldown reduction in %")]
    public float value;
}