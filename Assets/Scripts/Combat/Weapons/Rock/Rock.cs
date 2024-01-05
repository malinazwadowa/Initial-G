using System.Collections;
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
        currentRankParameters = baseParameters.ranks[currentRank];
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
                this.name,
                spawnPosition,
                target,
                currentRankParameters.damage * characterStats.damageModifier,
                currentRankParameters.speed * characterStats.weaponSpeedModifier,
                currentRankParameters.knockbackPower
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
        if (currentRank < baseParameters.ranks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Rock.");
            currentRankParameters = baseParameters.ranks[currentRank];
        }
        else
        {
            Debug.Log("Maximum Rock rank reached.");
        }
    }
}
