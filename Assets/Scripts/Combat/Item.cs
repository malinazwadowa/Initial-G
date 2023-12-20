using UnityEngine;

public class Item : MonoBehaviour 
{
    protected int currentRank;

    public SO_Item baseItemParameters;

    public virtual void RankUp()
    {
        currentRank++;
        EquipmentControllerUI.Instance.UpdateItemRank(GetType(), currentRank);
    }
}
