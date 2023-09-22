using System.Collections;
using UnityEngine;

public class SpearProperties
{
    public float damage;
    public float speed;
    public float cooldown;
    public int amount;
    public float strength;
    public GameObject prefab;

}

public class Spear : Weapon
{
    
    public SpearData spearBaseData;
    private SpearRank spearCurrentRankData;
    
    private float timer = 2;
    private SpearProperties currentSpearProperties;

    public void Start()
    {
        spearCurrentRankData = spearBaseData.spearRanks[currentRank];
        currentSpearProperties = new SpearProperties();

        
    }

    public void SetCurrentProperties()
    {
        SpearProperties spearProperties = new SpearProperties();
        spearProperties.damage = spearCurrentRankData.damage * baseDamageMultiplier;
        spearProperties.cooldown = spearCurrentRankData.cooldown * baseCooldownMultiplier;
        spearProperties.speed = spearCurrentRankData.speed * baseSpeedMultiplier;
        spearProperties.amount = spearCurrentRankData.amount;
        spearProperties.prefab = spearCurrentRankData.projectilePrefab;
        currentSpearProperties = spearProperties;
    }



    public override void WeaponTick()
    {
        base.WeaponTick();

        timer += Time.deltaTime;

        if (timer > spearCurrentRankData.cooldown)
        {
            ThrowSpears(myWeaponWielder.GetPosition(), myWeaponWielder.GetFacingDirection(), spearCurrentRankData.projectilePrefab);
            timer = 0;
        }
    }

    private void ThrowSpears(Vector3 position, Vector3 direction, GameObject spearProjectilePrefab)
    {
        //Spawning main spear.
        GameObject newSpear = ObjectPooler.Instance.SpawnObject(spearProjectilePrefab, position);
        newSpear.GetComponent<SpearProjectile>().Init(spearCurrentRankData.speed, direction, spearProjectilePrefab, currentSpearProperties);

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
            float spawnDelay = Mathf.Abs(offset) * spearBaseData.spawnDelayMultiplier;

            StartCoroutine(ThrowSpearWithDelay(position + finalOffset, direction, currentSpearProperties.speed, spawnDelay, spearProjectilePrefab));
        }
    }

    private float CalculateOffset(int i)
    {
        int value = i / 2;
        float offset = (i % 2 == 0) ? (spearBaseData.projectileSpacing * value) : (-spearBaseData.projectileSpacing * value);
        return offset;
    }
    private IEnumerator ThrowSpearWithDelay(Vector3 position, Vector3 direction, float speed, float delay, GameObject spearProjectilePrefab)
    {
        yield return new WaitForSeconds(delay);

        GameObject nextSpear = ObjectPooler.Instance.SpawnObject(spearCurrentRankData.projectilePrefab, position);
        nextSpear.GetComponent<SpearProjectile>().Init(speed, direction, spearProjectilePrefab, currentSpearProperties);
    }
    public override void RankUp()
    {
        if (currentRank < spearBaseData.spearRanks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up.");
            spearCurrentRankData = spearBaseData.spearRanks[currentRank];
            SetCurrentProperties();
        }
        else
        {
            Debug.Log("Maximum spear rank reached.");
        }
    }
}

