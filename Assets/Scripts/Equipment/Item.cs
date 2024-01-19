using NaughtyAttributes;
using UnityEngine;

public class Item : MonoBehaviour 
{
    [HideInInspector] public int currentRank { get; private set; }
    [Expandable] public SO_ItemParameters baseItemParameters;
    public float TimeOfEquipping { get; private set; }

    protected void SetEquippedTime()
    {
        TimeOfEquipping = Time.time;
    }

    public virtual void RankUp()
    {
        Debug.Log($"Ranking up {this.GetType().Name.ToString()}");
        if (currentRank >= baseItemParameters.amountOfRanks - 1) { Debug.Log($"{gameObject.GetType().Name} is max rank already"); return; }

        Debug.Log($"Ranking up {this.GetType().Name} current rank is {currentRank}");
        currentRank++;
        EquipmentUI.Instance.UpdateItemRank(GetType(), currentRank);

        Debug.Log($"Ranking up {this.GetType().Name} rank after ranking up is {currentRank} and max rank is {baseItemParameters.amountOfRanks - 1}");

        if (currentRank >= baseItemParameters.amountOfRanks - 1) { GameManager.Instance.gameStatsController.RegisterFullyRankedUpItem(this.GetType().Name); }
    }
}
