using UnityEngine;

public interface IItemWielder
{
    public Vector3 GetCenterPosition();
    public Transform GetCenterTransform();
    public Vector2 GetFacingDirection();
    
}
