using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Weapon 
{
    private SO_RockParameters baseParameters;
    private RockRank currentRankParameters;
    private float timer;
    
    public override void Initialize(IItemWielder weaponWielder, CharacterStats characterStats)
    {
        base.Initialize(weaponWielder, characterStats);

        baseParameters = (SO_RockParameters)baseItemParameters;
        currentRankParameters = baseParameters.ranks[CurrentRank];
    }
    
    public override void WeaponTick()
    {
        base.WeaponTick();

        timer += Time.deltaTime;
        if (timer > currentRankParameters.cooldown * characterStats.cooldownModifier)
        {
            StartCoroutine(ThrowRocks(baseParameters.spawnDelayForAdditionalRocks, currentRankParameters.amount));
            timer = 0;
        }
    }

    private IEnumerator ThrowRocks(float delay, int amount)
    {
        if (currentRankParameters.amount > 0)
        {
            SpawnRock();
        }

        for (int i = 1; i < amount; i++)
        {
            yield return new WaitForSeconds(delay);
            SpawnRock();
        }
    }

    private void SpawnRock()
    {
        Vector3 spawnPosition = weaponWielder.GetCenterPosition() + GetRandomSpawnOffset(baseParameters.spawnOffsetRangeForAdditionalRocks);
        Transform target = Utilities.GetClosestEnemy(spawnPosition);
        if (target != null)
        {
            GameObject newRock = ObjectPooler.Instance.SpawnObject(currentRankParameters.projectilePrefab, spawnPosition);
            newRock.GetComponent<RockProjectile>().Initialize
                (
                this.GetType().Name,
                spawnPosition,
                target,
                currentRankParameters.damage * characterStats.damageModifier,
                currentRankParameters.speed * characterStats.weaponSpeedModifier,
                currentRankParameters.knockbackPower,
                currentRankParameters.piercing
                );
        }
        else
        {
            return;
        }
    }

    private Vector3 GetRandomSpawnOffset(float offsetValue)
    {
        Vector3 spawnOffset = Vector3.zero;
        spawnOffset.x = Random.Range(-offsetValue, offsetValue);
        spawnOffset.y = Random.Range(-offsetValue, offsetValue);
        return spawnOffset;
    }

    public override void RankUp()
    {
        /*
        if (currentRank < baseParameters.ranks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Rock.");
            currentRankParameters = baseParameters.ranks[currentRank];
        }
        else
        {
            Debug.Log("Maximum Rock rank reached.");
        } */
        currentRankParameters = baseParameters.ranks[CurrentRank];
        base.RankUp();
    }

    public override Dictionary<string, float> GetParameters(int rank)
    {
        Dictionary<string, float> parameters = new Dictionary<string, float>();

        if (rank >= baseParameters.ranks.Length || rank < 0)
        {
            Debug.LogError("Invalid rank specified for GetParameters.");
            return parameters;
        }

        RockRank rankParameters = baseParameters.ranks[rank];
        parameters.Add("speed", rankParameters.speed);
        parameters.Add("cooldown", rankParameters.cooldown);
        parameters.Add("amount", rankParameters.amount);
        parameters.Add("damage", rankParameters.damage);
        parameters.Add("knockbackPower", rankParameters.knockbackPower);
        parameters.Add("piercing", rankParameters.piercing);

        return parameters;
    }
}
