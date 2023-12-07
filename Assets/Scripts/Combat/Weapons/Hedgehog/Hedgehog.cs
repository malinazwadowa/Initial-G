using UnityEngine;

public class Hedgehog : Weapon
{
    public SO_HedgehogParameters hedgehogBaseData;
    private HedgehogRank hedgehogCurrentRankData;
    private WeaponProperties currentHedgehogProperties;

    private bool hasFinishedSpinning = true;
    private float cooldownTimer;
    private float durationTimer;

    void Start()
    {
        hedgehogCurrentRankData = hedgehogBaseData.hedgehogRanks[currentRank];
        currentHedgehogProperties = new WeaponProperties();
        SetCurrentProperties();

        hedgehogBaseData.OnWeaponDataChanged += SetCurrentProperties;
        
        cooldownTimer = float.PositiveInfinity;
    }

    public override void SetCurrentProperties()
    {
        WeaponProperties hedgehogProperties = new WeaponProperties();
        hedgehogProperties.damage = hedgehogCurrentRankData.damage * combatStats.damageModifier;
        hedgehogProperties.cooldown = hedgehogCurrentRankData.cooldown * combatStats.cooldownModifier;
        hedgehogProperties.radius = hedgehogCurrentRankData.radius;
        hedgehogProperties.duration = hedgehogCurrentRankData.duration;
        hedgehogProperties.speed = hedgehogCurrentRankData.speed * combatStats.speedModifier;
        hedgehogProperties.amount = hedgehogCurrentRankData.amount;
        hedgehogProperties.prefab = hedgehogCurrentRankData.projectilePrefab;
        hedgehogProperties.knockbackPower = hedgehogCurrentRankData.knockbackPower;
        currentHedgehogProperties = hedgehogProperties;
    }

    public override void WeaponTick()
    {
        base.WeaponTick();

        cooldownTimer += Time.deltaTime;

        if (hasFinishedSpinning == true && cooldownTimer > currentHedgehogProperties.cooldown) 
        {
            SpawnHedgehogs();
            hasFinishedSpinning=false;
            durationTimer = 0;
        }

        if(hasFinishedSpinning == false)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer > currentHedgehogProperties.duration)
            {
                cooldownTimer = 0;
                hasFinishedSpinning = true;
            }
        }

    }

    public void SpawnHedgehogs()
    {
        int numberOfProjectiles = currentHedgehogProperties.amount;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * (360f / numberOfProjectiles);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject hedgehog = ObjectPooler.Instance.SpawnObject(currentHedgehogProperties.prefab, myWeaponWielder.GetCenterPosition(), rotation);
            InitializeHedgehog(hedgehog);
        }
    }

    private void InitializeHedgehog(GameObject hedgehog)
    {
        //xd
        var hedgehogProjectile = hedgehog.GetComponent<HedgehogProjectile>();
        var weaponTransform = myWeaponWielder.GetCenterTransform();
        var damage = currentHedgehogProperties.damage;
        var speed = currentHedgehogProperties.speed;
        var knockbackPower = currentHedgehogProperties.knockbackPower;
        var radius = currentHedgehogProperties.radius;
        var duration = currentHedgehogProperties.duration;

        hedgehogProjectile.Init(weaponTransform, damage, speed, knockbackPower, radius, duration);
    }

    public override void RankUp()
    {
        if (currentRank < hedgehogBaseData.hedgehogRanks.Length - 1)
        {
            base.RankUp();
            Debug.Log("Ranking up Hedgehog.");
            hedgehogCurrentRankData = hedgehogBaseData.hedgehogRanks[currentRank];
            SetCurrentProperties();
        }
        else
        {
            Debug.Log("Maximum Hedgehog rank reached.");
        }
    }

}
