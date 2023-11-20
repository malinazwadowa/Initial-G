using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] public EnemyData enemyData;

    protected Player player;
    protected HealthController healthController;
    private EnemyMovementController enemyMovementController;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public virtual void Init()
    {
        player = PlayerManager.Instance.GetPlayer();
         
        healthController = GetComponent<HealthController>();
        healthController.Init(enemyData.maxHealth);

        enemyMovementController = GetComponent<EnemyMovementController>();
    }
    public virtual void Update()
    {
        if (!Utilities.IsObjectInView(1.2f, transform.position))
        {
            ObjectPooler.Instance.DeSpawnObject(gameObject);
        }
    }
  
    public void GetDamaged(float amount)
    {
        //Possibly armor logic. 
        healthController.SubstractCurrentHealth(amount);
        if(healthController.GetCurrentHealth() <= 0)
        {
            GetKilled();
        }
    }

    public void GetKilled()
    {
        ObjectPooler.Instance.DeSpawnObject(gameObject);
        LootManager.Instance.DropLoot(enemyData.tier, transform.position);
        EventManager.OnEnemyKilled?.Invoke();
    }

    public void GetKnockbacked(float power, Vector3 knockbackDirection)
    {
        if (gameObject.activeSelf)
        {
            float powerSpeedCompensation = (enemyData.speed / 50) + 1;
            knockbackDirection.Normalize();
            StartCoroutine(enemyMovementController.ApplyKnockback(power * powerSpeedCompensation, knockbackDirection));
        }
    }
}
