using UnityEngine;

public class SO_Item : ScriptableObject
{
    [Header("Icon for UI")]
    public Sprite icon;

    [HideInInspector] public int maxRank;
}
