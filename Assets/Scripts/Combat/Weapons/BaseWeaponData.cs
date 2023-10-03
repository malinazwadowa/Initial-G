using UnityEngine;


//unused atm
[CreateAssetMenu(fileName = "newBaseWeaponData", menuName = "Weapon Data/Base Weapon Data/Base Data")]
public class BaseWeaponData : ScriptableObject
{
    public float baseCooldownMultiplier = 1.0f;
    public float baseDamageMultiplier = 1.0f;
    public float baseSpeedMultiplier = 1.0f;
}


