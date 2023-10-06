using UnityEngine;

public interface IWeaponWielder
{
    public Vector3 GetCenterPosition();
    public Transform GetCenterTransform();
    public Vector2 GetFacingDirection();
    
}
