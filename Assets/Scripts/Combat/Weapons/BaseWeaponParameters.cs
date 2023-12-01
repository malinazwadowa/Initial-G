using UnityEngine;


//unused atm
[CreateAssetMenu(fileName = "newBaseWeaponParameters", menuName = "ScriptableObjects/Base Weapon Parameters")]
public class BaseWeaponParameters : ScriptableObject
{
    public float baseCooldownMultiplier = 1.0f;
    public float baseDamageMultiplier = 1.0f;
    public float baseSpeedMultiplier = 1.0f;
}


