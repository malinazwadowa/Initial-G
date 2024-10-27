using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Weapon
{
    private SO_CrystalParameters baseParameters;
    private CrystalRank currentRankParameters;

    private float cooldownTimer;
    private float durationTimer;

    public override void Initialize(IItemWielder weaponWielder, CharacterStats characterStats)
    {
        base.Initialize(weaponWielder, characterStats);
        baseParameters = (SO_CrystalParameters)baseItemParameters;
        currentRankParameters = baseParameters.ranks[CurrentRank];
    }


    public override void WeaponTick()
    {
        base.WeaponTick();
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer > currentRankParameters.cooldown * characterStats.cooldownModifier)
        {
            SpawnWave();
            cooldownTimer = 0;
        }
    }

    private void SpawnWave()
    {
        GameObject newWave = ObjectPooler.Instance.SpawnObject(currentRankParameters.projectilePrefab, weaponWielder.GetCenterPosition(), default);
        newWave.GetComponent<CrystalProjectile>().Initialize(
            this.GetType().Name,
            currentRankParameters.damage * characterStats.damageModifier,
            currentRankParameters.speed * characterStats.weaponSpeedModifier,
            currentRankParameters.knockbackPower,
            currentRankParameters.duration * characterStats.durationModifier
            );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void RankUp()
    {
        base.RankUp();
        currentRankParameters = baseParameters.ranks[CurrentRank];
    }
    public override Dictionary<string, float> GetParameters(int rank)
    {
        Dictionary<string, float> parameters = new Dictionary<string, float>();

        if (rank >= baseParameters.ranks.Length || rank < 0)
        {
            Debug.LogError("Invalid rank specified for GetParameters.");
            return parameters;
        }

        CrystalRank rankParameters = baseParameters.ranks[rank];
        parameters.Add("speed", rankParameters.speed);
        parameters.Add("cooldown", rankParameters.cooldown);
        parameters.Add("radius", rankParameters.radius);
        parameters.Add("duration", rankParameters.duration);
        parameters.Add("amount", rankParameters.amount);
        parameters.Add("damage", rankParameters.damage);
        parameters.Add("knockbackPower", rankParameters.knockbackPower);

        return parameters;
    }

}
