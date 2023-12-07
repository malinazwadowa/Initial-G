using System.Collections;
using UnityEngine;

public class Rock : Weapon
{
    [SerializeField] private SO_RockParameters rockBaseData;
    private RockRank rockCurrentRankData;
    private WeaponProperties currentRockProperties;

    private float timer = 2;

    void Start()
    {
        rockCurrentRankData = rockBaseData.rockRanks[currentRank];
        currentRockProperties = new WeaponProperties();
        SetCurrentProperties();

        rockBaseData.onWeaponDataChanged += SetCurrentProperties;
    }
    public override void SetCurrentProperties()
    {
        WeaponProperties rockProperties = new WeaponProperties();
        rockProperties.damage = rockCurrentRankData.damage * combatStats.damageModifier;
        rockProperties.cooldown = rockCurrentRankData.cooldown * combatStats.cooldownModifier;
        rockProperties.speed = rockCurrentRankData.speed * combatStats.speedModifier;
        rockProperties.amount = rockCurrentRankData.amount;
        rockProperties.prefab = rockCurrentRankData.projectilePrefab;
        rockProperties.knockbackPower = rockCurrentRankData.knockbackPower;
        currentRockProperties = rockProperties;
    }
    public override void WeaponTick()
    {
        base.WeaponTick();

        timer += Time.deltaTime;
        if (timer > currentRockProperties.cooldown)
        {   
            StartCoroutine(ThrowRocks(rockBaseData.spawnDelayForAdditionalRocks, currentRockProperties.amount));
            timer = 0;
        }
    }

    private void SpawnRocks()
    {
        Vector3 spawnPosition = myWeaponWielder.GetCenterPosition() + GetRandomSpawnOffset(rockBaseData.spawnOffsetRangeForAdditionalRocks);

        if (Utilities.GetClosestEnemy(spawnPosition) != null)
        {
            GameObject newRock = ObjectPooler.Instance.SpawnObject(currentRockProperties.prefab, spawnPosition);
            newRock.GetComponent<RockProjectile>().Init(spawnPosition, Utilities.GetClosestEnemy(spawnPosition), currentRockProperties.damage, currentRockProperties.speed, currentRockProperties.knockbackPower);
        }
        else
        {
            return;
        }
        
    }

    private IEnumerator ThrowRocks(float delay, int amount)
    {
        if (currentRockProperties.amount > 0)
        {
            SpawnRocks();
        }

        for (int i = 1; i < amount ; i++)
        {
            yield return new WaitForSeconds(delay);
            SpawnRocks();
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
        if (currentRank < rockBaseData.rockRanks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Rock.");
            rockCurrentRankData = rockBaseData.rockRanks[currentRank];
            SetCurrentProperties();
        }
        else
        {
            Debug.Log("Maximum Rock rank reached.");
        }
    }
}