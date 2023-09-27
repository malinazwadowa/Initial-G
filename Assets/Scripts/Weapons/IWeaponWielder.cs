using UnityEngine;

public interface IWeaponWielder
{
    public Vector3 GetWeaponsPosition();
    public Transform GetWeaponsTransform();
    public Vector2 GetFacingDirection();
    
}
