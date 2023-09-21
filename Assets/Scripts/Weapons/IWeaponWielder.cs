using UnityEngine;

public interface IWeaponWielder
{
    public Vector2 GetPosition();
    public Vector2 GetFacingDirection();
    //must be generic Data
    public PlayerData GetData();
}
