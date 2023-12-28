using NaughtyAttributes;
using UnityEngine;

public class Item : MonoBehaviour 
{
    protected int currentRank;

    [Expandable] public SO_ItemParameters baseItemParameters;

    public virtual void RankUp()
    {
        currentRank++;
        EquipmentControllerUI.Instance.UpdateItemRank(GetType(), currentRank);
    }
}
