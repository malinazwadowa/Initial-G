using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class SpearClone3 : Weapon
{
    private SO_SpearParameters baseParameters;
    private SpearRank currentRankParameters;

    private float timer = 2;

    public override void Initialize(IWeaponWielder weaponWielder, CharacterStats characterStats)
    {
        base.Initialize(weaponWielder, characterStats);

        baseParameters = (SO_SpearParameters)baseItemParameters;
        weaponType = baseParameters.weaponType;
        currentRankParameters = baseParameters.ranks[currentRank];
    }

    public override void WeaponTick()
    {
        base.WeaponTick();

        timer += Time.deltaTime;

        if (timer > currentRankParameters.cooldown * characterStats.cooldownModifier)
        {
            SpawnSpears(weaponWielder.GetCenterPosition(), weaponWielder.GetFacingDirection());
            timer = 0;
        }
    }

    private void SpawnSpears(Vector3 position, Vector3 direction)
    {
        //Spawning main spear.
        if (currentRankParameters.amount > 0)
        {
            GameObject newSpear = ObjectPooler.Instance.SpawnObject(currentRankParameters.projectilePrefab, position);
            newSpear.GetComponent<SpearProjectile>().Init(
                direction,
                currentRankParameters.damage * characterStats.damageModifier,
                currentRankParameters.speed * characterStats.weaponSpeedModifier,
                currentRankParameters.knockbackPower
                );
        }

        //Spawning additional spears.
        for (int i = 2; i <= currentRankParameters.amount; ++i)
        {
            float offset = CalculateOffset(i);

            // Calculate the perpendicular direction to the throw direction
            Vector3 throwDirection = direction;
            Vector3 perpendicularDirection = new Vector3(-throwDirection.y, throwDirection.x, 0).normalized;

            // Apply the calculated offset in the perpendicular direction
            Vector3 finalOffset = offset * perpendicularDirection;

            // Delay the spawn of each spear based on the offset value
            float spawnDelay = i / 2 * baseParameters.spawnDelayForAdditionalProjectiles;

            StartCoroutine(ThrowSpearWithDelay(position + finalOffset, direction, spawnDelay));
        }
    }

    private float CalculateOffset(int i)
    {
        int value = i / 2;
        float offset = (i % 2 == 0) ? (baseParameters.projectileSpacing * value) : (-baseParameters.projectileSpacing * value);
        return offset;
    }

    private IEnumerator ThrowSpearWithDelay(Vector3 position, Vector3 direction, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject nextSpear = ObjectPooler.Instance.SpawnObject(currentRankParameters.projectilePrefab, position);
        nextSpear.GetComponent<SpearProjectile>().Init(
            direction,
            currentRankParameters.damage * characterStats.damageModifier,
            currentRankParameters.speed * characterStats.weaponSpeedModifier,
            currentRankParameters.knockbackPower
            );
    }

    public override void RankUp()
    {
        if (currentRank < baseParameters.ranks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Spear.");
            currentRankParameters = baseParameters.ranks[currentRank];
        }
        else
        {
            Debug.Log("Maximum Spear rank reached.");
        }
    }
}

