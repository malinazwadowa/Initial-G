using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : Weapon
{
    public HedgehogData hedgehogBaseData;
    private HedgehogRank hedgehogCurrentRankData;
    private float timer = 2;
    private WeaponProperties currentHedgehogProperties;
    void Start()
    {
        hedgehogCurrentRankData = hedgehogBaseData.hedgehogRanks[currentRank];
        currentHedgehogProperties = new WeaponProperties();
        SetCurrentProperties();

        hedgehogBaseData.OnWeaponDataChanged += SetCurrentProperties;
    }

    public override void WeaponTick()
    {
        base.WeaponTick();
        
        timer += Time.deltaTime;
        if(timer > currentHedgehogProperties.cooldown) 
        { 
            //do tick        
        }

    }

    public override void RankUp()
    {
        if (currentRank < hedgehogBaseData.hedgehogRanks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Hedgehog.");
            hedgehogCurrentRankData = hedgehogBaseData.hedgehogRanks[currentRank];
            SetCurrentProperties();
        }
        else
        {
            Debug.Log("Maximum Hedgehog rank reached.");
        }
    }

    public override void SetCurrentProperties()
    {
        WeaponProperties hedgehogProperties = new WeaponProperties();
        hedgehogProperties.damage = hedgehogCurrentRankData.damage * combatStats.damageModifier;
        hedgehogProperties.cooldown = hedgehogCurrentRankData.cooldown * combatStats.cooldownModifier;
        hedgehogProperties.speed = hedgehogCurrentRankData.speed * combatStats.speedModifier;
        hedgehogProperties.amount = hedgehogCurrentRankData.amount;
        hedgehogProperties.prefab = hedgehogCurrentRankData.projectilePrefab;
        currentHedgehogProperties = hedgehogProperties;
    }



}
