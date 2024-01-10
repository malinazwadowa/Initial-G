using NaughtyAttributes;
using UnityEngine;

public class SO_ItemParameters : ScriptableObject
{
    protected virtual void OnValidate()
    {
        unlockCondition.Validate();
    }

    [Header("Unlock condition")]
    public UnlockCondition unlockCondition;
    
    [Header("Icon for UI")]
    public Sprite icon;

    [ResizableTextArea][Header("Item description")]
    public string description;

    [HideInInspector] public int amountOfRanks;
}
