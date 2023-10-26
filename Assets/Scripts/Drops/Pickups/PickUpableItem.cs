using UnityEngine;

public abstract class PickUpableItem : ScriptableObject
{
    public abstract void OnPickUp(GameObject collector);
}
