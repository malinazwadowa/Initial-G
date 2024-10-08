using UnityEngine;

[CreateAssetMenu(fileName = "newPickUpItemParameters", menuName = "ScriptableObjects/Loot/PickUp Items/Healing PickUp Parameters")]
public class SO_HealingPickUpParameters : ScriptableObject
{
    public float restoreAmount;
    public AudioClipNameSelector pickUpClip;
}
