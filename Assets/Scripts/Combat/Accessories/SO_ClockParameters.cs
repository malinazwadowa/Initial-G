using UnityEngine;

[CreateAssetMenu(fileName = "newClockParameters", menuName = "ScriptableObjects/Accessories/Clock Parameters")]

public class SO_ClockParameters : ScriptableObject
{
    public float numberOfRanks;
    [Header("Per rank reduction in %")]
    public float value;
}