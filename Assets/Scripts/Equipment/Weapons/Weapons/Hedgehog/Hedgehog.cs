using NaughtyAttributes;
using UnityEngine;

public class Hedgehog : Weapon
{
    private SO_HedgehogParameters baseParameters;
    private HedgehogRank currentRankParameters;
    
    private bool hasFinishedSpinning = true;
    private float cooldownTimer;
    private float durationTimer;

    public override void Initialize(IItemWielder weaponWielder, CharacterStats characterStats)
    {
        base.Initialize(weaponWielder, characterStats);

        baseParameters = (SO_HedgehogParameters)baseItemParameters;
        currentRankParameters = baseParameters.ranks[currentRank];
        cooldownTimer = float.PositiveInfinity;
    }

    public override void WeaponTick()
    {
        base.WeaponTick();

        cooldownTimer += Time.deltaTime;

        if (hasFinishedSpinning == true && cooldownTimer > currentRankParameters.cooldown * characterStats.cooldownModifier) 
        {
            SpawnHedgehogs();
            hasFinishedSpinning=false;
            durationTimer = 0;
        }

        if(hasFinishedSpinning == false)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer > currentRankParameters.duration * characterStats.durationModifier)
            {
                cooldownTimer = 0;
                hasFinishedSpinning = true;
            }
        }
    }

    public void SpawnHedgehogs()
    {
        int numberOfProjectiles = currentRankParameters.amount;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * (360f / numberOfProjectiles);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject newHog = ObjectPooler.Instance.SpawnObject(currentRankParameters.projectilePrefab, weaponWielder.GetCenterPosition(), rotation);
            newHog.GetComponent<HedgehogProjectile>().Initalize(
                this.GetType().Name,
                weaponWielder.GetCenterTransform(),
                currentRankParameters.damage * characterStats.damageModifier,
                currentRankParameters.speed * characterStats.weaponSpeedModifier,
                currentRankParameters.knockbackPower,
                currentRankParameters.radius,
                currentRankParameters.duration * characterStats.durationModifier
                );
        }
    }

    public override void RankUp()
    {
        /*
        if (currentRank < baseParameters.ranks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Hedgehog.");
            currentRankParameters = baseParameters.ranks[currentRank];
        }
        else
        {
            Debug.Log("Maximum Hedgehog rank reached.");
        } */
        base.RankUp();
    }

}
