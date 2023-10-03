using System.Collections;
using UnityEngine;

public class Spear :  Weapon
{
    [SerializeField] private SpearData spearBaseData;
    private SpearRank spearCurrentRankData;
    private WeaponProperties currentSpearProperties;
    
    private float timer = 2;
    
    public void Start()
    {
        spearCurrentRankData = spearBaseData.spearRanks[currentRank];
        currentSpearProperties = new WeaponProperties();
        SetCurrentProperties();
        spearBaseData.onWeaponDataChanged += SetCurrentProperties;
    }

    public override void SetCurrentProperties()
    {
        WeaponProperties spearProperties = new WeaponProperties();
        spearProperties.damage = spearCurrentRankData.damage * combatStats.damageModifier;
        spearProperties.cooldown = spearCurrentRankData.cooldown * combatStats.cooldownModifier;
        spearProperties.speed = spearCurrentRankData.speed * combatStats.speedModifier;
        spearProperties.amount = spearCurrentRankData.amount;
        spearProperties.knockbackPower = spearCurrentRankData.knockbackPower;
        spearProperties.prefab = spearCurrentRankData.projectilePrefab;
        currentSpearProperties = spearProperties;
    }

    public override void WeaponTick()
    {
        base.WeaponTick();

        timer += Time.deltaTime;

        if (timer > currentSpearProperties.cooldown)
        {
            SpawnSpears(myWeaponWielder.GetWeaponsPosition(), myWeaponWielder.GetFacingDirection(), currentSpearProperties);
            timer = 0;
        }
    }

    private void SpawnSpears(Vector3 position, Vector3 direction, WeaponProperties spearProperties)
    {
        //Spawning main spear.
        if(currentSpearProperties.amount > 0)
        {
            GameObject newSpear = ObjectPooler.Instance.SpawnObject(spearProperties.prefab, position);
            newSpear.GetComponent<SpearProjectile>().Init(direction, spearProperties);
        }
        
        //Spawning additional spears.
        for (int i = 2; i <= spearCurrentRankData.amount; ++i)
        {
            float offset = CalculateOffset(i);

            // Calculate the perpendicular direction to the throw direction
            Vector3 throwDirection = direction;
            Vector3 perpendicularDirection = new Vector3(-throwDirection.y, throwDirection.x, 0).normalized;

            // Apply the calculated offset in the perpendicular direction
            Vector3 finalOffset = offset * perpendicularDirection;

            // Delay the spawn of each spear based on the offset value
            float spawnDelay = i/2 * spearBaseData.spawnDelayForAdditionalProjectiles;

            StartCoroutine(ThrowSpearWithDelay(position + finalOffset, direction, spawnDelay, spearProperties));
        }
    }

    private float CalculateOffset(int i)
    {
        int value = i / 2;
        float offset = (i % 2 == 0) ? (spearBaseData.projectileSpacing * value) : (-spearBaseData.projectileSpacing * value);
        return offset;
    }

    private IEnumerator ThrowSpearWithDelay(Vector3 position, Vector3 direction, float delay, WeaponProperties spearProperties)
    {
        yield return new WaitForSeconds(delay);

        GameObject nextSpear = ObjectPooler.Instance.SpawnObject(spearProperties.prefab, position);
        nextSpear.GetComponent<SpearProjectile>().Init(direction, spearProperties);
    }

    public override void RankUp()
    {
        if (currentRank < spearBaseData.spearRanks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Spear.");
            spearCurrentRankData = spearBaseData.spearRanks[currentRank];
            SetCurrentProperties();
        }
        else
        {
            Debug.Log("Maximum Spear rank reached.");
        }
    }
}

