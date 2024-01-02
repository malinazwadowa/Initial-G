using NaughtyAttributes;
using UnityEngine;

public class Item : MonoBehaviour 
{
    [HideInInspector] public int currentRank { get; private set; }

    [Expandable] public SO_ItemParameters baseItemParameters;

    public virtual void RankUp()
    {
        currentRank++;
        EquipmentControllerUI.Instance.UpdateItemRank(GetType(), currentRank);
    }
}
