using System.Collections;
using UnityEngine;

public class Rock : Weapon
{
    [SerializeField] private RockData rockBaseData;
    private RockRank rockCurrentRankData;

    private float timer = 2;

    private WeaponProperties currentRockProperties;
    void Start()
    {
        rockCurrentRankData = rockBaseData.rockRanks[currentRank];
        currentRockProperties = new WeaponProperties();
        SetCurrentProperties();

        rockBaseData.OnWeaponDataChanged += SetCurrentProperties;
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

    private void ThrowRock()
    {
        //GameObject prefab = currentRockProperties.prefab;
        Vector3 spawnPosition = myWeaponWielder.GetWeaponsPosition() + GetRandomSpawnOffset(rockBaseData.spawnOffsetRangeForAdditionalRocks);

        if (MathUtility.GetClosestEnemy(spawnPosition) != null)
        {
            GameObject newRock = ObjectPooler.Instance.SpawnObject(currentRockProperties.prefab, spawnPosition);
            newRock.GetComponent<RockProjectile>().Init(spawnPosition, MathUtility.GetClosestEnemy(spawnPosition), currentRockProperties);
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

    private Vector2 GetRandomSpawnOffset(float offsetValue)
    {
        Vector2 spawnOffset = Vector3.zero;
        spawnOffset.x = Random.Range(-offsetValue, offsetValue);
        spawnOffset.y = Random.Range(-offsetValue, offsetValue);
        return spawnOffset;
        
    }
    public override void SetCurrentProperties()
    {
        WeaponProperties rockProperties = new WeaponProperties();
        rockProperties.damage = rockCurrentRankData.damage * combatStats.damageModifier;
        rockProperties.cooldown = rockCurrentRankData.cooldown * combatStats.cooldownModifier;
        rockProperties.speed = rockCurrentRankData.speed * combatStats.speedModifier;
        rockProperties.amount = rockCurrentRankData.amount;
        rockProperties.prefab = rockCurrentRankData.projectilePrefab;
        currentRockProperties = rockProperties;
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