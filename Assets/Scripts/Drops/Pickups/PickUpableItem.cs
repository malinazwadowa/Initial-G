using UnityEngine;

public abstract class PickUpableItem : ScriptableObject
{
    public abstract void PickUp(GameObject collector);
}
