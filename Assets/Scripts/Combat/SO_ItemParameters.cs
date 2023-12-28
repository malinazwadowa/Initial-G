using NaughtyAttributes;
using System;
using System.Collections.Generic;
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

    [HideInInspector] public int maxRank;
}
