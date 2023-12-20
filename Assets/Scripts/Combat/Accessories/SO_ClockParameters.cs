using UnityEngine;

[CreateAssetMenu(fileName = "newClockParameters", menuName = "ScriptableObjects/Accessories/Clock Parameters")]

public class SO_ClockParameters : SO_Item
{
    private void OnValidate()
    {
        maxRank = numberOfRanks;
    }

    public int numberOfRanks;
    
    [Header("Per rank reduction in %")]
    public float value;
}