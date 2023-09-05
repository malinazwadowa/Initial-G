using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class Spear : Weapon
{
    public SpearData spearBaseData;
    private SpearRank spearCurrentRankData;
    
    private float timer = 2;

    public void Start()
    {
        spearCurrentRankData = spearBaseData.spearRanks[currentRank];
    }
    public override void WeaponTick(Vector3 position, Quaternion rotation)
    {
        base.WeaponTick(position, rotation);
        
        timer += Time.deltaTime;

        if (timer > spearCurrentRankData.cooldownTime)
        {
            ThrowSpears(position, rotation, spearCurrentRankData.projectilePrefab);
            timer = 0;
        }
        
    }
    private void ThrowSpears(Vector3 position, Quaternion rotation, PoolableObject spearProjectilePrefab)
    {
        //Spawning main spear.
        GameObject newSpear = ObjectPooler.Instance.SpawnObject(spearProjectilePrefab, position, rotation);
        newSpear.GetComponent<SpearProjectile>().Init(spearCurrentRankData.speed, rotation * Vector3.right, spearProjectilePrefab);

        //Spawning additional spears.
        for (int i = 2; i <= spearCurrentRankData.amount; ++i)
        {
            float offset = CalculateOffset(i);

            // Calculate the perpendicular direction to the throw direction
            Vector3 throwDirection = rotation * Vector3.right;
            Vector3 perpendicularDirection = new Vector3(-throwDirection.y, throwDirection.x, 0).normalized;

            // Apply the calculated offset in the perpendicular direction
            Vector3 finalOffset = offset * perpendicularDirection;

            // Delay the spawn of each spear based on the offset value
            float spawnDelay = Mathf.Abs(offset) * spearBaseData.spawnDelayMultiplier;

            StartCoroutine(ThrowSpearWithDelay(position + finalOffset, rotation, spearCurrentRankData.speed, spawnDelay));
        }
    }

    private float CalculateOffset(int i)
    {
        int value = i / 2;
        float offset = (i % 2 == 0) ? (spearBaseData.projectileSpacing * value) : (-spearBaseData.projectileSpacing * value);
        return offset;
    }
    private IEnumerator ThrowSpearWithDelay(Vector3 position, Quaternion rotation, float speed, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject nextSpear = ObjectPooler.Instance.SpawnObject(spearCurrentRankData.projectilePrefab, position, rotation);
        nextSpear.GetComponent<SpearProjectile>().Init(speed, rotation * Vector3.right, spearCurrentRankData.projectilePrefab);
    }
    public override void RankUp()
    {
        if (currentRank < spearBaseData.spearRanks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up.");
            spearCurrentRankData = spearBaseData.spearRanks[currentRank];
        }
        else
        {
            Debug.Log("Maximum spear rank reached.");
        }
    }
}
