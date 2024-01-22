using NaughtyAttributes;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [Expandable][SerializeField] public SO_EnemyParameters enemyParameters;

    protected Player player;
    protected HealthController healthController;
    private EnemyMovementController enemyMovementController;

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
    public virtual void Initialize()
    {
        player = PlayerManager.Instance.GetPlayer();
         
        healthController = GetComponent<HealthController>();
        healthController.Initialize(enemyParameters.maxHealth);

        enemyMovementController = GetComponent<EnemyMovementController>();
    }

    public virtual void Update()
    {
        if (!Utilities.IsObjectInView(3f, transform.position))
        {
            ObjectPooler.Instance.DespawnObject(gameObject);
        }
    }
  
    public void GetDamaged(float amount, string damageSource)
    {
        AudioManager.Instance.PlaySound(AudioClipID.EnemyHit);

        GameManager.Instance.gameStatsController.RegisterWeaponDamage(damageSource, Mathf.Min(amount, healthController.GetCurrentHealth()));

        healthController.SubstractCurrentHealth(amount);
        EventManager.OnEnemyDamaged?.Invoke((int)amount, transform.position);
        

        if(healthController.GetCurrentHealth() <= 0)
        {
            GameManager.Instance.gameStatsController.RegisterWeaponKill(damageSource);
            GameManager.Instance.gameStatsController.RegisterEnemyKill(enemyParameters.type);
            GetKilled();
        }
    }

    public void GetKilled()
    {
        LootManager.Instance.DropLoot(enemyParameters.tier, transform.position);
        
        LevelManager.Instance.AddScore();
        EventManager.OnEnemyKilled?.Invoke();
        ObjectPooler.Instance.DespawnObject(gameObject);
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        if (gameObject.activeSelf)
        {
            float powerSpeedCompensation = (enemyParameters.speed / 50) + 1;
            knockbackDirection.Normalize();
            StartCoroutine(enemyMovementController.ApplyKnockback(power * powerSpeedCompensation, knockbackDirection));
        }
    }
}
