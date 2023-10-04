using UnityEngine;

public abstract class PickUpItem : ScriptableObject
{
    public abstract void PickUp(GameObject collector);
}
