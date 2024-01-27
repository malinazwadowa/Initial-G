using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour 
{
    [HideInInspector] public int CurrentRank { get; private set; }
    [Expandable] public SO_ItemParameters baseItemParameters;
    public float TimeOfEquipping { get; private set; }

    protected void SetEquippedTime()
    {
        TimeOfEquipping = Time.time;
    }

    public virtual void RankUp()
    {
        if (CurrentRank >= baseItemParameters.amountOfRanks - 1) 
        { 
            Debug.Log($"{gameObject.GetType().Name} is max rank already");
            return;
        }

        CurrentRank++;
        EquipmentUI.Instance.UpdateItemRank(GetType(), CurrentRank);

        if (CurrentRank >= baseItemParameters.amountOfRanks - 1) 
        {
            GameManager.Instance.gameStatsController.RegisterFullyRankedUpItem(this.GetType().Name);
        }
    }
}
