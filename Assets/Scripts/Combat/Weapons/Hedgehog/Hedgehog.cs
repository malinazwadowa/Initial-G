using UnityEngine;

public class Hedgehog : Weapon
{
    public SO_HedgehogParameters baseParameters;
    private HedgehogRank currentRankParameters;
    //private WeaponProperties currentRankParameters;

    private bool hasFinishedSpinning = true;
    private float cooldownTimer;
    private float durationTimer;

    public override void Initialize(IWeaponWielder weaponWielder, CharacterStats characterStats)
    {
        base.Initialize(weaponWielder, characterStats);
        baseParameters = ItemParametersList.Instance.SO_HedgehogParameters;
        currentRankParameters = baseParameters.hedgehogRanks[currentRank];
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
        if (currentRank < baseParameters.hedgehogRanks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Hedgehog.");
            currentRankParameters = baseParameters.hedgehogRanks[currentRank];
        }
        else
        {
            Debug.Log("Maximum Hedgehog rank reached.");
        }
    }

}
