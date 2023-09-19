using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : Weapon
{
    [SerializeField] private RockData rockBaseData;
    private RockRank rockCurrentRankData;

    private float timer = 2;
    void Start()
    {
        rockCurrentRankData = rockBaseData.rockRanks[currentRank];
    }
    public override void WeaponTick()
    {
        base.WeaponTick();

        timer += Time.deltaTime;
        if (timer > rockCurrentRankData.cooldownTime)
        {   
            StartCoroutine(ThrowRocks(rockBaseData.spawnDelay, rockCurrentRankData.amount));
            timer = 0;
        }
    }

    private void ThrowRock()
    {
        GameObject prefab = rockCurrentRankData.projectilePrefab;
        Vector3 spawnPosition = PlayerManager.Instance.GetCurrentPlayerWeaponsPosition() + GetRandomSpawnOffset(rockBaseData.spawnOffsetRange);

        if (MathUtility.GetClosestEnemy(spawnPosition) != null)
        {
            GameObject newRock = ObjectPooler.Instance.SpawnObject(prefab, spawnPosition, transform.rotation);
            newRock.GetComponent<RockProjectile>().Init(rockCurrentRankData.speed, spawnPosition, MathUtility.GetClosestEnemy(spawnPosition), prefab);
        }
        else
        {
            return;
        }
        
    }

    private IEnumerator ThrowRocks(float delay, int amount)
    {
        ThrowRock();
        for (int i = 1; i < amount ; i++)
        {
            yield return new WaitForSeconds(delay);
            ThrowRock();
        }
    }

    private Vector3 GetRandomSpawnOffset(float offsetValue)
    {
        Vector3 spawnOffset = Vector3.zero;
        spawnOffset.x = Random.Range(-offsetValue, offsetValue);
        spawnOffset.y = Random.Range(-offsetValue, offsetValue);
        return spawnOffset;
        
    }
}
/*
 * private void ThrowRock(PoolableObject rockProjectilePrefab, Vector3 position)
    {
        GameObject newRock = ObjectPooler.Instance.SpawnObject(rockProjectilePrefab, position, transform.rotation);
        newRock.GetComponent<RockProjectile>().Init(rockCurrentRankData.speed, position, MathUtility.GetClosestEnemy(position));

        //StartCoroutine(ThrowRockWithDelay(rockBajectilePrefab));
    }
    private IEnumerator ThrowRockWithDelay(float delay, int amount, PoolableObject rockProjectilePrefab)
    {
        for (int i = 1; i < amount; i++)
        {
            yield return new WaitForSeconds(delay);

            Vector3 spawnPosition = PlayerManager.Instance.GetCurrentPlayerWeaponsPosition() + GetRandomSpawnOffset(rockBaseData.spawnOffsetRange);
            GameObject newRock = ObjectPooler.Instance.SpawnObject(rockProjectilePrefab, spawnPosition, transform.rotation);
            newRock.GetComponent<RockProjectile>().Init(rockCurrentRankData.speed, spawnPosition, MathUtility.GetClosestEnemy(spawnPosition));
        }
    }

    /// <summary>
    /// ////////////////////////////////////////////
    /// </summary>
 */