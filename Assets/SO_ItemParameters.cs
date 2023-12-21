using UnityEngine;

public class SO_ItemParameters : ScriptableObject
{
    public ItemType type;
    [Header("Icon for UI")]
    public Sprite icon;


    [HideInInspector] public int maxRank;
}
