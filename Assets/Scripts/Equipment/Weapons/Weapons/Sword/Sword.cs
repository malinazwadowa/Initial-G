using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private SO_SwordParameters baseParameters;
    private SwordRank currentRankParameters;
    private float timer;

    public override void Initialize(IItemWielder weaponWielder, CharacterStats characterStats)
    {
        base.Initialize(weaponWielder, characterStats);

        baseParameters = (SO_SwordParameters)baseItemParameters;
        currentRankParameters = baseParameters.ranks[CurrentRank];
    }

    public override void WeaponTick()
    {
        base.WeaponTick();

        timer += Time.deltaTime;

        if (timer > currentRankParameters.cooldown * characterStats.cooldownModifier)
        {
            GameObject newSword = ObjectPooler.Instance.SpawnObject(currentRankParameters.projectilePrefab, weaponWielder.GetCenterPosition());

            newSword.GetComponent<SwordProjectile>().Initialize(
                this.GetType().Name,
                weaponWielder.GetCenterTransform(),
                weaponWielder.GetFacingDirection(),
                baseParameters.speedModifier,
                currentRankParameters.damage * characterStats.damageModifier,
                currentRankParameters.radius,
                currentRankParameters.knockbackPower
                );
            timer = 0;
        }
    }

    public override void RankUp()
    {
        base.RankUp();
        currentRankParameters = baseParameters.ranks[CurrentRank];
    }

    public override Dictionary<string, float> GetParameters(int rank)
    {
        Dictionary<string, float> parameters = new Dictionary<string, float>();

        if (rank >= baseParameters.ranks.Length || rank < 0)
        {
            Debug.LogError("Invalid rank specified for GetParameters.");
            return parameters;
        }

        SwordRank rankParameters = baseParameters.ranks[rank];
        parameters.Add("cooldown", rankParameters.cooldown);
        parameters.Add("radius", rankParameters.radius);
        parameters.Add("damage", rankParameters.damage);
        parameters.Add("knockbackPower", rankParameters.knockbackPower);

        return parameters;
    }
}