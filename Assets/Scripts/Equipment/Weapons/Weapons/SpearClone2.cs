using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class SpearClone2 : Weapon
{
    private SO_SpearParameters baseParameters;
    private SpearRank currentRankParameters;

    private float timer = 2;

    public override void Initialize(IItemWielder weaponWielder, CharacterStats characterStats)
    {
        base.Initialize(weaponWielder, characterStats);

        baseParameters = (SO_SpearParameters)baseItemParameters;
        currentRankParameters = baseParameters.ranks[CurrentRank];
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
            newSpear.GetComponent<SpearProjectile>().Initialize(
                this.name,
                direction,
                currentRankParameters.damage * characterStats.damageModifier,
                currentRankParameters.speed * characterStats.weaponSpeedModifier,
                currentRankParameters.knockbackPower,
                currentRankParameters.piercing
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
        nextSpear.GetComponent<SpearProjectile>().Initialize(
            this.name,
            direction,
            currentRankParameters.damage * characterStats.damageModifier,
            currentRankParameters.speed * characterStats.weaponSpeedModifier,
            currentRankParameters.knockbackPower,
            currentRankParameters.piercing
            );
    }

    public override void RankUp()
    {
        base.RankUp();
    }
}

